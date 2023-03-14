using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMove : MonoBehaviour
{
    public void Update()
    {
        Vector2 pos=transform.position;
        pos.x+=1*Time.deltaTime;
        transform.position=pos;



    }



}
