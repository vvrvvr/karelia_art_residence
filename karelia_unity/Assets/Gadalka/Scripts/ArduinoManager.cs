using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Uduino;

public class ArduinoManager : MonoBehaviour
{
    [SerializeField] private Text arduinoOutpuText;
    [SerializeField] private Slider _slider;
    private UduinoManager _uduino;
    private bool isBoardConnected = true;

    //potentiometers 
    private int p1Current;
    private int p1Prev = 0;
    private int p2Current;
    private int p2Prev = 0;
    private int p3Current;
    private int p3Prev = 0;
    private int p4Current;
    private int p4Prev = 0;
    private int p5Current;
    private int p5Prev = 0;

    //buttons
    private int b1Current;
    private int b1Prev = 0;

    //крабики хз как по английски
    private int k1Current;
    private int k1Prev = 0;


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

            //

            p1Prev = p1Current;
            p1Prev = p1Current;
            p1Prev = p1Current;
            p1Prev = p1Current;
            p1Prev = p1Current;
            b1Prev = b1Current;
            k1Prev = k1Current;
        }
    }
}
