using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleController : MonoBehaviour
{
    public string[] sceneArr;
    public string mainScene;
    private int curSceneIndex;
    //单独场景持续时间
    public float duration = 7.0f;

    public GameObject startButton;


    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(SceneManager.GetActiveScene().path);

        curSceneIndex = 0;
        SceneManager.LoadSceneAsync(sceneArr[0], LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(timer);


        if (timer < duration)
        {
            timer += Time.deltaTime;
        }
        else if (timer > duration)
        {
            timer = 0;
            int nextIndex = GetNextScene();
            SwitchTitleScene(sceneArr[nextIndex]);
            curSceneIndex= nextIndex;
        }





    }




    private void SwitchTitleScene(string scene)
    {

        //Debug.Log("切换场景");


        CloseCurScene();
        SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);

    }




    private void CloseCurScene()
    {
        //Debug.Log(sceneArr[curSceneIndex]);
        SceneManager.UnloadSceneAsync(sceneArr[curSceneIndex]);
    }

    private int GetNextScene()
    {
        //Debug.Log("curSceneIndex=" + curSceneIndex);
        //Debug.Log("sceneArr.Length=" + sceneArr.Length);

        if (sceneArr.Length - 1 >= curSceneIndex+1)
        {
            return curSceneIndex+1;
        }
        else
            return 0;
    }


    public void StartGame()
    {
        Destroy(startButton);

        //加载动画
        StartCoroutine(LoadingPanelController.Instance.LoadScene("Main"));


        //加载动画
        //LoadingPanelController.Instance.LoadScene("Main");
        //SceneManager.LoadSceneAsync(mainScene, LoadSceneMode.Single);


    
    }



}
