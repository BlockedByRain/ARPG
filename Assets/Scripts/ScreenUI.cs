using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenUI : MonoBehaviour
{
    private Canvas canvas;

    public string textName;

    private Text text;

    string hexColor = "#FFFFFF";

    private MeshRenderer meshRenderer;

    private Transform textTransform;


    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("ScreenUI").GetComponent<Canvas>();


        meshRenderer = gameObject.GetComponent<MeshRenderer>();

        //查找指定名称的子物体
        textTransform = canvas.transform.Find(textName);
        if (textTransform != null)
        {
            //如果找到了同名的子物体，则获取它的 Text 组件
            text = textTransform.GetComponent<Text>();
            textTransform.position = Vector3.zero;
        }
        else
        {
            //如果没有找到同名的子物体，则在 Canvas 上创建一个新的 Text 组件
            GameObject textObject = new GameObject(textName, typeof(Text));
            //将新创建的 Text 对象设置为 Canvas 的子物体
            textObject.transform.SetParent(canvas.transform);
            //获取新创建的 Text 组件
            text = textObject.GetComponent<Text>();

            textTransform= textObject.transform;
        }

        //设置 Text 组件的信息
        text.text = textName;
        text.fontSize = 70;
        text.alignment = TextAnchor.MiddleCenter;
        text.font= Resources.GetBuiltinResource<Font>("Arial.ttf");
        //设置颜色
        Color color;
        if (ColorUtility.TryParseHtmlString(hexColor, out color))
        {
            text.color = color;
        }
        text.color = color;



        //设置 rectTransform 组件的信息
        RectTransform textRectTransform = text.gameObject.GetOrAddComponent<RectTransform>();
        textRectTransform.position = Vector3.zero;
        textRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        textRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        textRectTransform.pivot = new Vector2(0.5f, 0.5f);
        textRectTransform.sizeDelta = new Vector2(300, 300);
    }




    // Update is called once per frame
    void Update()
    {
        //Debug.Log(meshRenderer.isVisible);
        if (meshRenderer.isVisible)
        {
            // 物体在视野范围内，显示UI

            //Debug.Log("物体在视野范围内");
            textTransform.gameObject.SetActive(true);
        }
        else
        {
            // 物体不在视野范围内，隐藏UI

            //Debug.Log("物体不在视野范围内");
            textTransform.gameObject.SetActive(false);
        }


        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        //使用Camera.WorldToViewportPoint()方法将3D世界坐标转换为屏幕视口坐标，判断视口坐标是否在0到1的范围内来判断物体是否在相机的视线内
        if (viewportPos.z > 0 && viewportPos.x > 0 && viewportPos.x < 1 && viewportPos.y > 0 && viewportPos.y < 1)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            text.transform.position = screenPos;
        }
        else
        {
            //在视口坐标z轴小于0时，表示该点在相机后面，此时将text对象隐藏。这样可以保证当物体不在相机视线内时，不会再显示text
            text.gameObject.SetActive(false);
        }


    }

    private void OnDestroy()
    {

        Debug.Log(textTransform.gameObject);
        Destroy(textTransform.gameObject);
    }



}
