using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyWall : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color activeColor;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = defaultColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            spriteRenderer.color = activeColor;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            spriteRenderer.color = defaultColor;
        }
    }
}
