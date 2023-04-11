using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; } // ��������� ���������
    public event Action OnRotate; // ������� ��� �������� �������
    public bool isDead = false;

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
            isDead = true;
            Debug.Log("death");
        }
        
    }
}