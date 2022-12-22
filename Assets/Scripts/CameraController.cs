using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerInput pi;
    public float horizontalSpeed;
    public float verticalSpeed;

    [Header("摄像机衰减速度")]
    public float cameraDampValue;

    [Header("仰俯视角限制")]
    public int limitEulerX1 = -35;
    public int limitEulerX2 = 30;

    private GameObject playerHandle;
    private GameObject cameraHandle;

    private float tempEulerX;

    private GameObject camera;
    private GameObject model;

    private Vector3 cameraDampVelocity;


    private void Awake()
    {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
    
        camera = Camera.main.gameObject;
        model=playerHandle.GetComponent<ActorController>().model;


        camera.transform.position=transform.position;
        camera.transform.eulerAngles = transform.eulerAngles;

    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



    }

    private void FixedUpdate()
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


        //摄像机追踪效果
        //camera.transform.position=transform.position;
        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, transform.position, ref cameraDampVelocity, cameraDampValue);
        camera.transform.eulerAngles = transform.eulerAngles;


    }



}
