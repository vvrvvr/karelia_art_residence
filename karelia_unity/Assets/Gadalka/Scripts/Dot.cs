using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class Dot : MonoBehaviour
{
    [SerializeField] ArduinoManager _arduinoManager;
    private Transform _transform;
    [SerializeField] private LayerMask wallLayer;
    [Space]
    public float factorX;
    public float factorY;
    private float halfDotDimention = 0.5f;

    public bool hasControl = false;
    public bool isFirstLaunch = true; //нужно, чтобы точка обнуляла значения и не прескакивала за потенциометром, когда нет управления

    //vertical input
    private float verticallInput = 0;
    private float prevVerticalInput;
    private float verticalValue = 0;

    //horizontal input
    private float horizontalInput = 0f;
    private float prevhorizontalInput;
    private float horizontalValue = 0;


    private void OnEnable()
    {
        isFirstLaunch = true;
    }


    void Start()
    {
        //factorX = 0.5f;
        //factorY = 0.3f;
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        if (hasControl)
        {
            verticalValue = 0;
            horizontalValue = 0;

            verticallInput = _arduinoManager.VerticalInput;
            verticalValue = verticallInput - prevVerticalInput; //величина перемещения
            if (isFirstLaunch)
            {
                verticalValue = 0;
                //isFirstLaunch = false;
            }
            if (verticalValue > 0)
            {
                MoveUpDown(Vector3.up, verticalValue);
            }
            if (verticalValue < 0)
            {
                MoveUpDown(Vector3.down, verticalValue);
            }


            horizontalInput = _arduinoManager.HorizontalInput;
            horizontalValue = horizontalInput - prevhorizontalInput;

            if (horizontalValue > 0.1f)
            {
                horizontalValue = prevhorizontalInput;
                Debug.Log("щелчок");
            }
            //величина перемещения
            if (isFirstLaunch)
            {
                horizontalValue = 0;
                isFirstLaunch = false; //отключаем здесь, чтобы сработало в двух условиях
            }
            if (horizontalValue > 0)
            {
                MoveRightLeft(Vector3.right, horizontalValue);
                
            }

            if (horizontalValue < 0)
            {
                MoveRightLeft(Vector3.left, horizontalValue); 
            }



            prevVerticalInput = verticallInput;
            prevhorizontalInput = horizontalInput;
        }
    }

    private void MoveRightLeft(Vector3 dir, float moveVal)
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

    private void MoveUpDown(Vector3 dir, float moveVal)
    {
        Vector3 newPos = transform.position + dir * Time.deltaTime * moveVal * factorY;
        float dirLength = Mathf.Abs(newPos.y - transform.position.y);
        Vector3 rightRayPoint = new Vector3(transform.position.x + halfDotDimention - 0.01f, transform.position.y, transform.position.z);
        Vector3 leftRayPoint = new Vector3(transform.position.x - halfDotDimention + 0.01f, transform.position.y, transform.position.z);
        RaycastHit hit;

        if (Physics.Raycast(rightRayPoint, dir, out hit, dirLength, wallLayer))
        {
            
            switch (dir)
            {
                case Vector3 v when v.Equals(Vector3.up):
                    var pointY = hit.point.y - halfDotDimention;
                    var currentPointUp = new Vector3(transform.position.x, pointY, transform.position.z);
                    transform.position = currentPointUp;
                    break;
                case Vector3 v when v.Equals(Vector3.down):
                    var pointY2 = hit.point.y + halfDotDimention;
                    var currentPointDown = new Vector3(transform.position.x, pointY2, transform.position.z);
                    transform.position = currentPointDown;
                    break;
            }
        }
        else if (Physics.Raycast(leftRayPoint, dir, out hit, dirLength, wallLayer))
        {
            switch (dir)
            {
                case Vector3 v when v.Equals(Vector3.up):
                    var pointY = hit.point.y - halfDotDimention;
                    var currentPointUp = new Vector3(transform.position.x, pointY, transform.position.z);
                    transform.position = currentPointUp;
                    break;
                case Vector3 v when v.Equals(Vector3.down):
                    var pointY2 = hit.point.y + halfDotDimention;
                    var currentPointDown = new Vector3(transform.position.x, pointY2, transform.position.z);
                    transform.position = currentPointDown;
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
