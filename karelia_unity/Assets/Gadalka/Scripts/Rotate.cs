using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    // Update is called once per frame
    void Update()
    {
        Vector3 yAxis = new Vector3(0, 1, 0);
        transform.RotateAround(Vector3.zero, yAxis, speed * Time.deltaTime);
    }
}
