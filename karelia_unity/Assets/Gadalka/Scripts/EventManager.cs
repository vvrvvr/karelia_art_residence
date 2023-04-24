using UnityEngine;
using System;
using DG.Tweening;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; } // экземпл€р синглтона
    public event Action<bool> OnRotate; // событие дл€ поворота объекта
    public bool isDead = false;
    [SerializeField] private GameObject dotObject;
    private Transform dotTransform;
    [SerializeField] private Dot dotScript;
    [SerializeField] private GameObject deathParticle;
    [SerializeField] private Camera display2Cam;
    [SerializeField] private Display2Manager display2Manager;
    [SerializeField] private MusicManager musicManager;

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
    private void Start()
    {
        dotTransform = dotObject.transform;
    }

    private void Update()
    {
        //// провер€ем, нажата ли клавиша пробела
        if (Input.GetKeyDown(KeyCode.T))
        {
            // вызываем событие поворота объекта
            InvokeRotateEvent(false);
        }
    }

    // метод дл€ €вного вызова событи€ поворота объекта
    public void InvokeRotateEvent(bool isLeftTurn)
    {
        OnRotate?.Invoke(isLeftTurn);
        musicManager.PlayTurn();
        ModelsManager.Instance.GetRandomModel();
        display2Manager.distortionCount++;
    }

    public void PlayerDeath()
    { 
        if(!isDead)
        {
            dotScript.hasControl = false;
            isDead = true;
            Instantiate(deathParticle, dotTransform.position, Quaternion.identity);
            dotObject.SetActive(false);
            display2Cam.DOShakePosition(0.5f, 0.5f, 10, 45);
            display2Manager.ResetPlayerPosition();
            Debug.Log("death");
            ModelsManager.Instance.GetRandomModel();
        }
        
    }
}