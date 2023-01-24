using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArmAnimFix : MonoBehaviour
{
    private Animator anim;
    private YbotController ybotController;


    [SerializeField]
    public Vector3 a;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        ybotController = GetComponentInParent<YbotController>();
    }


    private void OnAnimatorIK(int layerIndex)
    {
        //�ֶ�ʱ����
        if (ybotController.leftIsShield)
        {
            //���ڷ�����̬ʱ�����ֲ�
            if (anim.GetBool("Defense") == false)
            {
                Transform leftLowArm = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
                leftLowArm.localEulerAngles += 0.75f * a;
                anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(leftLowArm.localEulerAngles));

            }
        }





    }

}
