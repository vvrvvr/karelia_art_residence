using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; } // ��������� ���������
    public event Action OnRotate; // ������� ��� �������� �������
    public bool isDead = false;
    [SerializeField] private GameObject dotObject;
    private Transform dotTransform;
    [SerializeField] private Dot dotScript;
    [SerializeField] private GameObject deathParticle;

    private void Awake()
    {
        // ���������, ���������� �� ��� ��������� ���������
        if (Instance == null)
        {
            Instance = this; // ���� ���, �� ������� ���
            DontDestroyOnLoad(gameObject); // ��������� ������ ����� �������
        }
        else
        {
            Destroy(gameObject); // ���� �������� ��� ����������, ������� ���� ������
        }
    }
    private void Start()
    {
        dotTransform = dotObject.transform;
    }

    private void Update()
    {
        // ���������, ������ �� ������� �������
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // �������� ������� �������� �������
            OnRotate?.Invoke();
        }
    }

    // ����� ��� ������ ������ ������� �������� �������
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