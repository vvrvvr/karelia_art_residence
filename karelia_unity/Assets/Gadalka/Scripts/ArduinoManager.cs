using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Uduino;

public class ArduinoManager : MonoBehaviour
{
    [SerializeField] private Text arduinoOutpuText;
    [SerializeField] private Slider _slider;
    [SerializeField] private Display2Manager _display2Manager;
    private UduinoManager _uduino;
    private bool isBoardConnected = true;

    //potentiometers 
    [HideInInspector] public int p1Current;
    [HideInInspector] public int p1Prev = 0;
    [HideInInspector] public int p2Current;
    [HideInInspector] public int p2Prev = 0;
    [HideInInspector] public int p3Current;
    [HideInInspector] public int p3Prev = 0;
    [HideInInspector] public int p4Current;
    [HideInInspector] public int p4Prev = 0;
    [HideInInspector] public int p5Current;
    [HideInInspector] public int p5Prev = 0;
    private int HorizontalControl;
    private int VerticalControl;


    //buttons
    [HideInInspector] public int b1Current;
    [HideInInspector] public int b1Prev = 0;

    //крабики хз как по английски
    [HideInInspector] public int k1Current;
    [HideInInspector] public int k1Prev = 0;


    private void Awake()
    {
        _uduino = UduinoManager.Instance;
    }
    void Start()
    {
        //potentiometers
        _uduino.pinMode(AnalogPin.A1, PinMode.Input);
        _uduino.pinMode(AnalogPin.A2, PinMode.Input);
        _uduino.pinMode(AnalogPin.A3, PinMode.Input);
        _uduino.pinMode(AnalogPin.A4, PinMode.Input);
        _uduino.pinMode(AnalogPin.A5, PinMode.Input);

        //buttons
        _uduino.pinMode(7, PinMode.Input_pullup);

        //krab
        _uduino.pinMode(AnalogPin.A0, PinMode.Input);

        //TEMP
        SetControls();
    }

    void Update()
    {
        if (isBoardConnected)
        {
            //potentiometers
            p1Current = _uduino.analogRead(AnalogPin.A1);
            p2Current = _uduino.analogRead(AnalogPin.A2);
            p3Current = _uduino.analogRead(AnalogPin.A3);
            p4Current = _uduino.analogRead(AnalogPin.A4);
            p5Current = _uduino.analogRead(AnalogPin.A5);

            //buttons
            b1Current = _uduino.digitalRead(7);

            //krab
            k1Current = _uduino.analogRead(AnalogPin.A0);


            //секция манипуляций
            UpdateVals();
            //

            p1Prev = p1Current;
            p1Prev = p1Current;
            p1Prev = p1Current;
            p1Prev = p1Current;
            p1Prev = p1Current;
            b1Prev = b1Current;
            k1Prev = k1Current;
        }
        else
        {

        }
    }

    public void SetControls()
    {

    }

    public void UpdateVals()
    {

    }

    public void ResetGame()
    {
        _display2Manager.ResetDisplay2();
    }
}
