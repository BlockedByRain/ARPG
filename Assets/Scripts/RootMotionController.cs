using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionController : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OnAnimatorMove()
    {
        //print("animator move!");
        SendMessageUpwards("OnUpdateRM", (object)anim.deltaPosition);

    }



}
