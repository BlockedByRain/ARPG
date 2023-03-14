using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoSingleton<TimeController>
{
    // ���徲̬����gravity�����ڿ���������������ٶ�
    public static float gravity = -100;

    public Text index;

    public Text count;



    // ����ṹ�壬���ڴ洢�ط�����
    public struct RecordedData
    {
        public Vector2 postion;  // λ��
        public Vector2 velocity; // �ٶ�
        public float animationTime; // ����ʱ��
    }

    // �洢��������Ļط�����
    RecordedData[,] recordedData;

    // ����¼������һ�㲻��Ҫ̫�࣬100000֡�����ݴ�Լ��Ӧ27���ӵļ�¼ʱ��
    int recordMax = 10000;
    // ��ǰ��¼����
    int recordCount;
    // ��ǰ��¼������
    int recordIndex;

    // ��־λ�������ж��Ƿ����ڻط�
    bool wasSteppingBack = false;

    // ���пɻ��ݵ�����
    TimeControlled[] timeObjects;

    //״̬
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
        // �Ѽ����пɻ�������
        timeObjects = GameObject.FindObjectsOfType<TimeControlled>();

        // ��ʼ����¼��������
        recordedData = new RecordedData[timeObjects.Length, recordMax];
    }

    void Update()
    {
        index.text = "Index:" + recordIndex.ToString();
        count.text = "Count:" + recordCount.ToString();



        //print(timeObjects.Length);
        {
            // ��ȡ����״̬
            bool pause = Input.GetKey(KeyCode.UpArrow); // ��ͣ
            bool stepBack = Input.GetKey(KeyCode.LeftArrow); // �ط�
            bool stepForward = Input.GetKey(KeyCode.RightArrow); // ���

            if (pause && !stepBack && !stepForward)
            {
                print("��ͣ");
                state = TIMESTATE.pause;
            }
            // �ط����
            else if (stepBack)
            {
                print("�ط�");
                state = TIMESTATE.stepBack;

                wasSteppingBack = true;

                if (recordIndex > 0)
                {

                    recordCount--;
                    //count��Ϊ������������Խ�磬��һ��Լ��
                    recordCount = Mathf.Clamp(recordCount, 0, recordMax - 2);

                    // ����������Ҫ�ٿ�ʱ������壬����λ�á��ٶȡ�����ʱ��Ȼ��ݵ���һ֡
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
            // ������
            else if (pause && stepForward)
            {
                print("���");
                state = TIMESTATE.stepForward;


                wasSteppingBack = true;
                if (recordIndex < recordCount - 1)
                {
                    recordCount++;

                    // ����������Ҫ�ٿ�ʱ������壬����λ�á��ٶȡ�����ʱ��ȿ������һ֡
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
            // �������
            else if (!pause && !stepBack)
            {
                print("����");
                state = TIMESTATE.normal;

                // �����һ֡�ǻط�״̬���򽫼�¼��֡�����µ���ǰ֡��
                if (wasSteppingBack)
                {
                    recordCount = recordIndex;
                    wasSteppingBack = false;
                }

                //�������пɻ������壬��¼���ǵ�λ�á��ٶȺͶ���ʱ��
                for (int objIndex = 0; objIndex < timeObjects.Length; objIndex++)
                {
                    TimeControlled timeobj = timeObjects[objIndex];
                    RecordedData data = new RecordedData();
                    data.postion = timeobj.transform.position;
                    data.velocity = timeobj.velocity;
                    data.animationTime = timeobj.animationTime;
                    recordedData[objIndex, recordCount] = data;
                }

                //���¼�¼����������
                recordCount++;

                //ȷ��recordCountʼ��С��recordMax-1
                recordCount = Mathf.Min(recordCount, recordMax - 2);
                recordIndex = recordCount;

                //�������пɻ������壬ִ�����ǵ�ʱ����ºͶ�������
                foreach (var timeObject in timeObjects)
                {
                    timeObject.TimeUpdate();
                    timeObject.UpdateAnimation();
                }
            }



        }
    }
}


