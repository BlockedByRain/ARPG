using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControlled : MonoBehaviour
{
    public Vector2 velocity; //物体的速度向量

    public AnimationClip currentAnimation; //当前播放的动画

    public float animationTime; //动画已经播放的时间

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


        


        // 计算速度
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;

        // 获取动画片段
        if (animator!=null && animator.enabled)
        {
            AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0); // 0代表layerIndex，一般为0即可
            if (clipInfo.Length > 0)
            {
                AnimationClip currentAnimation = clipInfo[0].clip;
            }
        }






    }


    //时间更新
    public virtual void TimeUpdate()
    {
        //如果当前有动画正在播放
        if (currentAnimation != null)
        {
            //增加动画已经播放的时间
            animationTime += Time.deltaTime;

            //如果动画时间超过了动画的长度，将时间归零
            if (animationTime > currentAnimation.length)
            {
                animationTime = animationTime - currentAnimation.length;
            }
        }

    }

    //动画更新
    public void UpdateAnimation()
    {
        //如果当前有动画正在播放
        if (currentAnimation != null)
        {
            //根据动画已经播放的时间，更新动画播放进度
            currentAnimation.SampleAnimation(gameObject, animationTime);
        }

    }



}
