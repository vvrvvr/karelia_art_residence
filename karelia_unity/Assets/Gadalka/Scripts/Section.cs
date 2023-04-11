using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
    

public class Section : MonoBehaviour
{
    public float rotationTime = 0.5f; // ����� � ��������, ������� ������ �������
    private bool isRotating = false; // ����, �����������, ��������� �� ������ ���
    private EventManager eventManager;
    [SerializeField] private Transform sprites;
    [SerializeField] private Transform collidersTriggers;
    [SerializeField] private Transform colliders;
    [SerializeField] private CheckColliders checkColliders;
   

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
        sprites.DORotate(new Vector3(0, 0, sprites.rotation.eulerAngles.z - 90), rotationTime)
            .OnComplete(() => { isRotating = false; });
        collidersTriggers.DORotate(new Vector3(0, 0, collidersTriggers.rotation.eulerAngles.z - 90), 0)
            .OnComplete(() => { checkColliders.CheckCollision(); });
        colliders.DORotate(new Vector3(0, 0, colliders.rotation.eulerAngles.z - 90), 0);
    }
    private void StartRotate()
    {
        if(!isRotating)
        {
            Rotate();
        }
    }
}
