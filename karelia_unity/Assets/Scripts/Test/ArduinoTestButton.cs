using UnityEngine;
using Uduino;
using UnityEngine.UI;


public class ArduinoTestButton : MonoBehaviour
{
    [SerializeField] private Text arduinoOutpuText;
    

    void Start()
    {
        UduinoManager.Instance.pinMode(11, PinMode.Input_pullup);
    }

    void Update()
    {
        var res = UduinoManager.Instance.digitalRead(11);
        arduinoOutpuText.text = ""+res;
    }
   

}
