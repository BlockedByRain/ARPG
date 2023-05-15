using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingPanelController : MonoSingleton<LoadingPanelController>
{
    public GameObject LoadingPanel;

    //Ŀ��ֵ
    public float targetValue;
    //��ǰֵ
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
            //��ֵ����Value
            slider.value = Mathf.MoveTowards(slider.value, targetValue, speed * Time.deltaTime);

        }


        //ʹ������
        //StartCoroutine(LoadingPanelController.Instance.LoadScene(targetScene));


    }
    /// <summary>
    /// �첽���ط�����
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
        //�����첽�����Ҳ���ʾ
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(targetScene);
        asyncOperation.allowSceneActivation = false;

        //�ȴ�����
        while (asyncOperation.isDone)
        {
            Instance.SetPercent(asyncOperation.progress);

            if (asyncOperation.progress >= 0.9f)
            {
                break;
            }

            yield return null;

        }

        //���������
        Instance.SetPercent(1f);
        while (Instance.CurrentValue<1)
        {
            yield return null;
        }


        //LoadingPanel.SetActive(false);
        isLoad = false;

        //��ʾ����
        asyncOperation.allowSceneActivation = true;

    }






}
