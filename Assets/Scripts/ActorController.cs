using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{

    public GameObject model;
    public PlayerInput pi;
    public float walkSpeed;
    public float runMultiplier;
    public float jumpVelocity;
    public float rollVelocity;
    public float jabVelocity;

    [Space(10)]
    [Header("-----Friction Settings-----")]
    public PhysicMaterial frictionZero;
    public PhysicMaterial frictionOne;
    




    private Animator anim;
    private Rigidbody rb;

    private Vector3 planarVec;
    private Vector3 thrustVec;      //跳跃冲量

    private bool lockPlanar=false;      //平面移动开关

    private CapsuleCollider col;

    private float lerpTarget;       //动画权重

    private Vector3 deltaPos;

    private void Awake()
    {
        anim = model.GetComponent<Animator>();
        pi = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        col= GetComponent<CapsuleCollider>();
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
        anim.SetFloat("Forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("Forward"),targetRunMulti,0.5f));

        if (rb.velocity.magnitude>1.0f)
        {
            anim.SetTrigger("Roll");
        }
        
        
        
        if (pi.jump)
        {
            anim.SetTrigger("Jump");
        }



        //禁用过渡期间的攻击
        //CheckState("ground")&&!CheckState("roll")&&!anim.IsInTransition(anim.GetLayerIndex("Base Layer"))
        if (pi.attack && CheckState("Ground") && !CheckState("roll") && !anim.IsInTransition(anim.GetLayerIndex("Base Layer")))
        {
            anim.SetTrigger("Attack");
        }



        if (pi.Dmag>0.1f)       //防止向量过小导致朝向错误
        {
            //取两个向量之和转向
            //利用球形插值平滑转向
            Vector3 targetForward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.4f);
            model.transform.forward = targetForward;
        }
        if (lockPlanar==false)
        {
            planarVec = pi.Dmag * model.transform.forward * walkSpeed * ((pi.run) ? runMultiplier : 1.0f);
        }

        

    }

    private void FixedUpdate()
    {
        rb.position += deltaPos;
        
        //不常用的写法
        //rb.position += planarVec * Time.deltaTime;
        rb.velocity = new Vector3(planarVec.x,rb.velocity.y,planarVec.z)+thrustVec;
        thrustVec = Vector3.zero;

        deltaPos = Vector3.zero;
    }


    private bool CheckState(string stateName , string layerName="Base Layer")
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
        col.material = frictionOne;
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

    public void OnRollEnter()
    {
        thrustVec = new Vector3(0, rollVelocity, 0);
        pi.InputEnabled = false;
        lockPlanar=true;
    }


    public void OnJabEnter()
    {
        //thrustVec = model.transform.forward * -jabVelocity;
        pi.InputEnabled = false;
        lockPlanar = true;
        
    }


    public void OnJabUpdate()
    {
        //thrustVec = model.transform.forward * -jabVelocity;
        thrustVec = model.transform.forward * anim.GetFloat("JabVelocity");

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
        currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.2f);
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
        currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.5f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack"), currentWeight);

    }

    public void OnUpdateRM(object _deltaPos)
    {
        if (CheckState("attack1hC", "Attack"))
        {
            //deltaPos += (Vector3)_deltaPos;
            //print(_deltaPos);

            //动画有问题，写死位移值
            deltaPos = anim.transform.forward*0.003f;

        }
        
    }

}
