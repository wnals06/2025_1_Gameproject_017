using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraF : MonoBehaviour
{
    public Transform target;                                //���� ��� (�÷��̾�)
    public Vector3 offset = new Vector3(0, 5, -10);          //�÷��̾�� ������ �Ÿ�
    public float smoothSpeed = 0.25f;                       //  ���󰡴� �ӵ�

    private void LateUpdate()                         //ī�޶� �������� ���� LateUpdate ���� ó��
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);                 //���� ��ġ ����
        transform.position = smoothPosition;                                        //���� ������Ʈ ��ġ�� ����ش�.

        transform.LookAt(transform.position);             //ī�޶� �׻� �÷��̾ �ٶ󺸵��� ����;
    }
}
