using UnityEngine;
using Uduino;
using UnityEngine.UI;


public class ArduinoTestButton : MonoBehaviour
{
    [SerializeField] private Text arduinoOutpuText;
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject errorScreen;

    //obj
    [SerializeField] private GameObject Hand;
    

    //private variables
    private UduinoManager  _uduino;
    private bool isBoardConnected = false;
    private int currentBlendShape = 0;
    private int maxBlendShape = 3;
    private SkinnedMeshRenderer handRenderer;
    private int currentButtonState= 0;
    private int prevButtonState = 2;

    private void Awake()
    {
        _uduino = UduinoManager.Instance;
        //errorScreen.SetActive(true);
        handRenderer = Hand.GetComponent<SkinnedMeshRenderer>();
    }

    void Start()
    {
        _uduino.pinMode(11, PinMode.Input_pullup);
        _uduino.pinMode(AnalogPin.A1, PinMode.Input);
        for (int i = 0; i <= maxBlendShape; i++)
        {
            float val = Random.Range(0, 100f);
            handRenderer.SetBlendShapeWeight(i, val);
        }
    }

    void Update()
    {
        if (isBoardConnected)
        {
            currentButtonState = _uduino.digitalRead(11);
            if(currentButtonState != prevButtonState)
            {
                Debug.Log("switched. Current shape = " + currentBlendShape);
                currentBlendShape++;
                if(currentBlendShape > maxBlendShape)
                {
                    currentBlendShape = 0;
                }
            }
            prevButtonState = currentButtonState;

            var potentiometerCondition = _uduino.analogRead(AnalogPin.A1);

            //arduinoOutpuText.text = "" + buttonCondition;
            var shapeVal = (float)potentiometerCondition / 10.0f;
            handRenderer.SetBlendShapeWeight(currentBlendShape, shapeVal);

            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    _uduino.GetPortState();
            //}
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
