using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; } // экземпл€р синглтона
    public event Action OnRotate; // событие дл€ поворота объекта
    public bool isDead = false;
    [SerializeField] private GameObject dotObject;
    private Transform dotTransform;
    [SerializeField] private Dot dotScript;
    [SerializeField] private GameObject deathParticle;

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
        // провер€ем, нажата ли клавиша пробела
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // вызываем событие поворота объекта
            OnRotate?.Invoke();
        }
    }

    // метод дл€ €вного вызова событи€ поворота объекта
    public void InvokeRotateEvent()
    {
        OnRotate?.Invoke();
    }

    public void PlayerDeath()
    { 
        if(!isDead)
        {
            dotScript.hasControl = false;
            isDead = true;
            Instantiate(deathParticle, dotTransform.position, Quaternion.identity);
            dotObject.SetActive(false);
            Debug.Log("death");
        }
        
    }
}