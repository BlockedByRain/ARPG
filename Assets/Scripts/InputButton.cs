using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputButton
{
    public bool IsPressing = false;
    public bool OnPressed = false;
    public bool OnReleased = false;
    public bool IsExtending = false;
    public bool Isdelaying = false;

    private bool curState = false;
    private bool lastState = false;

    public float extendingDurationTime = 0.15f;
    public float delayDurationTime = 0.15f;


    private InputTimer extTimer = new InputTimer();
    private InputTimer delayTimer =new InputTimer();

    public void Tick(bool input)
    {
        extTimer.Tick();
        delayTimer.Tick();

        curState = input;
        IsPressing = curState;

        OnPressed = false;
        OnReleased = false;
        IsExtending = false;
        Isdelaying = false;

        if (curState != lastState)
        {
            if (curState == true)
            {
                OnPressed = true;
                StartTimer(delayTimer,delayDurationTime);
            }
            else
            {
                OnReleased = true;
                StartTimer(extTimer, extendingDurationTime);
            }

        }
        
        //为触发时段时为true；
        if (extTimer.state == InputTimer.STATE.RUN)
        {
            IsExtending = true;
        }

        if (delayTimer.state==InputTimer.STATE.RUN)
        {
            Isdelaying = true;
        }


        lastState = curState;
    }

    private void StartTimer(InputTimer timer, float durationTime)
    {
        timer.durationTime = durationTime;
        timer.Execute();
    }


}
