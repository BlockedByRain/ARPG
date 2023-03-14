using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControlled : MonoBehaviour
{
    public Vector2 velocity; //������ٶ�����

    public AnimationClip currentAnimation; //��ǰ���ŵĶ���

    public float animationTime; //�����Ѿ����ŵ�ʱ��

    private Vector3 lastPosition;

    private Animator animator;

    void Start()
    {
        lastPosition = transform.position;

        animator = GetComponent<Animator>();

    }

    public void Update()
    {
        if (TimeController.Instance.state==TimeController.TIMESTATE.pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }


        


        // �����ٶ�
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;

        // ��ȡ����Ƭ��
        if (animator!=null && animator.enabled)
        {
            AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0); // 0����layerIndex��һ��Ϊ0����
            if (clipInfo.Length > 0)
            {
                AnimationClip currentAnimation = clipInfo[0].clip;
            }
        }






    }


    //ʱ�����
    public virtual void TimeUpdate()
    {
        //�����ǰ�ж������ڲ���
        if (currentAnimation != null)
        {
            //���Ӷ����Ѿ����ŵ�ʱ��
            animationTime += Time.deltaTime;

            //�������ʱ�䳬���˶����ĳ��ȣ���ʱ�����
            if (animationTime > currentAnimation.length)
            {
                animationTime = animationTime - currentAnimation.length;
            }
        }

    }

    //��������
    public void UpdateAnimation()
    {
        //�����ǰ�ж������ڲ���
        if (currentAnimation != null)
        {
            //���ݶ����Ѿ����ŵ�ʱ�䣬���¶������Ž���
            currentAnimation.SampleAnimation(gameObject, animationTime);
        }

    }



}
