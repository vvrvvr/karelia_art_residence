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
    [SerializeField] GameObject[] levels = new GameObject[3];


    //private vals;
    private float xBound = 5.8f;
    private float yBound = 3.8f;
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    private int currentLevel = 0;





    void Start()
    {
        xMin = -xBound;
        xMax = xBound;
        yMin = -yBound;
        yMax = yBound;

        ResetDisplay2();





    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.J))
        {
            ChangeLevel();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            ResetDisplay2();
        }
        //что-то делаем

        //set prev values


    }


    public void StartGame()
    {

    }

    public void ResetDisplay2()
    {
        currentLevel = 0;
        _dot.SetActive(false);
        _finish.SetActive(false);
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }
        levels[currentLevel].SetActive(true);
        SetStartPosition();
    }

    public void ChangeLevel()
    {
        currentLevel++;
        _dot.SetActive(false);
        _finish.SetActive(false);
        if (currentLevel >= levels.Length)
        {
            //finish game
            return;
        }
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }
        levels[currentLevel].SetActive(true);
        SetStartPosition();
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
