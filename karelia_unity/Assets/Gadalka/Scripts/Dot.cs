using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dot : MonoBehaviour
{
    public bool hasControl = true;
    public Text _text;
    public Slider _slider;
    private int currentInt = 0;
    private int prevInt;
    public int speed = 1;
    private int moveValue = 0;
    public float factor = 1.1f;
    [SerializeField] private LayerMask wallLayer;
    private float halfDotDimention = 0.5f;

    void Start()
    {
        //currentInt = Random.Range(0, 1024);
        prevInt = currentInt;
    }

    void Update()
    {
        if (hasControl)
        {

            moveValue = 0;
            _text.text = "value = " + currentInt;
            _slider.value = (float)currentInt / 1000.0f;

            var hor = Input.GetAxisRaw("Horizontal");
            currentInt += (int)hor * speed;
            if (currentInt > 1024)
            {
                currentInt = 1024;
            }
            else if (currentInt < 0)
            {
                currentInt = 0;
            }

            moveValue = currentInt - prevInt;

            if (moveValue > 0)
            {
                MoveUpDown(Vector3.up, moveValue);
            }
            if (moveValue < 0)
                MoveUpDown(Vector3.down, moveValue);


            prevInt = currentInt;
        }
    }

    private void MoveRightLeft(Vector3 dir, int moveVal)
    {
        Vector3 newPos = transform.position + dir * Time.deltaTime * moveVal * factor;
        float dirLength = Mathf.Abs(newPos.x - transform.position.x);
        Vector3 upRayPoint = new Vector3(transform.position.x, transform.position.y + halfDotDimention, transform.position.z);
        Vector3 downRayPoint = new Vector3(transform.position.x, transform.position.y - halfDotDimention, transform.position.z);
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
                    transform.position += dir * Time.deltaTime * moveValue * factor;
                    break;
                case Vector3 v when v.Equals(Vector3.left):
                    transform.position -= dir * Time.deltaTime * moveValue * factor;
                    break;
            }
        }

    }

    private void MoveUpDown(Vector3 dir, int moveVal)
    {
        Vector3 newPos = transform.position + dir * Time.deltaTime * moveVal * factor;
        float dirLength = Mathf.Abs(newPos.y - transform.position.y);
        Vector3 leftRayPoint = new Vector3(transform.position.x - halfDotDimention, transform.position.y, transform.position.z);
        Vector3 rightRayPoint = new Vector3(transform.position.x + halfDotDimention, transform.position.y, transform.position.z);
        RaycastHit hit;

        if (Physics.Raycast(leftRayPoint, dir, out hit, dirLength, wallLayer))
        {
            var point = new Vector3(0f, 0f, 0f);
            switch (dir)
            {
                case Vector3 v when v.Equals(Vector3.up):
                    point = hit.point - new Vector3(halfDotDimention, 0f, 0f);
                    point = new Vector3(point.x, transform.position.y, point.z);
                    transform.position = point;
                    break;
                case Vector3 v when v.Equals(Vector3.down):
                    point = hit.point + new Vector3(halfDotDimention, 0f, 0f);
                    point = new Vector3(point.x, transform.position.y, point.z);
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
                    point = hit.point - new Vector3(0f, halfDotDimention, 0f);
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
                    transform.position += dir * Time.deltaTime * moveValue * factor;
                    break;
                case Vector3 v when v.Equals(Vector3.down):
                    transform.position -= dir * Time.deltaTime * moveValue * factor;
                    break;
            }
        }

    }


}
