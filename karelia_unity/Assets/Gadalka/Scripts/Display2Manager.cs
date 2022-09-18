using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display2Manager : MonoBehaviour
{
    [SerializeField] Camera _cam;
    [SerializeField] GameObject _dot;
    [SerializeField] GameObject _finish;
    [SerializeField] ArduinoManager _arduinoManager;
    //levels
    [SerializeField] GameObject level1;
    [SerializeField] GameObject level2;
    [SerializeField] GameObject level3;


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

        ResetDisplay2();
    }


    public void StartGame()
    {

    }

    public void ResetDisplay2()
    {
        
    }

    public void ChangeLevel()
    {

    }


    private void SetStartPosition()
    {

    }
}
