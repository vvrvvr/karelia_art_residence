using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
    

public class Section : MonoBehaviour
{
    public float rotationTime = 0.5f; // время в секундах, которое займет поворот
    private bool isRotating = false; // флаг, указывающий, вращается ли объект уже
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

        // используем метод RotateAround для поворота объекта с помощью DOTween
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
