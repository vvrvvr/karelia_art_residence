using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display2Manager : MonoBehaviour
{
    [SerializeField] Camera _cam;
    [SerializeField] GameObject _dot;
    [SerializeField] GameObject _finish;


    //private vals;
    private float xBound = 6.16f;
    private float yBound = 4.5f;
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;
    void Start()
    {
        xMin = -xBound;
        xMax = xBound;
        yMin = -yBound;
        yMax = yBound;


        _dot.transform.position = new Vector3(xMax, yMax, 0);
    }

    private void SetStartPosition()
    {

    }
}
