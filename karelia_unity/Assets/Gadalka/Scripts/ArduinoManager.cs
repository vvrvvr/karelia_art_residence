using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Uduino;

public class ArduinoManager : MonoBehaviour
{
    [SerializeField] private Slider p1Slider;
    [SerializeField] private Slider p2Slider;

    [SerializeField] private Display2Manager _display2Manager;
    private UduinoManager _uduino;
    private bool isBoardConnected = true;

    //potentiometers 
    private int potentiometer1;
    [Space (10)]
    [SerializeField] private int p1Min = 0;
    [SerializeField] private int p1Max = 0;
    [SerializeField] private int p1MaxValue = 100;
    [Space(10)]
    public float HorizontalInput;
    public float VerticalInput;

    private int potentiometer2;

    

    //buttons
    [HideInInspector] public int b3Current;
    [HideInInspector] public int b5Current;
    [HideInInspector] public int b7Current;

    [HideInInspector] public float p1Prev = 0;
    [HideInInspector] public float p2Prev = 0;
    [HideInInspector] public int b3Prev = 0;
    [HideInInspector] public int b5Prev = 0;
    [HideInInspector] public int b7Prev = 0;

    //крабики хз как по английски
    [HideInInspector] public int k0Prev = 0;

    
    [HideInInspector] public float p1diffCurrent = 0;

    //private float p1diffCurrentPrev = 0;

    private void Awake()
    {
        _uduino = UduinoManager.Instance;
        _uduino.alwaysRead = false;
        _uduino.readTimeout = 1;
        _uduino.writeTimeout = 1;
    }

    void Start()
    {
        //potentiometers
        _uduino.pinMode(AnalogPin.A1, PinMode.Input_pullup);
        _uduino.pinMode(AnalogPin.A3, PinMode.Input_pullup);

        // //buttons
        _uduino.pinMode(3, PinMode.Input_pullup);
        _uduino.pinMode(5, PinMode.Input_pullup);
        _uduino.pinMode(7, PinMode.Input_pullup);
        //
        // //krab
        // _uduino.pinMode(AnalogPin.A0, PinMode.Input);

        //TEMP
    }

 

    void Update()
    {
        if (isBoardConnected)
        {
            //potentiometers
            potentiometer1 = _uduino.analogRead(AnalogPin.A1);
            potentiometer2 = _uduino.analogRead(AnalogPin.A3);
            
            // //buttons
            b3Current = _uduino.digitalRead(3);
            b5Current = _uduino.digitalRead(5);
            b7Current = _uduino.digitalRead(7);
            //
            // //krab
            // potentiometersArr[0] = _uduino.analogRead(AnalogPin.A0);

            //секци€ манипул€ций
            UpdateVals();

           

            // set previous values
            p1Prev = HorizontalInput;
            p2Prev = VerticalInput;

            //b3Prev = b3Current;
            //b5Prev = b5Current;
            //b7Prev = b7Current;

            //p1diffCurrentPrev = p1diffCurrent;
            //p1diffCurrent = 0;
        }
        else
        {
            //что-то делать, если ардуинка не подключена
        }
    }


    public void UpdateVals()
    {
        if (potentiometer1 < p1Min )
        {
            potentiometer1 = p1Min;
        }
        if(potentiometer1 > p1Max)
        {
            potentiometer1 = p1Max;
        }
        HorizontalInput =  Mathf.InverseLerp(p1Min, p1Max, potentiometer1);

        if (Mathf.Abs(HorizontalInput - p1Prev) > 0.7f)
        {
            HorizontalInput = p1Prev;
            //добавить звук щелчка
        }


        if (potentiometer2 < p1Min)
        {
            potentiometer2 = p1Min;
        }
        if (potentiometer2 > p1Max)
        {
            potentiometer2 = p1Max;
        }
        VerticalInput = Mathf.InverseLerp(p1Min, p1Max, potentiometer2);

        if (Mathf.Abs(VerticalInput - p2Prev) > 1f)
        {
            VerticalInput = p2Prev;
            //добавить звук щелчка
        }


         

        //update values
        p1Slider.value = HorizontalInput;
        p2Slider.value = VerticalInput;

    }

    public void ResetGame()
    {
        _display2Manager.ResetDisplay2();
    }

    public void DataRecieve()
    {
        //Debug.Log("data recieved");
    }
    //
    public void BoardConnected()
    {
        Debug.Log("board connected");
        isBoardConnected = true;
        //errorScreen.SetActive(false);
    }
    public void BoardDisconnected()
    {
        Debug.Log("board disconnected");
        isBoardConnected = false;
        //errorScreen.SetActive(true);
    }
}
