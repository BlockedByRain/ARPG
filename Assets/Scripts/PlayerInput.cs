using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("-----KeySetting-----")]
    public string KeyUp = "w";
    public string KeyDown = "s";
    public string KeyLeft = "a";
    public string KeyRight = "d";

    public string KeyA;
    public string KeyB;
    public string KeyC;
    public string KeyD;

    public string keyJRight;
    public string keyJLeft;
    public string keyJUp;
    public string keyJDown;





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

    //2 trigger once signal
    [Header("-----trigger once signal-----")]
    public bool jump;
    private bool lastJump;

    public bool attack;
    private bool lastAttack;

    //3 double trigger
    //[Header("-----pressing signal-----")]



    [Header("-----Others-----")]
    public bool InputEnabled = true;
    private float TargetDup;
    private float TargetDright;
    private float velocityDup;
    private float velocityDright;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyDown(KeyUp))
        //{
        //    print("keyup is pressed!");
        //}

        //camera信号
        Jup = (Input.GetKey(keyJUp) ? 1.0f : 0) - (Input.GetKey(keyJDown) ? 1.0f : 0);
        Jright = (Input.GetKey(keyJRight) ? 1.0f : 0) - (Input.GetKey(keyJLeft) ? 1.0f : 0);







        TargetDup = (Input.GetKey(KeyUp) ? 1.0f : 0) - (Input.GetKey(KeyDown) ? 1.0f : 0);
        TargetDright = (Input.GetKey(KeyRight) ? 1.0f : 0) - (Input.GetKey(KeyLeft) ? 1.0f : 0);

        //未启用时置零
        if (InputEnabled == false)
        {
            TargetDup = 0;
            TargetDright = 0;
        }

        //变化时间，灵敏度
        Dup = Mathf.SmoothDamp(Dup, TargetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, TargetDright, ref velocityDright, 0.1f);
        Vector2 tempDAxis=SquareToCircle(Dup,Dright);
        float normalDup = tempDAxis.x;
        float normalDright = tempDAxis.y;
        Dmag = Mathf.Sqrt((normalDup * normalDup) + (normalDright * normalDright));
        Dvec = normalDup * transform.forward + normalDright * transform.right;

        run = Input.GetKey(KeyA);

        //跳跃
        bool newJump=Input.GetKey(KeyB);
        //jump = newJump;
        if (newJump==true && lastJump==false)
        {
            jump = true;
            //Debug.Log("jumping!");
        }
        else
        {
            jump = false;
        }
        lastJump = newJump;

        //攻击
        bool newAttack = Input.GetKey(KeyC);
        if (newAttack == true && lastAttack == false)
        {
            attack = true;
        }
        else
        {
            attack = false;
        }
        lastAttack = newAttack;

    }


    //椭圆映射法解决斜向移动1.414
    private Vector2 SquareToCircle(float Dup,float Dright)
    {
        Vector2 input = new Vector2(Dup, Dright);
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);
        return output;


    }



}
