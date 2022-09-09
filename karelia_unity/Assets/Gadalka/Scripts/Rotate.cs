using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    // Update is called once per frame
    void Update()
    {
        // Rotate the object around its local X axis at 1 degree per second
        transform.Rotate(Vector3.up * Time.deltaTime * speed);
    }
}
