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

        //����ָ�����Ƶ�������
        textTransform = canvas.transform.Find(textName);
        if (textTransform != null)
        {
            //����ҵ���ͬ���������壬���ȡ���� Text ���
            text = textTransform.GetComponent<Text>();
            textTransform.position = Vector3.zero;
        }
        else
        {
            //���û���ҵ�ͬ���������壬���� Canvas �ϴ���һ���µ� Text ���
            GameObject textObject = new GameObject(textName, typeof(Text));
            //���´����� Text ��������Ϊ Canvas ��������
            textObject.transform.SetParent(canvas.transform);
            //��ȡ�´����� Text ���
            text = textObject.GetComponent<Text>();

            textTransform= textObject.transform;
        }

        //���� Text �������Ϣ
        text.text = textName;
        text.fontSize = 70;
        text.alignment = TextAnchor.MiddleCenter;
        text.font= Resources.GetBuiltinResource<Font>("Arial.ttf");
        //������ɫ
        Color color;
        if (ColorUtility.TryParseHtmlString(hexColor, out color))
        {
            text.color = color;
        }
        text.color = color;



        //���� rectTransform �������Ϣ
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
            // ��������Ұ��Χ�ڣ���ʾUI

            //Debug.Log("��������Ұ��Χ��");
            textTransform.gameObject.SetActive(true);
        }
        else
        {
            // ���岻����Ұ��Χ�ڣ�����UI

            //Debug.Log("���岻����Ұ��Χ��");
            textTransform.gameObject.SetActive(false);
        }


        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        //ʹ��Camera.WorldToViewportPoint()������3D��������ת��Ϊ��Ļ�ӿ����꣬�ж��ӿ������Ƿ���0��1�ķ�Χ�����ж������Ƿ��������������
        if (viewportPos.z > 0 && viewportPos.x > 0 && viewportPos.x < 1 && viewportPos.y > 0 && viewportPos.y < 1)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            text.transform.position = screenPos;
        }
        else
        {
            //���ӿ�����z��С��0ʱ����ʾ�õ���������棬��ʱ��text�������ء��������Ա�֤�����岻�����������ʱ����������ʾtext
            text.gameObject.SetActive(false);
        }


    }

    private void OnDestroy()
    {

        Debug.Log(textTransform.gameObject);
        Destroy(textTransform.gameObject);
    }



}
