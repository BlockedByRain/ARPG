using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    private IUserInput pi;
    public float horizontalSpeed;
    public float verticalSpeed;

    [Header("摄像机衰减速度")]
    public float cameraDampValue;

    [Header("仰俯视角限制")]
    public int limitEulerX1 = -35;
    public int limitEulerX2 = 30;

    [Header("锁定距离")]
    public float lockDistance = 10.0f;

    public Image lockDot;
    public bool lockState;

    public bool isAI =false;

    private GameObject playerHandle;
    private GameObject cameraHandle;

    private float tempEulerX;

    private new GameObject camera;
    private GameObject model;

    private Vector3 cameraDampVelocity;

    private LockTatget lockTarget;


    // Start is called before the first frame update
    void Start()
    {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        ActorController ac = playerHandle.GetComponent<ActorController>();

        model = ac.model;
        pi = ac.pi;

        if (!isAI)
        {
            camera = Camera.main.gameObject;
            lockDot.enabled = false;
            //隐藏鼠标
            Cursor.lockState = CursorLockMode.Locked;

            //同步位置
            camera.transform.position = transform.position;
            camera.transform.eulerAngles = transform.eulerAngles;
        }


        lockState = false;



    }



    private void FixedUpdate()
    {
        if (lockTarget == null)
        {
            //记录模型当前朝向
            Vector3 tempModelEuler = model.transform.eulerAngles;

            playerHandle.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime);
            //cameraHandle.transform.Rotate(Vector3.right,pi.Jup * -verticalSpeed * Time.deltaTime);

            //限制摄像机上下旋转的最大角度
            tempEulerX -= pi.Jup * verticalSpeed * Time.fixedDeltaTime;
            tempEulerX = Mathf.Clamp(tempEulerX, limitEulerX1, limitEulerX2);
            cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);

            //覆写朝向
            model.transform.eulerAngles = tempModelEuler;
        }
        else
        {
            //锁定点位置
            if (!isAI)
            {
                lockDot.transform.position = Camera.main.WorldToScreenPoint(lockTarget.obj.transform.position);

            }

            Vector3 tempForward = lockTarget.obj.transform.position - model.transform.position;
            tempForward.y = 0;
            playerHandle.transform.forward = tempForward;
            
            //看向脚底
            Vector3 tempLookAt = new Vector3(lockTarget.obj.transform.position.x, lockTarget.obj.transform.position.y - lockTarget.halfHeight, lockTarget.obj.transform.position.z);
            cameraHandle.transform.LookAt(tempLookAt);

            //太远取消锁定
            if (Vector3.Distance(model.transform.position,lockTarget.obj.transform.position)>lockDistance)
            {
                LockProcessA(null, false, false, isAI);

            }
        }


        if (!isAI)
        {
            //摄像机追踪效果
            //camera.transform.position=transform.position;
            camera.transform.position = Vector3.SmoothDamp(camera.transform.position, transform.position, ref cameraDampVelocity, cameraDampValue);

            //太僵硬，柔化防晕
            //camera.transform.eulerAngles = transform.eulerAngles;
            camera.transform.LookAt(cameraHandle.transform);

        }


    }



    public void SwitchLock()
    {
        Vector3 modelOrigin1 = model.transform.position;
        Vector3 modelorigin2 = modelOrigin1 + new Vector3(0, 1, 0);
        Vector3 boxCenter = modelorigin2 + model.transform.forward * 5.0f;

        Collider[] cols = Physics.OverlapBox(boxCenter, new Vector3(0.5f, 0.5f, 5f),
            model.transform.rotation, LayerMask.GetMask(isAI? "Player" : "Enemy"));

        if (cols.Length == 0)
        {
            LockProcessA(null, false, false, isAI);

        }
        else
        {
            foreach (Collider col in cols)
            {
                if (lockTarget!=null && lockTarget.obj == col.gameObject)
                {
                    LockProcessA(null, false, false, isAI);
                    break;
                }
                LockProcessA(new LockTatget(col.gameObject, col.bounds.extents.y), true, true, isAI);

                break;
            }
        }

    }


    private void LockProcessA(LockTatget _lockTatget,bool _lockDotEnable, bool _lockState,bool _isAI)
    {
        lockTarget = _lockTatget;
        if (!isAI)
        {
            lockDot.enabled = _lockDotEnable;
        }
        lockState = _lockState;
    }


    private class LockTatget
    {
        public GameObject obj;
        public float halfHeight;

        public LockTatget(GameObject obj,float halfHeight)
        {
            this.obj = obj;
            this.halfHeight = halfHeight;
        }



    }


}


