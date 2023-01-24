using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTimer
{
    public enum STATE
    {
        IDLE,
        RUN,
        FINISHED,
    }
    public STATE state;

    //����ʱ��
    public float durationTime = 1.0f;
    //����ʱ��
    private float elapsedTime=0;

    public void Tick()
    {
        if (state ==STATE.IDLE)
        {

        }
        else if (state ==STATE.RUN)
        {
            elapsedTime+=Time.deltaTime;
            if (elapsedTime >= durationTime)
            {
                state = STATE.FINISHED;
            }
        }
        else if(state ==STATE.FINISHED)
        {

        }
        else
        {
            Debug.Log("Timer error");
        }

        
    }

    public void Execute()
    {
        elapsedTime=0;
        state = STATE.RUN;
    }



}
