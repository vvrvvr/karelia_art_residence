using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
    

public class Section : MonoBehaviour
{
    public float rotationTime = 0.5f; // ����� � ��������, ������� ������ �������
    private bool isRotating = false; // ����, �����������, ��������� �� ������ ���
    private EventManager eventManager;

    private void Start()
    {
        eventManager = FindObjectOfType<EventManager>();
        eventManager.OnRotate += StartRotate;
    }

    private void OnDestroy()
    {
        eventManager.OnRotate -= StartRotate;
    }

    private void Rotate()
    {
        isRotating = true;

        // ���������� ����� RotateAround ��� �������� ������� � ������� DOTween
        transform.DORotate(new Vector3(0, 0, transform.rotation.eulerAngles.z - 90), rotationTime)
            .OnComplete(() => { isRotating = false; });
    }
    private void StartRotate()
    {
        if(!isRotating)
        {
            Rotate();
        }
    }
}
