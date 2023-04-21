using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Display2Manager : MonoBehaviour
{
    [SerializeField] Transform _cam1Anchor;
    [SerializeField] GameObject _dot;
    [SerializeField] GameObject _finish;
    [SerializeField] ArduinoManager _arduinoManager;
    [SerializeField] private ModelsManager _modelsManager;
    [SerializeField] private GameObject resetTimeline;
    [SerializeField] private PlayableDirector finishTimeline;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private TextMeshProUGUI distortionText;
    public int distortionCount = 0;

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

    private int resetButtonCurrent = 0;
    private int resetButtonPrev = 0;

    private int rotateButton1Current = 0;
    private int rotateButton1Prev = 0;

    private int rotateButton2Current = 0;
    private int rotateButton2Prev = 0;
    private bool canRotate = true;

    [SerializeField] private EventManager _eventManager;




    void Start()
    {
        xMin = -xBound;
        xMax = xBound;
        yMin = -yBound;
        yMax = yBound;

        _dotPosition = _dot.transform;
        _finishPoition = _finish.transform;
        ResetDisplay2();
    }

    private void Update()
    {
        resetButtonCurrent = _arduinoManager.b5Current;
        if (resetButtonCurrent != resetButtonPrev && resetButtonCurrent == 0)
        {
            ResetDisplay2();
        }


        rotateButton1Current = _arduinoManager.b3Current;
        if (rotateButton1Current != rotateButton1Prev && rotateButton1Current == 0)
        {
            if (canRotate)
                _eventManager.InvokeRotateEvent(true);
            else
                _cam1Anchor.DORotate(Vector3.zero, 0.5f, RotateMode.FastBeyond360);

        }

        rotateButton2Current = _arduinoManager.b7Current;
        if (rotateButton2Current != rotateButton2Prev && rotateButton2Current == 0)
        {
            if (canRotate)
                _eventManager.InvokeRotateEvent(false);
            else
                _cam1Anchor.DORotate(Vector3.zero, 0.5f, RotateMode.FastBeyond360);
        }



        _currentDistance = Vector3.Distance(_dotPosition.localPosition, _finishPoition.localPosition);

        if (Input.GetKeyDown(KeyCode.J))
        {
            ChangeLevel();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            ResetDisplay2();
        }

        //что-то делаем

        //set prev values

        resetButtonPrev = resetButtonCurrent;
        rotateButton1Prev = rotateButton1Current;
        rotateButton2Prev = rotateButton2Current;
    }

    public int GetLevelsLength()
    {
        return levels.Length;

    }

    public void StartGame()
    {

    }
    public void EndGame()
    {
        finishTimeline.Play();
        _cam1Anchor.DORotate(Vector3.zero, 0.5f, RotateMode.FastBeyond360);
        ModelsManager.Instance.SetModelMaterialAmplitude(0f);
        canRotate = false;
        messageText.text =  ModelsManager.Instance.GetModelName();
        distortionText.text = "Количество искажений: " + distortionCount;
    }

    public void ResetDisplay2()
    {
        canRotate = true;
        currentLevel = 0;
        _dot.SetActive(false);
        _finish.SetActive(false);
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }
        levels[currentLevel].SetActive(true);
        SetStartPosition();
        _modelsManager.SetStartValues();
        resetTimeline.SetActive(true);
        ModelsManager.Instance.GetRandomModel();
        ModelsManager.Instance.ShuffleArray();
        distortionCount = 0;
    }

    public void ChangeLevel()
    {
        _modelsManager.isShaderWorking = false;

        currentLevel++;
        _dot.SetActive(false);
        _finish.SetActive(false);
        if (currentLevel >= levels.Length)
        {
            levels[levels.Length - 1].SetActive(false);
            EndGame();
            return;
        }
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }
        levels[currentLevel].SetActive(true);

        SetStartPosition();
        _modelsManager.ChangeLevelValues();
    }


    public void SetStartPosition()
    {
        StartCoroutine(WaitToSetDot());
        dotX = Choose(xMax, xMin);
        dotY = Choose(yMax, yMin);
        var finishX = dotX * -1;
        var finishY = dotY * -1;
        //размещаем точку и финиш на экране
        _dot.transform.localPosition = new Vector3(dotX, dotY, 0f);
        _finish.transform.localPosition = new Vector3(finishX, finishY, 0f);
        _finish.SetActive(true);

        //set distance and send it to models manager
        _maxDistance = Vector3.Distance(_dot.transform.localPosition, _finish.transform.localPosition);
        _modelsManager._maxDistance = _maxDistance;
        _modelsManager.isShaderWorking = true;
    }
    public void ResetPlayerPosition()
    {
        _dotPosition.localPosition = new Vector3(dotX, dotY, 0f);
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
