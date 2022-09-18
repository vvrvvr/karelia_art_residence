using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dot : MonoBehaviour
{
    //private bool hasControl = true;
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
            Move(Vector3.up, moveValue);
        }
        if (moveValue < 0)
            Move(Vector3.down, moveValue);


        prevInt = currentInt;
    }

    private void Move(Vector3 dir, int moveVal)
    {
        Vector3 newPos = transform.position + dir * Time.deltaTime * moveVal * factor;
        float dirLength = Mathf.Abs(newPos.x - transform.position.x);
        if (Physics.Raycast(transform.position, dir, out RaycastHit hit, dirLength, wallLayer))
        {
            var point = new Vector3(0f,0f,0f);
            switch (dir)
            {
                case Vector3 v when v.Equals(Vector3.right):
                    point = hit.point - new Vector3(halfDotDimention, 0f, 0f);
                    transform.position = point;
                    break;
                case Vector3 v when v.Equals(Vector3.left):
                    point = hit.point + new Vector3(halfDotDimention, 0f, 0f);
                    transform.position = point;
                    break;
                case Vector3 v when v.Equals(Vector3.up):
                    point = hit.point - new Vector3(0f, halfDotDimention, 0f);
                    transform.position = point;
                    break;
                case Vector3 v when v.Equals(Vector3.down):
                    point = hit.point + new Vector3(0f, halfDotDimention, 0f);
                    transform.position = point;
                    break;
            }
            //var point = hit.point - new Vector3(halfDotDimention, 0f, 0f);
            //transform.position = point;
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
                case Vector3 v when v.Equals(Vector3.up):
                    transform.position += dir * Time.deltaTime * moveValue * factor;
                    break;
                case Vector3 v when v.Equals(Vector3.down):
                    transform.position -= dir * Time.deltaTime * moveValue * factor;
                    break;
            }
            //transform.position += dir * Time.deltaTime * moveValue * factor;
        }
    }
}
