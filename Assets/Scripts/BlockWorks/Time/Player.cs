using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpVelocity = 40f;
    public Vector2 velocity;

    public  void Update()
    {

        Vector2 pos =transform.position;

        pos.y += velocity.y * Time.deltaTime;
        velocity.y+=-100*Time.deltaTime;

        if (pos.y<1)
        {
            pos.y = 1;
            velocity.y = 0;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            velocity.y = jumpVelocity;
        }

        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= moveSpeed*Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            pos.x += moveSpeed * Time.deltaTime;
        }


        transform.position = pos;

    }
}
