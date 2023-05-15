using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void ReturnTitle()
    {

        Debug.Log("end");
        StartCoroutine(LoadingPanelController.Instance.LoadScene("Title"));
    }




}
