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
    private bool isBoardConnected = false;
    

    //potentiometers 
    private int[] potentiometersArr = new int[6];
    [HideInInspector] public int p1Prev = 0;
    [HideInInspector] public int p2Prev = 0;
    [HideInInspector] public int p3Prev = 0;
    [HideInInspector] public int p4Prev = 0;
    [HideInInspector] public int p5Prev = 0;
    public int HorizontalControl;
    public int VerticalControl;

    private int vertIndex;
    private int horIndex;


    //buttons
    [HideInInspector] public int b1Current;
    [HideInInspector] public int b1Prev = 0;

    //крабики хз как по английски
    [HideInInspector] public int k0Prev = 0;


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
        SetControls(false); // задаЄм случайные потенциометры дл€ управлени€ перемещением
    }

    void Update()
    {
        if (isBoardConnected)
        {
            //potentiometers
            potentiometersArr[1] = _uduino.analogRead(AnalogPin.A1);
            potentiometersArr[2] = _uduino.analogRead(AnalogPin.A2);
            potentiometersArr[3] = _uduino.analogRead(AnalogPin.A3);
            potentiometersArr[4] = _uduino.analogRead(AnalogPin.A4);
            potentiometersArr[5] = _uduino.analogRead(AnalogPin.A5);

            //buttons
            b1Current = _uduino.digitalRead(7);

            //krab
            potentiometersArr[0] = _uduino.analogRead(AnalogPin.A0);


            //секци€ манипул€ций
            UpdateVals();

            // set previous values
            p1Prev = potentiometersArr[1];
            p2Prev = potentiometersArr[2];
            p3Prev = potentiometersArr[3];
            p4Prev = potentiometersArr[4];
            p5Prev = potentiometersArr[5];
            b1Prev = b1Current;
            k0Prev = potentiometersArr[0];
        }
        else
        {
            //что-то делать, если ардуинка не подключена
        }
    }

    public void SetControls(bool isRandom)
    {
        if (isRandom)
        {
            vertIndex = Random.Range(1, potentiometersArr.Length);
            horIndex = Random.Range(1, potentiometersArr.Length);
            if (vertIndex == horIndex)
            {
                while (vertIndex == horIndex)
                {
                    horIndex = Random.Range(1, potentiometersArr.Length);
                }
            }
            Debug.Log("vert = " + vertIndex);
            Debug.Log("hor = " + horIndex);
        }
        else
        {
            vertIndex = 1;
            horIndex = 2;
        }
    }

    public void UpdateVals()
    {
        HorizontalControl = potentiometersArr[horIndex];
        VerticalControl = potentiometersArr[vertIndex];
    }

    public void ResetGame()
    {
        _display2Manager.ResetDisplay2();
        SetControls(true);
        //reset display 1
    }

    public void DataRecieve()
    {
        Debug.Log("data recieved");
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
