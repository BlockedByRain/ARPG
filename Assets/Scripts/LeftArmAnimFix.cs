using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArmAnimFix : MonoBehaviour
{
    private Animator anim;

    [SerializeField]
    public Vector3 a;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    private void OnAnimatorIK(int layerIndex)
    {
        //不在防御姿态时调整手部
        if (anim.GetBool("Defense")==false)
        {
            Transform leftLowArm = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
            leftLowArm.localEulerAngles += 0.75f * a;
            anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(leftLowArm.localEulerAngles));

        }



    }

}
