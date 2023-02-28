using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : IActorManagerInterface
{
    public float HPMax = 15.0f;
    public float HP = 15.0f;

    [Header("一阶状态")]
    public bool isGround;
    public bool isJump;
    public bool isFall;
    public bool isRoll;
    public bool isJab;
    public bool isAttack;
    public bool isHit;
    public bool isDie;
    public bool isBlocked;
    public bool isDefense;
    public bool isCounterBack;      //关联状态
    public bool isCounterBackEnable;    //关联动画事件


    [Header("二阶状态")]
    public bool isAllowDefense;
    public bool isImmortal;
    public bool isCounterBackSuccess;
    public bool isCounterBackFailure;


    private void Start()
    {
        //修正HP数值
        //AddHP(0);
        HP = HPMax;
    }

    private void Update()
    {
        isGround = am.yc.CheckState("Ground");
        isJump = am.yc.CheckState("jump");
        isFall = am.yc.CheckState("fall");
        isRoll = am.yc.CheckState("roll");
        isJab = am.yc.CheckState("jab");
        isAttack = am.yc.CheckStateTag("attackL") || am.yc.CheckStateTag("attackR");
        isHit = am.yc.CheckState("hit");
        isDie = am.yc.CheckState("die");
        isBlocked = am.yc.CheckState("blocked");

        isCounterBack = am.yc.CheckState("counterBack");
        isCounterBackSuccess = isCounterBackEnable;
        isCounterBackFailure = isCounterBack && !isCounterBackEnable; ;


        isAllowDefense = isGround || isBlocked;
        isDefense = isAllowDefense && am.yc.CheckState("defense1h", "Defense");
        isImmortal = isRoll || isJab;
    }

    public void AddHP(float value)
    {
        HP += value;
        HP = Mathf.Clamp(HP, 0, HPMax);

    }




}
