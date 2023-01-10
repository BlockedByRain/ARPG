using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInput : IUserInput
{
    [Header("-----JoystickSettings-----")]
    public string axisX = "axisX";
    public string axisY = "axisY";
    public string axisJright = "axis4";
    public string axisJup = "axis5";

    [Header("-----跳冲砍-----")]
    public string btnA = "Btn0";
    public string btnB = "Btn1";
    public string btnX = "Btn2";
    public string btnY = "Btn3";

    [Header("-----左右肩-----")]
    public string btnLB = "Btn4";
    public string btnRB = "Btn5";

    [Header("-----右摇杆-----")]
    public string btnJstick = "Btn9";


    public InputButton buttonA = new InputButton();
    public InputButton buttonB = new InputButton();
    public InputButton buttonX = new InputButton();
    public InputButton buttonY = new InputButton();
    public InputButton buttonLB = new InputButton();
    public InputButton buttonRB = new InputButton();
    public InputButton buttonJstick = new InputButton();


    //[Header("-----OutputSignal-----")]
    //public float Dup;
    //public float Dright;
    //public float Dmag;      //当前输入向量模
    //public Vector3 Dvec;

    //public float Jright;
    //public float Jup;


    ////1 pressing signal
    //[Header("-----pressing signal-----")]
    //public bool run;

    ////2 trigger once signal
    //[Header("-----trigger once signal-----")]
    //public bool jump;
    //private bool lastJump;

    //public bool attack;
    //private bool lastAttack;

    ////3 double trigger
    ////[Header("-----pressing signal-----")]



    //[Header("-----Others-----")]
    //public bool InputEnabled = true;
    //private float TargetDup;
    //private float TargetDright;
    //private float velocityDup;
    //private float velocityDright;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        buttonA.Tick(Input.GetButton(btnA));
        buttonB.Tick(Input.GetButton(btnB));
        buttonX.Tick(Input.GetButton(btnX));
        buttonY.Tick(Input.GetButton(btnY));
        buttonLB.Tick(Input.GetButton(btnLB));
        buttonRB.Tick(Input.GetButton(btnRB));
        buttonJstick.Tick(Input.GetButton(btnJstick));

        //camera信号
        Jup = -1* Input.GetAxis(axisJup);
        Jright = Input.GetAxis(axisJright);


        TargetDup = Input.GetAxis(axisY);
        TargetDright = Input.GetAxis(axisX);

        //未启用时置零
        if (InputEnabled == false)
        {
            TargetDup = 0;
            TargetDright = 0;
        }

        //变化时间，灵敏度
        Dup = Mathf.SmoothDamp(Dup, TargetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, TargetDright, ref velocityDright, 0.1f);

        Vector2 tempDAxis = SquareToCircle(Dup, Dright);
        float normalDup = tempDAxis.x;
        float normalDright = tempDAxis.y;
        Dmag = Mathf.Sqrt((normalDup * normalDup) + (normalDright * normalDright));
        Dvec = normalDup * transform.forward + normalDright * transform.right;

        //跑动
        run = (buttonA.IsPressing && !buttonA.Isdelaying) || buttonA.IsExtending;
        
        //防御
        defense = buttonLB.IsPressing;

        //跳跃
        jump = buttonB.OnPressed && buttonB.IsExtending;

        //攻击
        attack = buttonX.OnPressed;

        //锁定
        lockon = buttonJstick.OnPressed;

    }


    ////椭圆映射法解决斜向移动1.414
    //private Vector2 SquareToCircle(float Dup, float Dright)
    //{
    //    Vector2 input = new Vector2(Dup, Dright);
    //    Vector2 output = Vector2.zero;
    //    output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
    //    output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);
    //    return output;


    //}







}
