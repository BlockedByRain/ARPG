using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{

    public GameObject model;
    public PlayerInput pi;
    public float walkSpeed = 2.0f;
    public float runMultiplier;
    public float jumpVelocity;

    [SerializeField]
    private Animator anim;
    private Rigidbody rb;

    private Vector3 planarVec;
    private Vector3 thrustVec;      //��Ծ����

    [SerializeField]
    private bool lockPlanar=false;      //ƽ���ƶ�����



    private void Awake()
    {
        anim = model.GetComponent<Animator>();
        pi = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
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
        anim.SetFloat("Forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("Forward"),targetRunMulti,0.5f));
        if (pi.jump)
        {
            anim.SetTrigger("Jump");
        }


        if (pi.Dmag>0.1f)       //��ֹ������С���³������
        {
            //ȡ��������֮��ת��
            //�������β�ֵƽ��ת��
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
        //�����õ�д��
        //rb.position += planarVec * Time.deltaTime;
        rb.velocity = new Vector3(planarVec.x,rb.velocity.y,planarVec.z)+thrustVec;
        thrustVec = Vector3.zero;
    }


    public void OnJumpEnter()
    {
        //Debug.Log("On jump enter");
        pi.InputEnabled = false;
        lockPlanar = true;
        thrustVec = new Vector3(0, jumpVelocity, 0);
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
    }



}
