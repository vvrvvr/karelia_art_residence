using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CamRotateManager : MonoBehaviour
{
    [SerializeField] ArduinoManager _arduinoManager;
    [SerializeField] private Transform _camAnchor;
    [SerializeField] private float _rotateSpeed;
    

    public bool hasControl = true;
    public bool isFirstLaunch = true;
    //vertical input
    private float verticallInput = 0;
    private float prevVerticalInput;
    private float verticalValue = 0;

    //horizontal input
    private float horizontalInput = 0f;
    private float prevhorizontalInput;
    private float horizontalValue = 0;

    private Vector3 currentRotation;

    void Start()
    {
        
    }
    private void OnEnable()
    {
        isFirstLaunch = true;
    }

    void Update()
    {
        if (hasControl)
        {
            verticalValue = 0;
            horizontalValue = 0;
            currentRotation = _camAnchor.transform.rotation.eulerAngles;


            horizontalInput = _arduinoManager.HorizontalInput;
            horizontalValue = horizontalInput - prevhorizontalInput;

            if (horizontalValue > 0.1f)
            {
                horizontalValue = prevhorizontalInput;
                //Debug.Log("щелчок");
            }
            //величина перемещения
            if (isFirstLaunch)
            {
                horizontalValue = 0;
                isFirstLaunch = false; //отключаем здесь, чтобы сработало в двух условиях
            }
            if(Mathf.Abs(horizontalValue) > 0)
                RotateHorizontal(horizontalValue, _rotateSpeed);

            verticallInput = _arduinoManager.VerticalInput;
            verticalValue = verticallInput - prevVerticalInput;

            if (verticalValue > 0.1f)
            {
                verticalValue = prevVerticalInput;
                //Debug.Log("щелчок");
            }
            //величина перемещения
            if (isFirstLaunch)
            {
                verticalValue = 0;
                isFirstLaunch = false; //отключаем здесь, чтобы сработало в двух условиях
            }
            if (Mathf.Abs(verticalValue) > 0)
                RotateVertical(verticalValue, _rotateSpeed);



            prevVerticalInput = verticallInput;
            prevhorizontalInput = horizontalInput;
        }
    }
    private void RotateHorizontal(float rotateValue, float rotateSpeed)
    {
        // получаем текущий поворот объекта по оси Y
        //Vector3 currentRotation = _camAnchor.transform.rotation.eulerAngles;
        float newYRotation = currentRotation.y + (rotateValue * rotateSpeed);

        // поворачиваем объект на новый угол
        _camAnchor.transform.rotation = Quaternion.Euler(currentRotation.x, newYRotation, currentRotation.z);
    }
    private void RotateVertical(float rotateValue, float rotateSpeed)
    {
        // получаем текущий поворот объекта по оси Y
        //Vector3 currentRotation = _camAnchor.transform.rotation.eulerAngles;
        float newXRotation = currentRotation.x + (rotateValue * rotateSpeed);
        // Создаем твин для поворота объекта
        _camAnchor.transform.rotation = Quaternion.Euler(newXRotation, currentRotation.y, currentRotation.z);
    }
}
