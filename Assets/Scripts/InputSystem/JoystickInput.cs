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

    [Header("-----Ã¯≥Âø≥-----")]
    public string btnA = "Btn0";
    public string btnB = "Btn1";
    public string btnX = "Btn2";
    public string btnY = "Btn3";

    [Header("-----◊Û”“ºÁ-----")]
    public string btnLB = "Btn4";
    public string btnRB = "Btn5";

    [Header("-----◊Û”“∞‚ª˙-----")]
    public string btnLT = "axis3";
    public string btnRT = "axis3";

    [Header("-----”““°∏À-----")]
    public string btnJstick = "Btn9";


    public InputButton buttonA = new InputButton();
    public InputButton buttonB = new InputButton();
    public InputButton buttonX = new InputButton();
    public InputButton buttonY = new InputButton();
    public InputButton buttonLB = new InputButton();
    public InputButton buttonRB = new InputButton();
    public InputButton buttonJstick = new InputButton();


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

        //camera–≈∫≈
        Jup = -1 * Input.GetAxis(axisJup);
        Jright = Input.GetAxis(axisJright);

        //move–≈∫≈
        TargetDup = Input.GetAxis(axisY);
        TargetDright = Input.GetAxis(axisX);



        //Œ¥∆Ù”√ ±÷√¡„
        if (InputEnabled == false)
        {
            TargetDup = 0;
            TargetDright = 0;
        }

        //±‰ªØ ±º‰£¨¡È√Ù∂»
        Dup = Mathf.SmoothDamp(Dup, TargetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, TargetDright, ref velocityDright, 0.1f);

        Vector2 tempDAxis = SquareToCircle(Dup, Dright);
        float normalDup = tempDAxis.x;
        float normalDright = tempDAxis.y;
        Dmag = Mathf.Sqrt((normalDup * normalDup) + (normalDright * normalDright));
        Dvec = normalDup * transform.forward + normalDright * transform.right;

        //≈‹∂Ø
        run = (buttonA.IsPressing && !buttonA.Isdelaying) || buttonA.IsExtending;

        //∑¿”˘
        defense = buttonLB.IsPressing;

        //Ã¯‘æ
        jump = buttonB.OnPressed && buttonB.IsExtending;

        //∫ÛÃ¯
        roll = buttonX.OnReleased && buttonX.Isdelaying;

        //π•ª˜
        //attack = buttonX.OnPressed;

        //À¯∂®
        lockon = buttonJstick.OnPressed;

        rb = buttonRB.OnPressed;
        lb = buttonLB.OnPressed;

        //∞‚ª˙–≈∫≈
        lt = Input.GetAxisRaw(btnLT) < 0 ? true : false;
        rt = Input.GetAxisRaw(btnRT) > 0 ? true : false;


    }









}
