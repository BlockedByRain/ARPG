using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : IUserInput
{
    [Header("-----KeySettings-----")]
    public string KeyUp = "w";
    public string KeyDown = "s";
    public string KeyLeft = "a";
    public string KeyRight = "d";

    public string KeyRun;
    public string KeyJump;
    public string KeyAttack;
    public string KeyDefense;
    public string KeyLock;

    [Space(10)]
    [Header("-----CameraSettings-----")]
    public string keyJRight;
    public string keyJLeft;
    public string keyJUp;
    public string keyJDown;


    public Dictionary<InputButton,string> buttonList =new Dictionary<InputButton, string>();


    public InputButton buttonA = new InputButton();
    public InputButton buttonB = new InputButton();
    public InputButton buttonC = new InputButton();
    public InputButton buttonD = new InputButton();
    public InputButton buttonE = new InputButton();


    [Header("-----MouseSettings-----")]
    //鼠标显示
    public bool mouseEnable = false;
    //鼠标灵敏度
    public float mouseSensitivityX = 1.0f;
    public float mouseSensitivityY = 1.0f;




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

    private void Awake()
    {
        buttonList.Add(buttonA,KeyRun);
        buttonList.Add(buttonB,KeyJump);
        buttonList.Add(buttonC,KeyAttack);
        buttonList.Add(buttonD,KeyDefense);
        buttonList.Add(buttonE,KeyLock);
    }

    // Update is called once per frame
    void Update()
    {
        buttonA.Tick(Input.GetKey(KeyRun));
        buttonB.Tick(Input.GetKey(KeyJump));
        buttonC.Tick(Input.GetKey(KeyAttack));
        buttonD.Tick(Input.GetKey(KeyDefense));
        buttonE.Tick(Input.GetKey(KeyLock));


        //Tick所有按键
        //TickButtonList(buttonList);


        //camera信号
        if (mouseEnable == true)
        {
            //鼠标
            Jup = Input.GetAxis("Mouse Y") * 3f * mouseSensitivityY;
            Jright = Input.GetAxis("Mouse X") * 2.5f * mouseSensitivityX;
        }
        else if (mouseEnable == false)
        {
            //键盘
            Jup = (Input.GetKey(keyJUp) ? 1.0f : 0) - (Input.GetKey(keyJDown) ? 1.0f : 0);
            Jright = (Input.GetKey(keyJRight) ? 1.0f : 0) - (Input.GetKey(keyJLeft) ? 1.0f : 0);

        }


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

        Vector2 tempDAxis = SquareToCircle(Dup, Dright);
        float normalDup = tempDAxis.x;
        float normalDright = tempDAxis.y;
        Dmag = Mathf.Sqrt((normalDup * normalDup) + (normalDright * normalDright));
        Dvec = normalDup * transform.forward + normalDright * transform.right;

        //跑动
        run = (buttonA.IsPressing && !buttonA.Isdelaying) || buttonA.IsExtending;
        //跳跃
        //jump = buttonB.OnPressed && buttonB.IsExtending;
        jump = buttonB.OnPressed;

        //攻击
        attack = buttonC.OnPressed;
        //防御
        defense = buttonD.IsPressing;
        //锁定
        lockon = buttonE.OnPressed;

    }


    ////椭圆映射法解决斜向移动1.414
    //private Vector2 SquareToCircle(float Dup,float Dright)
    //{
    //    Vector2 input = new Vector2(Dup, Dright);
    //    Vector2 output = Vector2.zero;
    //    output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
    //    output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);
    //    return output;


    //}


    private void TickButtonList(Dictionary<InputButton,string> list)
    {
        foreach (KeyValuePair<InputButton,string> pair in list)
        {
            pair.Key.Tick(Input.GetKey(pair.Value));
        }
    }

}
