using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Uduino;

public class Dot : MonoBehaviour
{
    [SerializeField] ArduinoManager _arduinoManager;
    [SerializeField] private LayerMask wallLayer;
    [Space]
    [SerializeField] private Text _textVert;
    [SerializeField] private Slider _sliderVert;
    [Space]
    [SerializeField] private Text _textHor;
    [SerializeField] private Slider _sliderHor;
    [SerializeField] private float factorX = 0.5f;
    [SerializeField] private float factorY = 0.3f;
    private float halfDotDimention = 0.5f;

    public bool hasControl = false;
    public bool isFirstLaunch = true; //нужно, чтобы точка обнуляла значения и не прескакивала за потенциометром, когда нет управления

    //vertical input
    private int verticallInput = 0;
    private int prevVerticalInput;
    private int verticalValue = 0;
    //horizontal input
    private int horizontalInput = 0;
    private int prevhorizontalInput;
    private int horizontalValue = 0;

    private void OnEnable()
    {
        isFirstLaunch = true;
    }
   

    void Start()
    {

    }

    void Update()
    {
        if (hasControl)
        {
            verticalValue = 0;
            horizontalValue = 0;

            _textVert.text = "vertical = " + verticallInput;
            _sliderVert.value = (float)verticallInput / 1000.0f;

            _textHor.text = "horizontal = " + horizontalInput;
            _sliderHor.value = (float)horizontalInput / 1000.0f;

            verticallInput = _arduinoManager.VerticalControl;
            verticalValue = verticallInput - prevVerticalInput; //величина перемещения
            if(isFirstLaunch)
            {
                verticalValue = 0;
                //isFirstLaunch = false;
            }
            if (verticalValue > 0)
                MoveUpDown(Vector3.up, verticalValue);
            if (verticalValue < 0)
                MoveUpDown(Vector3.down, verticalValue);

            horizontalInput = _arduinoManager.HorizontalControl;
            horizontalValue = horizontalInput - prevhorizontalInput; //величина перемещения
            if(isFirstLaunch)
            {
                horizontalValue = 0;
                isFirstLaunch = false; //отключаем здесь, чтобы сработало в двух условиях
            }
            if (horizontalValue > 0)
                MoveRightLeft(Vector3.right, horizontalValue);
            if (horizontalValue < 0)
                MoveRightLeft(Vector3.left, horizontalValue);


            prevVerticalInput = verticallInput;
            prevhorizontalInput = horizontalInput;
        }
    }

    private void MoveRightLeft(Vector3 dir, int moveVal)
    {
        Vector3 newPos = transform.position + dir * Time.deltaTime * moveVal * factorX;
        float dirLength = Mathf.Abs(newPos.x - transform.position.x);
        Vector3 upRayPoint = new Vector3(transform.position.x, transform.position.y + halfDotDimention - 0.01f, transform.position.z);
        Vector3 downRayPoint = new Vector3(transform.position.x, transform.position.y - halfDotDimention + 0.01f, transform.position.z);
        RaycastHit hit;

        if (Physics.Raycast(upRayPoint, dir, out hit, dirLength, wallLayer))
        {
            var point = new Vector3(0f, 0f, 0f);
            switch (dir)
            {
                case Vector3 v when v.Equals(Vector3.right):
                    point = hit.point - new Vector3(halfDotDimention, 0f, 0f);
                    point = new Vector3(point.x, transform.position.y, point.z);
                    transform.position = point;
                    break;
                case Vector3 v when v.Equals(Vector3.left):
                    point = hit.point + new Vector3(halfDotDimention, 0f, 0f);
                    point = new Vector3(point.x, transform.position.y, point.z);
                    transform.position = point;
                    break;
            }
        }
        else if (Physics.Raycast(downRayPoint, dir, out hit, dirLength, wallLayer))
        {
            var point = new Vector3(0f, 0f, 0f);
            switch (dir)
            {
                case Vector3 v when v.Equals(Vector3.right):
                    point = hit.point - new Vector3(halfDotDimention, 0f, 0f);
                    point = new Vector3(point.x, transform.position.y, point.z);
                    transform.position = point;
                    break;
                case Vector3 v when v.Equals(Vector3.left):
                    point = hit.point + new Vector3(halfDotDimention, 0f, 0f);
                    point = new Vector3(point.x, transform.position.y, point.z);
                    transform.position = point;
                    break;
            }
        }
        else
        {
            switch (dir)
            {
                case Vector3 v when v.Equals(Vector3.right):
                    transform.position += dir * Time.deltaTime * moveVal * factorX;
                    break;
                case Vector3 v when v.Equals(Vector3.left):
                    transform.position -= dir * Time.deltaTime * moveVal * factorX;
                    break;
            }
        }

    }

    private void MoveUpDown(Vector3 dir, int moveVal)
    {
        Vector3 newPos = transform.position + dir * Time.deltaTime * moveVal * factorY;
        float dirLength = Mathf.Abs(newPos.y - transform.position.y);
        Vector3 leftRayPoint = new Vector3(transform.position.x - halfDotDimention + 0.01f, transform.position.y, transform.position.z);
        Vector3 rightRayPoint = new Vector3(transform.position.x + halfDotDimention - 0.01f, transform.position.y, transform.position.z);
        RaycastHit hit;

        if (Physics.Raycast(leftRayPoint, dir, out hit, dirLength, wallLayer))
        {
            var point = new Vector3(0f, 0f, 0f);
            switch (dir)
            {
                case Vector3 v when v.Equals(Vector3.up):
                    point = hit.point - new Vector3(halfDotDimention, 0f, 0f);
                    point = new Vector3(transform.position.x, point.y, point.z);
                    transform.position = point;
                    break;
                case Vector3 v when v.Equals(Vector3.down):
                    point = hit.point + new Vector3(halfDotDimention, 0f, 0f);
                    point = new Vector3(transform.position.x, point.y, point.z);
                    transform.position = point;
                    break;
            }
        }
        else if (Physics.Raycast(rightRayPoint, dir, out hit, dirLength, wallLayer))
        {
            var point = new Vector3(0f, 0f, 0f);
            switch (dir)
            {
                case Vector3 v when v.Equals(Vector3.up):
                    point = hit.point - new Vector3(halfDotDimention, 0f, 0f);
                    point = new Vector3(transform.position.x, point.y, point.z);
                    transform.position = point;
                    break;
                case Vector3 v when v.Equals(Vector3.down):
                    point = hit.point + new Vector3(halfDotDimention, 0f, 0f);
                    point = new Vector3(transform.position.x, point.y, point.z);
                    transform.position = point;
                    break;
            }
        }
        else
        {
            switch (dir)
            {
                case Vector3 v when v.Equals(Vector3.up):
                    transform.position += dir * Time.deltaTime * moveVal * factorY;
                    break;
                case Vector3 v when v.Equals(Vector3.down):
                    transform.position -= dir * Time.deltaTime * moveVal * factorY;
                    break;
            }
        }

    }

}
