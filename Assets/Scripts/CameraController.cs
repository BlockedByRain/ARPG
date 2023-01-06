using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private IUserInput pi;
    public float horizontalSpeed;
    public float verticalSpeed;

    [Header("�����˥���ٶ�")]
    public float cameraDampValue;

    [Header("�����ӽ�����")]
    public int limitEulerX1 = -35;
    public int limitEulerX2 = 30;

    private GameObject playerHandle;
    private GameObject cameraHandle;

    private float tempEulerX;

    private new GameObject camera;
    private GameObject model;

    private Vector3 cameraDampVelocity;



    // Start is called before the first frame update
    void Start()
    {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        ActorController ac = playerHandle.GetComponent<ActorController>();

        camera = Camera.main.gameObject;
        model = ac.model;
        pi = ac.pi;


        camera.transform.position = transform.position;
        camera.transform.eulerAngles = transform.eulerAngles;

    }

    // Update is called once per frame
    void Update()
    {



    }

    private void FixedUpdate()
    {
        //��¼ģ�͵�ǰ����
        Vector3 tempModelEuler = model.transform.eulerAngles;

        playerHandle.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime);

        //cameraHandle.transform.Rotate(Vector3.right,pi.Jup * -verticalSpeed * Time.deltaTime);

        //���������������ת�����Ƕ�
        tempEulerX -= pi.Jup * verticalSpeed * Time.fixedDeltaTime;
        tempEulerX = Mathf.Clamp(tempEulerX, limitEulerX1, limitEulerX2);
        cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);


        //��д����
        model.transform.eulerAngles = tempModelEuler;


        //�����׷��Ч��
        //camera.transform.position=transform.position;
        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, transform.position, ref cameraDampVelocity, cameraDampValue);
        
        //̫��Ӳ���ữ����
        //camera.transform.eulerAngles = transform.eulerAngles;
        camera.transform.LookAt(cameraHandle.transform);

    }



}
