using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public LayerMask obstacleMask; // 可以穿过的物体所在的层

    public Camera mainCamera;
    public Collider cameraCollider;

    private Vector3 previousPosition; // 摄像机上一帧的位置

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        cameraCollider = GetComponent<Collider>();

        previousPosition = transform.position;
    }

    void LateUpdate()
    {
        // 计算摄像机的移动向量
        Vector3 movement = transform.position - previousPosition;

        // 检测碰撞
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -movement, out hit, movement.magnitude, obstacleMask))
        {
            // 如果摄像机穿过了物体，将其移回碰撞点
            transform.position = hit.point;
        }

        previousPosition = transform.position;
    }
}
