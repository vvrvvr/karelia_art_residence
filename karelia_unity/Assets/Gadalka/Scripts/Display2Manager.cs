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

    //levels
    [SerializeField] GameObject level1;
    [SerializeField] GameObject level2;
    [SerializeField] GameObject level3;


    //private vals;
    private float xBound = 5.8f;
    private float yBound = 3.8f;
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

        _dot.SetActive(false);
        _finish.SetActive(false);

        SetStartPosition();
        ResetDisplay2();


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
        StartCoroutine(WaitToSetDot());
        var dotX = Choose(xMax, xMin);
        var dotY = Choose(yMax, yMin);
        var finishX = dotX * -1;
        var finishY = dotY * -1;

        _dot.transform.position = new Vector3(dotX, dotY, 0f);
        _finish.transform.position = new Vector3(finishX, finishY, 0f);
    }

    private IEnumerator WaitToSetDot()
    {
        yield return new WaitForSeconds(1f);
        _dot.SetActive(true);
        _finish.SetActive(true);
    }

    private float Choose(float val1, float val2)
    {
        float[] arr = new float[2];
        arr[0] = val1;
        arr[1] = val2;
        int index = Random.Range(0, 2);
        return arr[index];
    }
}
