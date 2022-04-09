using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{

    public GameObject model;
    public PlayerInput pi;

    [SerializeField]
    private Animator anim;



    private void Awake()
    {
        anim = model.GetComponent<Animator>();
        pi = GetComponent<PlayerInput>();
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(pi.Dup);

        //���ɶ��������κνǶ����붼����ǰ
        anim.SetFloat("Forward", pi.Dmag);

        //ȡ��������֮��ת��
        model.transform.forward = pi.Dvec;




    }
}
