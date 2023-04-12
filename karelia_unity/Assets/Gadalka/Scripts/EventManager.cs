using UnityEngine;
using System;
using DG.Tweening;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; } // ��������� ���������
    public event Action OnRotate; // ������� ��� �������� �������
    public bool isDead = false;
    [SerializeField] private GameObject dotObject;
    private Transform dotTransform;
    [SerializeField] private Dot dotScript;
    [SerializeField] private GameObject deathParticle;
    [SerializeField] private Camera display2Cam;
    [SerializeField] private Display2Manager display2Manager;

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
            display2Cam.DOShakePosition(0.5f, 0.5f, 10, 45);
            display2Manager.ResetPlayerPosition();
            Debug.Log("death");
        }
        
    }
}