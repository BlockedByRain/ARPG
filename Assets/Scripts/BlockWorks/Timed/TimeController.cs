using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoSingleton<TimeController>
{
    // 定义静态变量gravity，用于控制物体的重力加速度
    public static float gravity = -100;

    public Text index;

    public Text count;



    // 定义结构体，用于存储回放数据
    public struct RecordedData
    {
        public Vector2 postion;  // 位置
        public Vector2 velocity; // 速度
        public float animationTime; // 动画时间
    }

    // 存储单个对象的回放数据
    RecordedData[,] recordedData;

    // 最大记录数量，一般不需要太多，100000帧的数据大约对应27分钟的记录时间
    int recordMax = 10000;
    // 当前记录数量
    int recordCount;
    // 当前记录的索引
    int recordIndex;

    // 标志位，用于判断是否正在回放
    bool wasSteppingBack = false;

    // 所有可回溯的物体
    TimeControlled[] timeObjects;

    //状态
    public TIMESTATE state;

    public enum TIMESTATE
    {
        stepBack,
        pause,
        stepForward,
        normal,

    }

    private void Awake()
    {
        // 搜集所有可回溯物体
        timeObjects = GameObject.FindObjectsOfType<TimeControlled>();

        // 初始化记录数据数组
        recordedData = new RecordedData[timeObjects.Length, recordMax];
    }

    void Update()
    {
        index.text = "Index:" + recordIndex.ToString();
        count.text = "Count:" + recordCount.ToString();



        //print(timeObjects.Length);
        {
            // 获取按键状态
            bool pause = Input.GetKey(KeyCode.UpArrow); // 暂停
            bool stepBack = Input.GetKey(KeyCode.LeftArrow); // 回放
            bool stepForward = Input.GetKey(KeyCode.RightArrow); // 快进

            if (pause && !stepBack && !stepForward)
            {
                print("暂停");
                state = TIMESTATE.pause;
            }
            // 回放情况
            else if (stepBack)
            {
                print("回放");
                state = TIMESTATE.stepBack;

                wasSteppingBack = true;

                if (recordIndex > 0)
                {

                    recordCount--;
                    //count会为负数导致数组越界，做一个约束
                    recordCount = Mathf.Clamp(recordCount, 0, recordMax - 2);

                    // 遍历所有需要操控时间的物体，将其位置、速度、动画时间等回溯到上一帧
                    for (int objIndex = 0; objIndex < timeObjects.Length; objIndex++)
                    {
                        TimeControlled timeobj = timeObjects[objIndex];
                        RecordedData data = recordedData[objIndex, recordCount];
                        timeobj.transform.position = data.postion;
                        timeobj.velocity = data.velocity;

                        timeobj.animationTime = data.animationTime;
                        timeobj.UpdateAnimation();
                    }
                }
            }
            // 快进情况
            else if (pause && stepForward)
            {
                print("快进");
                state = TIMESTATE.stepForward;


                wasSteppingBack = true;
                if (recordIndex < recordCount - 1)
                {
                    recordCount++;

                    // 遍历所有需要操控时间的物体，将其位置、速度、动画时间等快进到下一帧
                    for (int objIndex = 0; objIndex < timeObjects.Length; objIndex++)
                    {
                        TimeControlled timeobj = timeObjects[objIndex];
                        RecordedData data = recordedData[objIndex, recordCount];
                        timeobj.transform.position = data.postion;
                        timeobj.velocity = data.velocity;

                        timeobj.animationTime = data.animationTime;
                        timeobj.UpdateAnimation();
                    }
                }
            }
            // 正常情况
            else if (!pause && !stepBack)
            {
                print("正常");
                state = TIMESTATE.normal;

                // 如果上一帧是回放状态，则将记录的帧数更新到当前帧数
                if (wasSteppingBack)
                {
                    recordCount = recordIndex;
                    wasSteppingBack = false;
                }

                //遍历所有可回溯物体，记录它们的位置、速度和动画时间
                for (int objIndex = 0; objIndex < timeObjects.Length; objIndex++)
                {
                    TimeControlled timeobj = timeObjects[objIndex];
                    RecordedData data = new RecordedData();
                    data.postion = timeobj.transform.position;
                    data.velocity = timeobj.velocity;
                    data.animationTime = timeobj.animationTime;
                    recordedData[objIndex, recordCount] = data;
                }

                //更新记录数量和索引
                recordCount++;

                //确保recordCount始终小于recordMax-1
                recordCount = Mathf.Min(recordCount, recordMax - 2);
                recordIndex = recordCount;

                //遍历所有可回溯物体，执行它们的时间更新和动画更新
                foreach (var timeObject in timeObjects)
                {
                    timeObject.TimeUpdate();
                    timeObject.UpdateAnimation();
                }
            }



        }
    }
}


