using UnityEngine;
using Uduino;
using UnityEngine.UI;


public class ArduinoTestButton : MonoBehaviour
{
    [SerializeField] private Text arduinoOutpuText;
    [SerializeField] private Slider _slider;

    //private variables
    private UduinoManager  _uduino;

    private void Awake()
    {
        _uduino = UduinoManager.Instance;
    }

    void Start()
    {
        _uduino.pinMode(11, PinMode.Input_pullup);
        _uduino.pinMode(AnalogPin.A1, PinMode.Input);
    }

    void Update()
    {
        var buttonCondition = _uduino.digitalRead(11);
        var potentiometerCondition = _uduino.analogRead(AnalogPin.A1);

        arduinoOutpuText.text = ""+buttonCondition;
        _slider.value = (float)potentiometerCondition / 1000.0f;
    }
   

}
