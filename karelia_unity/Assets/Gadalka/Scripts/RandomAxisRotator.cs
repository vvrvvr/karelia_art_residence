using UnityEngine;

public class RandomAxisRotator : MonoBehaviour
{
    public float speed = 1.0f;
    public float timeMin = 1.0f;
    public float timeMax = 3.0f;

    private float timer;
    private float rotationTime;
    private Vector3 currentAxis;

    void Start()
    {
        SetNewRotationTime();
        ChooseRandomAxis();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= rotationTime)
        {
            ChooseRandomAxis();
            SetNewRotationTime();
        }

        transform.Rotate(currentAxis, speed * Time.deltaTime);
    }

    void SetNewRotationTime()
    {
        rotationTime = Random.Range(timeMin, timeMax);
        timer = 0.0f;
    }

    void ChooseRandomAxis()
    {
        currentAxis = Random.onUnitSphere;
    }
}