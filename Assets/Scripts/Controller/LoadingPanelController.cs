using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingPanelController : MonoSingleton<LoadingPanelController>
{
    public Slider slider;

    //Ŀ��ֵ
    public float targetValue;
    //��ǰֵ
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
        //��ֵ����Value
        slider.value=Mathf.MoveTowards(slider.value, targetValue, speed*Time.deltaTime);

        //ʹ��
        //LoadingPanelController.Instance.Init();
        //StartCoroutine(LoadingPanelController.Instance.LoadScene());


    }
    /// <summary>
    /// �첽���ط�����
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadScene()
    {
        yield return null;

        //�����첽�����Ҳ���ʾ
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("");
        asyncOperation.allowSceneActivation = false;

        //�ȴ�����
        while (asyncOperation.isDone)
        {
            LoadingPanelController.Instance.SetPercent(asyncOperation.progress);

            if (asyncOperation.progress >= 0.9f)
            {
                break;
            }

            yield return null;

        }

        //���������
        LoadingPanelController.Instance.SetPercent(1f);
        while (LoadingPanelController.Instance.CurrentValue<1)
        {
            yield return null;
        }

        //��ʾ����
        asyncOperation.allowSceneActivation = true;

    }






}
