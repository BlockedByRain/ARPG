using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour
{
    public float offset = 0.1f;
    public CapsuleCollider capsuleCollider;

    //½ºÄÒ¼ì²âµã
    private Vector3 point1;
    private Vector3 point2;
    private float radius;



    private void Awake()
    {
        radius = capsuleCollider.radius-0.05f;
        //Debug.Log(radius);



    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        point1 = transform.position + transform.up * (radius-offset);
        point2 = transform.position + transform.up * (capsuleCollider.height-offset) - transform.up * radius;

        Collider[] outputCols = Physics.OverlapCapsule(point1, point2, radius,LayerMask.GetMask("Ground"));

        if (outputCols.Length!=0)
        {
            //foreach (var col in outputCols)
            //{
            //    Debug.Log("collider: "+ col.name);
            //}

            SendMessageUpwards("IsGround");

        }
        else
        {
            SendMessageUpwards("IsNotGround");
        }
    }


}
