using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsPanelController : MonoSingleton<TipsPanelController>
{
    public GameObject tipsPanel;


    public bool isOpen;

    private Text tipsTitle;
    private Text tips;

    public KeyCode openKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void Init()
    {
        tipsTitle = tipsPanel.transform.Find("TipsTitle").gameObject.GetComponent<Text>();
        tips = tipsPanel.transform.Find("Tips").gameObject.GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(openKey))
        {
            Open();
        }
    }


    public void SetTips(string tipsTitlesText, string tipsText)
    {
        tipsTitle.text = tipsTitlesText;
        tips.text = tipsText;
    }



    public void Close()
    {
        isOpen = false;
        tipsPanel.SetActive(isOpen);
        Cursor.lockState = CursorLockMode.Locked;
    }


    public void Open()
    {
        if (isOpen == false)
        {
            isOpen = true;
            tipsPanel.SetActive(isOpen);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Close();
        }


    }

}
