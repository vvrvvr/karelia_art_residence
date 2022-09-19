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

    

    //debug
  

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

   



    void Start()
    {
        xMin = -xBound;
        xMax = xBound;
        yMin = -yBound;
        yMax = yBound;

        ResetDisplay2();
        StartCoroutine(WaitToSetDot());

    }

    private void Update()
    {
        

        //что-то делаем

        //set prev values
       

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

    private IEnumerator WaitToSetDot()
    {
        yield return new WaitForSeconds(0.1f);
        _dot.SetActive(true);
    }
}
