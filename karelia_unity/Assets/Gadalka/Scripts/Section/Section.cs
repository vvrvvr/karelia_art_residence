using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Section : MonoBehaviour
{
    public float rotationTime = 0.5f; // время в секундах, которое займет поворот
    private bool isRotating = false; // флаг, указывающий, вращается ли объект уже
    private EventManager eventManager;
    [SerializeField] private Transform sprites;
    [SerializeField] private Transform collidersTriggers;
    [SerializeField] private Transform colliders;
    [SerializeField] private CheckColliders checkColliders;
    [SerializeField] private Material spriteMaterial;
    private float fadeTime;


    private void Start()
    {
        fadeTime = rotationTime / 2;
        eventManager = FindObjectOfType<EventManager>();
        eventManager.OnRotate += StartRotate;
    }

    private void OnDestroy()
    {
        eventManager.OnRotate -= StartRotate;
    }

    private void Rotate()
    {
        isRotating = true;
        ChangeTransparencyWithTween();
        // используем метод RotateAround для поворота объекта с помощью DOTween
        sprites.DORotate(new Vector3(0, 0, sprites.rotation.eulerAngles.z - 90), rotationTime)
            .OnComplete(() => { isRotating = false; });
        collidersTriggers.DORotate(new Vector3(0, 0, collidersTriggers.rotation.eulerAngles.z - 90), 0)
            .OnComplete(() => { checkColliders.CheckCollision(); });
        colliders.DORotate(new Vector3(0, 0, colliders.rotation.eulerAngles.z - 90), 0);
    }
    private void StartRotate()
    {
        if (!isRotating)
        {
            Rotate();
        }
    }

    public void ChangeTransparencyWithTween()
    {
        var tween1 = spriteMaterial.DOFade(0.4f, fadeTime-0.05f);
        var tween2 = spriteMaterial.DOFade(1f, fadeTime-0.1f).SetDelay(fadeTime);

        // Последовательность из двух твинов, которые запускаются один за другим
        Sequence sequence = DOTween.Sequence();
        sequence.Append(tween1);
        sequence.Append(tween2);
    }
}
