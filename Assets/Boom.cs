using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private Vector3 pos;
    public float radius = 5.0f;
    public float firepower = 10;
    public float upwardpower = 5;

    public ForceMode boomForcemode = ForceMode.Impulse;




    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryBoom();

        }
        

    }

    private void TryBoom()
    {
        Debug.Log("11");
        Collider[] colliders = Physics.OverlapSphere(pos, radius);
        foreach (Collider col in colliders)
        {

            if (col.GetComponent<Rigidbody>())
            {
                col.GetComponent<Rigidbody>().AddExplosionForce(firepower, pos, radius, upwardpower, boomForcemode);
            }
            else
            {

                
                continue;
            }
        }


    }




}
