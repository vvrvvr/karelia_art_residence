using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTest : MonoBehaviour
{
    private EventManager m_EventManager;

    private void Start()
    {
        m_EventManager = EventManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            m_EventManager.PlayerDeath();
        }
    }
}
