using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KianaController : ActorController
{
    public CameraController camctl;
    public float walkSpeed;
    public float runMultiplier;
    public float jumpVelocity;
    public float evadeForwardVelocity;
    public float evadeBackwardVelocity;
    public int maxEvadeNum;

    [Space(10)]
    [Header("-----Friction Settings-----")]
    public PhysicMaterial frictionZero;
    public PhysicMaterial frictionOne;

    private GhostEffectController ghostEffectController;

    private Animator anim;
    private Rigidbody rb;

    private Vector3 planarVec;
    private Vector3 thrustVec;      //跳跃冲量

    private bool canAttack;     //攻击开关

    private bool lockPlanar = false;      //平面移动开关
    private bool trackDirection = false;

    private int curEvadeNum = 0;

    private CapsuleCollider col;

    private float lerpTarget;       //动画权重

    private Vector3 deltaPos;

    private void Awake()
    {
        IUserInput[] inputs = GetComponents<IUserInput>();
        camctl = GetComponentInChildren<CameraController>();
        ghostEffectController = GetComponentInChildren<GhostEffectController>();

        foreach (var input in inputs)
        {
            if (input.enabled == true)
            {
                pi = input;
                break;
            }
        }
        anim = model.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //测试pi
        //print(pi.Dup);

        //勾股定理，无论任何角度输入都能向前，加速插值平滑
        float targetRunMulti = (pi.run) ? 2.0f : 1.0f;

        if (camctl.lockState == false)
        {
            anim.SetFloat("Forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("Forward"), targetRunMulti, 0.5f));
        }
        else
        {
            Vector3 localDvec = transform.InverseTransformVector(pi.Dvec);
            anim.SetFloat("Forward", localDvec.z * targetRunMulti);

        }



        //if (pi.dodge && curEvadeNum<maxEvadeNum)
        if (pi.dodge)
        {
            anim.SetTrigger("Evade");
            canAttack = false;
        }

        if (pi.jump)
        {
            anim.SetTrigger("Jump");
            canAttack = false;
        }

        //禁用过渡期间的攻击
        //CheckState("ground")&&!CheckState("roll")&&!anim.IsInTransition(anim.GetLayerIndex("Base Layer"))
        if (pi.attack && CheckState("Ground") && canAttack == true && !CheckState("roll") && !anim.IsInTransition(anim.GetLayerIndex("Base Layer")))
        {
            anim.SetTrigger("Attack");
        }

        //锁定
        if (pi.lockon)
        {
            camctl.SwitchLock();
        }

        if (camctl.lockState == false)
        {
            if (pi.Dmag > 0.1f)       //防止向量过小导致朝向错误
            {
                //取两个向量之和转向
                //利用球形插值平滑转向
                Vector3 targetForward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.4f);
                model.transform.forward = targetForward;
            }
            if (lockPlanar == false)
            {
                planarVec = pi.Dmag * model.transform.forward * walkSpeed * ((pi.run) ? runMultiplier : 1.0f);
            }
        }
        else
        {
            if (trackDirection == false)
            {
                model.transform.forward = transform.forward;

            }
            else
            {
                model.transform.forward = planarVec.normalized;
            }

            if (lockPlanar == false)
            {
                planarVec = pi.Dvec * walkSpeed * ((pi.run) ? runMultiplier : 1.0f);

            }
        }




    }

    private void FixedUpdate()
    {
        //rb.position += deltaPos;
        transform.position += deltaPos;
        deltaPos = Vector3.zero;

        //不常用的写法
        //rb.position += planarVec * Time.deltaTime;
        rb.velocity = new Vector3(planarVec.x, rb.velocity.y, planarVec.z) + thrustVec;
        thrustVec = Vector3.zero;




    }


    private bool CheckState(string stateName, string layerName = "Base Layer")
    {
        //是否是查询的状态
        int layerIndex = anim.GetLayerIndex(layerName);
        return anim.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);
    }




    /// <summary>
    /// message
    /// </summary>

    public void OnJumpEnter()
    {
        //Debug.Log("On jump enter");
        thrustVec = new Vector3(0, jumpVelocity, 0);
        pi.InputEnabled = false;
        lockPlanar = true;
        trackDirection = true;
    }

    //public void OnJumpExit()
    //{
    //    //Debug.Log("On jump exit");
    //    pi.InputEnabled = true;
    //    lockPlanar = false;
    //}

    public void IsGround()
    {
        //print("is on ground");
        anim.SetBool("IsGround", true);
    }

    public void IsNotGround()
    {
        //print("is not on ground");
        anim.SetBool("IsGround", false);

    }

    public void OnGroundEnter()
    {
        pi.InputEnabled = true;
        lockPlanar = false;
        canAttack = true;
        col.material = frictionOne;
        trackDirection = false;
        curEvadeNum = 0;
    }

    public void OnGroundExit()
    {
        col.material = frictionZero;
    }

    public void OnFallEnter()
    {
        pi.InputEnabled = false;
        lockPlanar = true;
    }

    public void OnEvadeForwardEnter()
    {
        if (pi.run == false)
        {
            planarVec = Vector3.zero;
            planarVec = model.transform.forward * evadeForwardVelocity;
        }
        
        pi.InputEnabled = false;
        lockPlanar = true;
        trackDirection = true;
        curEvadeNum += 1;
        col.material = frictionOne;
    }

    public void OnEvadeForwardUpdate()
    {
        anim.SetFloat("Forward", anim.GetFloat("Forward") * 0.9f);

    }

    public void OnEvadeForwardExit()
    {
        planarVec = Vector3.zero;
    }

    public void OnEvadeBackwardEnter()
    {
        planarVec = model.transform.forward * -evadeBackwardVelocity;

        pi.InputEnabled = false;
        lockPlanar = true;
        curEvadeNum += 1;

        SwitchGhostEffects(true);
        
    }




    public void OnEvadeBackwardUpdate()
    {

    }

    public void OnEvadeBackwardExit()
    {
        SwitchGhostEffects(false);
    }

    public void OnAttack1hAEnter()
    {
        pi.InputEnabled = false;
        lerpTarget = 1.0f;

    }

    public void OnAttack1hAUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("Attack1hAVelocity");
        float currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("Attack"));
        currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.4f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack"), currentWeight);

    }


    public void OnAttackIdleEnter()
    {
        pi.InputEnabled = true;
        lerpTarget = 0f;
        //anim.SetLayerWeight(anim.GetLayerIndex("Attack"), 0);

    }

    public void OnAttackIdleUpdate()
    {
        float currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("Attack"));
        currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.05f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack"), currentWeight);

    }

    //rootmotion处理
    public void OnUpdateRM(object _deltaPos)
    {
        if (CheckState("EvadeForward", "Base Layer"))
        {
            //deltaPos += deltaPos + (Vector3)_deltaPos;
            //平滑处理
            //deltaPos += (0.6f * deltaPos + 0.4f * (Vector3)_deltaPos);
        }

        if (CheckState("EvadeBackward", "Base Layer"))
        {
            //deltaPos += deltaPos + (Vector3)_deltaPos;
            //平滑处理
            deltaPos += (0.6f * deltaPos + 0.4f * (Vector3)_deltaPos);
        }


        if (CheckState("attack1hC", "Attack"))
        {
            //deltaPos += deltaPos + (Vector3)_deltaPos;
            //平滑处理
            deltaPos += (0.6f * deltaPos + 0.4f * (Vector3)_deltaPos);
        }

    }

    public void SwitchGhostEffects(bool use)
    {
        foreach (var effect in ghostEffectController.effects)
        {
            effect.openGhostEffect = use;
        }
    }




}
