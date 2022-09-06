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
    private Vector3 thrustVec;      //跳跃冲量

    [SerializeField]
    private bool lockPlanar=false;      //平面移动开关



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
        //测试pi
        //print(pi.Dup);

        //勾股定理，无论任何角度输入都能向前，加速插值平滑
        float targetRunMulti = (pi.run) ? 2.0f : 1.0f;
        anim.SetFloat("Forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("Forward"),targetRunMulti,0.5f));
        if (pi.jump)
        {
            anim.SetTrigger("Jump");
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
        //不常用的写法
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
