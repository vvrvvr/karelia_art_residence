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


    // vals;
    private float xBound = 6f;
    private float yBound = 6f;
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;
    private float dotX;
    private float dotY;

    private int currentLevel = 0;

    public float _currentDistance;
    public float _maxDistance;

    private Transform _dotPosition;
    private Transform _finishPoition;

    [SerializeField] private EventManager _eventManager;




    void Start()
    {
        xMin = -xBound;
        xMax = xBound;
        yMin = -yBound;
        yMax = yBound;

        ResetDisplay2();
        _dotPosition = _dot.transform;
        _finishPoition = _finish.transform;
    }

    private void Update()
    {
        _currentDistance = Vector3.Distance(_dotPosition.position, _finishPoition.position);

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


    public void SetStartPosition()
    {
        StartCoroutine(WaitToSetDot());
        dotX = Choose(xMax, xMin);
        dotY = Choose(yMax, yMin);
        var finishX = dotX * -1;
        var finishY = dotY * -1;
        //размещаем точку и финиш на экране
        _dot.transform.position = new Vector3(dotX, dotY, 0f);
        _finish.transform.position = new Vector3(finishX, finishY, 0f);
        _finish.SetActive(true);
        _maxDistance = Vector3.Distance(_dot.transform.position, _finish.transform.position);
    }
    public void ResetPlayerPosition()
    {
        _dotPosition.position = new Vector3(dotX, dotY, 0f);
        StartCoroutine(WaitToSetDot());
    }

    private IEnumerator WaitToSetDot()
    {
        yield return new WaitForSeconds(1f);
        _dot.SetActive(true);
        _dot.GetComponent<Dot>().hasControl = true;
        _eventManager.isDead = false;
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
