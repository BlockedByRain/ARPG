using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadtest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(LoadingPanelController.Instance.LoadScene("456"));
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            TipsPanelController.Instance.SetTips("111","111");


            Debug.Log("111"+TipsPanelController.Instance.isOpen);
            TipsPanelController.Instance.Open();
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            TipsPanelController.Instance.SetTips("222","222");
            TipsPanelController.Instance.Open();
        }


    }
}
