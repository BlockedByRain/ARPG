using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionWaypoint : MonoBehaviour
{
    public Image img;
    public Transform target;
    public Text meter;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //���ø���߽�
        //Graphic.GetPixelAdjustedRect ������ӽ� Graphic RectTransform �����ؾ�׼����,
        float minX = img.GetPixelAdjustedRect().width/2;
        float maxX = Screen.width - minX;

        float minY= img.GetPixelAdjustedRect().height/2;
        float maxY= Screen.height - minY;

        Vector2 postion = Camera.main.WorldToScreenPoint(target.position+offset);

        //������Ļֱ���ñ�Ե
        if (Vector3.Dot((target.position-transform.position),transform.forward)<0)
        {
            if (postion.x<Screen.width/2)
            {
                postion.x = maxX;
            }
            else
            {
                postion.x = minX;
            }
        }

        postion.x=Mathf.Clamp(postion.x, minX, maxX);
        postion.y=Mathf.Clamp(postion.y, minY, maxY);

        img.transform.position = postion;
        meter.text=((int)Vector3.Distance(target.position,transform.position)).ToString()+"M";










    }
}
