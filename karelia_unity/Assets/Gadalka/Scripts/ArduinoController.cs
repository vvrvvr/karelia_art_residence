using UnityEngine;
using Uduino;

public class ArduinoController : MonoBehaviour
{
    private UduinoManager uduinoManager;
    public int pinNumber1 = 7;
    public int pinNumber2 = 6;
    public int analogPinNumber = 1; // Номер аналогового пина
    private int lastPinValue1 = 0;
    private int lastPinValue2 = 0;
    private int lastAnalogPinValue = 0;

    void Start()
    {
        uduinoManager = UduinoManager.Instance;
        UduinoManager.Instance.alwaysRead = true;
        uduinoManager.pinMode(pinNumber1, PinMode.Input);
        uduinoManager.pinMode(pinNumber2, PinMode.Input);
        uduinoManager.pinMode(analogPinNumber, PinMode.Input_pullup); // Установка внутреннего подтягивающего резистора
        uduinoManager.OnDataReceived += OnDataReceived;
    }

    void OnDataReceived(string data, UduinoDevice device)
    {
        Debug.Log("ds");
    }
}