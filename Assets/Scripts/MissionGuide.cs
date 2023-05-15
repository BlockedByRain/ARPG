using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionGuide : MonoBehaviour
{
    //����
    public GUIDEMODEL model;

    //�켣�ڵ�
    public Transform[] Targets;

    public LineRenderer lineRenderer;
    public TrailRenderer trailRenderer;

    WaitForSeconds waitTime = new WaitForSeconds(1 / 30f);

    public float trailSpeed=1;

    int index = 1;

    // Start is called before the first frame update
    void Start()
    {
        InitTrailRenderer();
        StartCoroutine(DelayForKeepUpdate());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator DelayForKeepUpdate()
    {
        while (true)
        {
            yield return waitTime;

            switch (model)
            {
                case GUIDEMODEL.line:
                    UpdateLineRenderer();
                    break;
                case GUIDEMODEL.trail:


                    //��ǰĿ�귽������ȷ���Ƿ񵽴�
                    Vector3 currentTagetDirection;

                    //��¼�ڵ�
                    Vector3[] trailPoints = new Vector3[Targets.Length];
                    for (int i = 0; i < Targets.Length; i++)
                    {
                        trailPoints[i] = Targets[i].position;
                        trailPoints[i].y = 0.1f;
                    }

                    //���㵱ǰ����
                    Vector3 currentTaget = trailPoints[index];
                    currentTagetDirection = (currentTaget - trailRenderer.transform.position).normalized;

                    Vector3 nextPos = trailRenderer.transform.position + currentTagetDirection * trailSpeed;
                    Vector3 nextTargetDirection = (currentTaget - nextPos).normalized;


                    //ȷ���Ƿ񵽴�Ŀ���
                    if (currentTagetDirection != nextTargetDirection)
                    {
                        nextPos = currentTaget;
                        index++;
                    }

                    trailRenderer.transform.position = nextPos;

                    if (index >= Targets.Length)
                    {
                        index = 1;

                        yield return new WaitForSeconds(trailRenderer.time);

                        trailRenderer.emitting = false;
                        trailRenderer.transform.position = trailPoints[0];
                        trailRenderer.Clear();
                        trailRenderer.emitting = true;
                    }



                    break;



            }


        }


    }


    private void UpdateLineRenderer()
    {
        //��¼�ڵ�
        Vector3[] linePoints = new Vector3[Targets.Length];
        for (int i = 0; i < Targets.Length; i++)
        {
            linePoints[i] = Targets[i].position;
            linePoints[i].y = 0.1f;
        }

        //���½ڵ�
        lineRenderer.positionCount = linePoints.Length;
        lineRenderer.SetPositions(linePoints);

    }


    




    private void InitTrailRenderer()
    {
        //���õ���һ���ڵ�
        if (Targets!=null)
        {
            trailRenderer.transform.position = Targets[0].position;
        }
    }


}


public enum GUIDEMODEL
{
    line,trail
}
