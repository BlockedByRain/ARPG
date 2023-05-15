using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public LayerMask obstacleMask; // ���Դ������������ڵĲ�

    public Camera mainCamera;
    public Collider cameraCollider;

    private Vector3 previousPosition; // �������һ֡��λ��

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        cameraCollider = GetComponent<Collider>();

        previousPosition = transform.position;
    }

    void LateUpdate()
    {
        // ������������ƶ�����
        Vector3 movement = transform.position - previousPosition;

        // �����ײ
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -movement, out hit, movement.magnitude, obstacleMask))
        {
            // �����������������壬�����ƻ���ײ��
            transform.position = hit.point;
        }

        previousPosition = transform.position;
    }
}
