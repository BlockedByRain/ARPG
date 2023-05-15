using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroductionController : MonoBehaviour
{
    public GameObject introductionPanel;

    public bool isOpen;

    public KeyCode openKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(openKey))
        {
            Open();
        }

    }





    public void Close()
    {
        isOpen = false;
        introductionPanel.SetActive(isOpen);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Open()
    {
        if (isOpen == false)
        {
            isOpen = true;
            introductionPanel.SetActive(isOpen);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Close();
        }


    }

    private void UpdateCursor()
    {
        if (isOpen)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else if (!isOpen)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    //Ë¢ÐÂ³¡¾°
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //SceneManager.LoadScene("Title");
        
    }


}
