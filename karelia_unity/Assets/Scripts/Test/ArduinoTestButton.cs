using UnityEngine;
using Uduino;
using UnityEngine.UI;


public class ArduinoTestButton : MonoBehaviour
{
    [SerializeField] private Text arduinoOutpuText;
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject errorScreen;

    //private variables
    private UduinoManager  _uduino;
    private bool isBoardConnected = false;

    private void Awake()
    {
        _uduino = UduinoManager.Instance;
        errorScreen.SetActive(true);
    }

    void Start()
    {
        _uduino.pinMode(11, PinMode.Input_pullup);
        _uduino.pinMode(AnalogPin.A1, PinMode.Input);
        
    }

    void Update()
    {
        if (isBoardConnected)
        {
            var buttonCondition = _uduino.digitalRead(11);
            var potentiometerCondition = _uduino.analogRead(AnalogPin.A1);

            arduinoOutpuText.text = "" + buttonCondition;
            _slider.value = (float)potentiometerCondition / 1000.0f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _uduino.GetPortState();
            }
        }
    }
   
    public void DataRecieve()
    {
        Debug.Log("data recieved");
    }
    public void BoardConnected()
    {
        Debug.Log("board connected");
        isBoardConnected = true;
        errorScreen.SetActive(false);
    }
    public void BoardDisconnected()
    {
        Debug.Log("board disconnected");
        isBoardConnected = false;
        errorScreen.SetActive(true);
    }
}
