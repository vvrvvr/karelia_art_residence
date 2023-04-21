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

    private void Rotate(bool dir)
    {
        var rotationValue = 0;
        if (dir)
            rotationValue = 90;
        else
            rotationValue = - 90;

        isRotating = true;
        ChangeTransparencyWithTween();
        // используем метод DOLocalRotate для поворота объекта в локальных координатах
        sprites.DOLocalRotate(new Vector3(0, 0, sprites.localRotation.eulerAngles.z + rotationValue), rotationTime)
            .OnComplete(() => { isRotating = false; });
        collidersTriggers.DOLocalRotate(new Vector3(0, 0, collidersTriggers.localRotation.eulerAngles.z + rotationValue), 0)
            .OnComplete(() => { checkColliders.CheckCollision(); });
        colliders.DOLocalRotate(new Vector3(0, 0, colliders.localRotation.eulerAngles.z + rotationValue), 0);
    }
    private void StartRotate(bool isLeft)
    {
        if (!isRotating)
        {
            Rotate(isLeft);
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
