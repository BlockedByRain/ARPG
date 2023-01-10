using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUserInput : MonoBehaviour
{
    [Header("-----OutputSignal-----")]
    public float Dup;
    public float Dright;
    public float Dmag;      //当前输入向量模
    public Vector3 Dvec;

    public float Jright;
    public float Jup;


    //1 pressing signal
    [Header("-----pressing signal-----")]
    public bool run;
    public bool defense;

    //2 trigger once signal
    [Header("-----trigger once signal-----")]
    public bool jump;
    protected bool lastJump;

    public bool attack;
    protected bool lastAttack;
    public bool roll;
    public bool lockon;

    //3 double trigger
    //[Header("-----pressing signal-----")]



    [Header("-----Others-----")]
    public bool InputEnabled = true;
    protected float TargetDup;
    protected float TargetDright;
    protected float velocityDup;
    protected float velocityDright;

    //椭圆映射法解决斜向移动1.414
    protected Vector2 SquareToCircle(float Dup, float Dright)
    {
        Vector2 input = new Vector2(Dup, Dright);
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);
        return output;


    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }










}
