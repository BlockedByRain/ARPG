using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{

    public GameObject model;
    public PlayerInput pi;
    public float walkSpeed = 2.0f;

    [SerializeField]
    private Animator anim;
    private Rigidbody rb;

    private Vector3 movingVec;



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

        //勾股定理，无论任何角度输入都能向前
        anim.SetFloat("Forward", pi.Dmag);

        if (pi.Dmag>0.1f)       //防止向量过小导致朝向错误
        {
            //取两个向量之和转向
            model.transform.forward = pi.Dvec;
        }

        movingVec =pi.Dmag * model.transform.forward*walkSpeed;


        




    }



    private void FixedUpdate()
    {
        //不常用的写法
        //rb.position += movingVec * Time.deltaTime;
        rb.velocity = new Vector3(movingVec.x,rb.velocity.y,movingVec.z);
    }








}
