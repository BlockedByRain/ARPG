using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public GameObject model;
    public IUserInput pi;
    public CameraController camctl;
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
    private Vector3 thrustVec;      //��Ծ����

    private bool canAttack;     //��������

    private bool lockPlanar = false;      //ƽ���ƶ�����
    private bool trackDirection = false;

    private CapsuleCollider col;

    private float lerpTarget;       //����Ȩ��

    private Vector3 deltaPos;

    private void Awake()
    {
        IUserInput[] inputs = GetComponents<IUserInput>();
        camctl = GetComponentInChildren<CameraController>();

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

        //����pi
        //print(pi.Dup);

        //���ɶ��������κνǶ����붼����ǰ�����ٲ�ֵƽ��
        float targetRunMulti = (pi.run) ? 2.0f : 1.0f;

        if (camctl.lockState == false)
        {
            anim.SetFloat("Forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("Forward"), targetRunMulti, 0.5f));
            anim.SetFloat("Right", 0);
        }
        else
        {
            Vector3 localDvec = transform.InverseTransformVector(pi.Dvec);
            anim.SetFloat("Forward", localDvec.z * targetRunMulti);
            anim.SetFloat("Right", localDvec.x * targetRunMulti);

        }

        anim.SetBool("Defense", pi.defense);

        //if (rb.velocity.magnitude>1.0f)
        if (pi.roll || rb.velocity.magnitude > 7f)
        {
            anim.SetTrigger("Roll");
            canAttack = false;
        }

        if (pi.jump)
        {
            anim.SetTrigger("Jump");
            canAttack = false;
        }

        //���ù����ڼ�Ĺ���
        //CheckState("ground")&&!CheckState("roll")&&!anim.IsInTransition(anim.GetLayerIndex("Base Layer"))
        if (pi.attack && CheckState("Ground") && canAttack == true && !CheckState("roll") && !anim.IsInTransition(anim.GetLayerIndex("Base Layer")))
        {
            anim.SetTrigger("Attack");
        }

        //����
        if (pi.lockon)
        {
            camctl.SwitchLock();
        }

        if (camctl.lockState == false)
        {
            if (pi.Dmag > 0.1f)       //��ֹ������С���³������
            {
                //ȡ��������֮��ת��
                //�������β�ֵƽ��ת��
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
            if (trackDirection==false)
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

        //�����õ�д��
        //rb.position += planarVec * Time.deltaTime;
        rb.velocity = new Vector3(planarVec.x, rb.velocity.y, planarVec.z) + thrustVec;
        thrustVec = Vector3.zero;




    }


    private bool CheckState(string stateName, string layerName = "Base Layer")
    {
        //�Ƿ��ǲ�ѯ��״̬
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
        trackDirection=false;
    }

    public void OnGroundExit()
    {
        col.material = frictionZero;
    }

    public void OnFallEnter()
    {
        print("aa");
        pi.InputEnabled = false;
        lockPlanar = true;
    }

    public void OnRollEnter()
    {
        thrustVec = new Vector3(0, rollVelocity, 0);
        pi.InputEnabled = false;
        lockPlanar = true;
        trackDirection=true;
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

    public void OnUpdateRM(object _deltaPos)
    {


        if (CheckState("attack1hC", "Attack"))
        {
            //ƽ������
            //deltaPos += deltaPos + (Vector3)_deltaPos;
            deltaPos += (0.6f * deltaPos + 0.4f * (Vector3)_deltaPos);
        }

    }

}
