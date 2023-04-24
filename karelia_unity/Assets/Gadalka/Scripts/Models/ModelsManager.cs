using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelsManager : MonoBehaviour
{
    public static ModelsManager Instance { get; private set; } // экземпл€р синглтона

    private GameObject[] modelArray;
    [SerializeField] private GameObject models;
    [SerializeField] private Texture[] modelsTextures = new Texture[0];
    [SerializeField] private Material material;
    [SerializeField] private Display2Manager display2Manager;
    [Space]
    [SerializeField] private MusicManager musicManager;
    private int currentCount = 0;
    private int currentTextureCount = 0;
    private int maxCount;

    [SerializeField] private float maxAmplitude;

    public float _minDistance = 0f;
    public float _maxDistance = 0f;
    public float _currentMin;
    public float _currentMax;
    public float _amplitudeSection;
    public float _currentDistance;

    //public float testTest = 0;

    public bool isShaderWorking = false;
    private void Awake()
    {
        // провер€ем, существует ли уже экземпл€р синглтона
        if (Instance == null)
        {
            Instance = this; // если нет, то создаем его
            DontDestroyOnLoad(gameObject); // сохран€ем объект между сценами
        }
        else
        {
            Destroy(gameObject); // если синглтон уже существует, удал€ем этот объект
        }
    }
    void Start()
    {
        //models
        CollectModels();
        currentCount = Random.Range(0, maxCount);
        currentTextureCount = Random.Range(0, modelsTextures.Length-1);
        _currentDistance = display2Manager._currentDistance;
        //material
        // SetStartValues();

        GetRandomModel();
    }
    public void ChangeLevelValues()
    {
        _currentMax = _currentMin;
        _currentMin = _currentMin - _amplitudeSection;
        if (_currentMin < 10)
            _currentMin = 0f;
    }

    public void SetStartValues()
    {
        _currentMax = maxAmplitude;
        _amplitudeSection = _currentMax / display2Manager.GetLevelsLength();
        _currentMin = _currentMax - _amplitudeSection;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.U))
        {
            GetRandomModel();
        }
        HandleAmplitudeValues();
    }

    public void SetModelMaterialAmplitude(float val)
    {
        material.SetFloat("_Amplitude", val);
        musicManager.NoiseSetValue(val); 
    }

    private void HandleAmplitudeValues()
    {
        if(isShaderWorking)
        {
            var distance = display2Manager._currentDistance;
            var val = ConvertNumber(distance);
            material.SetFloat("_Amplitude", val);
            musicManager.NoiseSetValue(val);
            musicManager.PulseSetValue(distance);
        }
        else
        {
            musicManager.PulseSetValue(-1);
        }
    }

    public string GetModelName()
    {
        string name = "";
        foreach (var model in modelArray)
        {
            if (model.activeSelf)
            {
                name = model.name;
                break;
            }
        }
        return name;
    }

    public float ConvertNumber(float number)
    {
        float convertedNumber = Mathf.InverseLerp(_minDistance, _maxDistance, number);
        convertedNumber = Mathf.Lerp(_currentMin, _currentMax, convertedNumber);
        return convertedNumber;
    }

    private void CollectModels()
    {
        if (models != null)
        {
            int childCount = models.transform.childCount;
            maxCount = childCount;
            modelArray = new GameObject[childCount];
            for (int i = 0; i < childCount; i++)
            {
                GameObject child = models.transform.GetChild(i).gameObject;
                modelArray[i] = child;
                child.SetActive(false);
            }
        }
        else
        {
            Debug.Log("модели не найдены");
        }
    }
    
    public void GetRandomModel()
    {
        int newCount = Random.Range(0, modelArray.Length -1 );

        while(newCount == currentCount)
        {
            newCount = Random.Range(0, modelArray.Length - 1);
        }
        GetRandomTexture();
        foreach(var model in modelArray)
        {
            model.SetActive(false);
        }
        //modelArray[currentCount].SetActive(false);
        currentCount = newCount;
        modelArray[newCount].SetActive(true);
    }
    
    private void GetRandomTexture()
    {
        int newCount = Random.Range(0, modelsTextures.Length - 1);

        while (newCount == currentTextureCount)
        {
            newCount = Random.Range(0, modelsTextures.Length - 1);
        }
        currentTextureCount = newCount;
        material.mainTexture = modelsTextures[newCount];
    }

    public void ShuffleArray()
    {
        // »спользуем алгоритм ‘ишера-…етса дл€ перемешивани€ массива в случайном пор€дке
        for (int i = modelArray.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            // ќбмениваем элементы с индексами i и j местами
            GameObject temp = modelArray[i];
            modelArray[i] = modelArray[j];
            modelArray[j] = temp;
        }
    }
}
