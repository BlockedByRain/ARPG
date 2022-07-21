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
        //����pi
        //print(pi.Dup);

        //���ɶ��������κνǶ����붼����ǰ
        anim.SetFloat("Forward", pi.Dmag);

        if (pi.Dmag>0.1f)       //��ֹ������С���³������
        {
            //ȡ��������֮��ת��
            model.transform.forward = pi.Dvec;
        }

        movingVec =pi.Dmag * model.transform.forward*walkSpeed;


        




    }



    private void FixedUpdate()
    {
        //�����õ�д��
        //rb.position += movingVec * Time.deltaTime;
        rb.velocity = new Vector3(movingVec.x,rb.velocity.y,movingVec.z);
    }








}
