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

        //勾股定理，无论任何角度输入都能向前
        anim.SetFloat("Forward", pi.Dmag);

        //取两个向量之和转向
        model.transform.forward = pi.Dvec;




    }
}
