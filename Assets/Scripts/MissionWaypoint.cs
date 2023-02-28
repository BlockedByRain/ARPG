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
        //设置附表边界
        //Graphic.GetPixelAdjustedRect 返回最接近 Graphic RectTransform 的像素精准矩形,
        float minX = img.GetPixelAdjustedRect().width/2;
        float maxX = Screen.width - minX;

        float minY= img.GetPixelAdjustedRect().height/2;
        float maxY= Screen.height - minY;

        Vector2 postion = Camera.main.WorldToScreenPoint(target.position+offset);

        //超出屏幕直接置边缘
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
