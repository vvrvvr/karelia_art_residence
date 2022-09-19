using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display2Manager : MonoBehaviour
{
    [SerializeField] Camera _cam;
    [SerializeField] GameObject _dot;
    [SerializeField] GameObject _finish;
    [SerializeField] ArduinoManager _arduinoManager;

    private bool isFirstLaunch = true;

    //debug
    public Text _text;
    public Slider _slider1;
    public Slider _slider2;

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

    //dot moving
    [HideInInspector] public int horizontalMoveValue  = 0;
    [HideInInspector] public int verticalMoveValue = 0;
    private int prevHorizontalValue = 0;
    private int prevVerticalValue = 0;
    private int currentHor = 0;
    private int currentVert = 0;


    void Start()
    {
        xMin = -xBound;
        xMax = xBound;
        yMin = -yBound;
        yMax = yBound;

        ResetDisplay2();
        //ChangeLevel();
        //SetStartPosition();
    }

    private void Update()
    {
        currentHor = _arduinoManager.HorizontalControl;
        currentVert = _arduinoManager.VerticalControl;
        if (isFirstLaunch)
        {
            isFirstLaunch = false;
            prevHorizontalValue = currentHor;
            prevVerticalValue = currentVert;
        }
        horizontalMoveValue = currentHor - prevHorizontalValue;
        if(horizontalMoveValue != 0)
            _text.text = "horizontal move value = " + horizontalMoveValue;
        verticalMoveValue = currentVert - prevVerticalValue;

        //что-то делаем

        //set prev values
        prevHorizontalValue = currentHor;
        prevVerticalValue = currentVert;

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
