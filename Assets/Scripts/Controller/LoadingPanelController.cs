using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingPanelController : MonoSingleton<LoadingPanelController>
{
    public GameObject LoadingPanel;

    //目标值
    public float targetValue;
    //当前值
    public float CurrentValue => slider.value;
    private readonly float speed = 1f;

    private Slider slider;

    public string targetScene;

    private bool isLoad;


    protected override void Init()
    {
        gameObject.SetActive(true);
        slider = LoadingPanel.transform.Find("LoadingBar").GetComponent<Slider>();
        slider.value = 0;



    }

    public void SetPercent(float percent)
    {
        targetValue = percent;
    }


    private void Start()
    {

        Instance.Init();




    }

    void Update()
    {
        if (isLoad)
        {
            //插值进度Value
            slider.value = Mathf.MoveTowards(slider.value, targetValue, speed * Time.deltaTime);

        }


        //使用样例
        //StartCoroutine(LoadingPanelController.Instance.LoadScene(targetScene));


    }
    /// <summary>
    /// 异步加载防卡顿
    /// </summary>
    /// <returns></returns>
    public IEnumerator LoadScene(string targetScene)
    {
        yield return null;
        isLoad = true;
        //if (LoadingPanel)
        //{
        //    LoadingPanel.SetActive(true);
        //}

        //LoadingPanel.SetActive(true);
        //启动异步加载且不显示
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(targetScene);
        asyncOperation.allowSceneActivation = false;

        //等待加载
        while (asyncOperation.isDone)
        {
            Instance.SetPercent(asyncOperation.progress);

            if (asyncOperation.progress >= 0.9f)
            {
                break;
            }

            yield return null;

        }

        //处理进度条
        Instance.SetPercent(1f);
        while (Instance.CurrentValue<1)
        {
            yield return null;
        }


        //LoadingPanel.SetActive(false);
        isLoad = false;

        //显示场景
        asyncOperation.allowSceneActivation = true;

    }






}
