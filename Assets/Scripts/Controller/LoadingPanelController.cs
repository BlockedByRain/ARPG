using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingPanelController : MonoSingleton<LoadingPanelController>
{
    public Slider slider;

    //目标值
    public float targetValue;
    //当前值
    public float CurrentValue => slider.value;
    private readonly float speed = 1f;



    protected override void Init()
    {
        slider = (Slider)Resources.Load("");

        gameObject.SetActive(true);
        slider.value = 0;
    }

    public void SetPercent(float percent)
    {
        targetValue = percent;
    }


    void Update()
    {
        //插值进度Value
        slider.value=Mathf.MoveTowards(slider.value, targetValue, speed*Time.deltaTime);

        //使用
        //LoadingPanelController.Instance.Init();
        //StartCoroutine(LoadingPanelController.Instance.LoadScene());


    }
    /// <summary>
    /// 异步加载防卡顿
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadScene()
    {
        yield return null;

        //启动异步加载且不显示
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("");
        asyncOperation.allowSceneActivation = false;

        //等待加载
        while (asyncOperation.isDone)
        {
            LoadingPanelController.Instance.SetPercent(asyncOperation.progress);

            if (asyncOperation.progress >= 0.9f)
            {
                break;
            }

            yield return null;

        }

        //处理进度条
        LoadingPanelController.Instance.SetPercent(1f);
        while (LoadingPanelController.Instance.CurrentValue<1)
        {
            yield return null;
        }

        //显示场景
        asyncOperation.allowSceneActivation = true;

    }






}
