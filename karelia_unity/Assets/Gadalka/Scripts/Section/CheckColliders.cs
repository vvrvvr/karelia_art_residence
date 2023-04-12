using UnityEngine;
using System.Collections;


public class CheckColliders : MonoBehaviour
{
    private GameObject[] childObjects;
    public string playerTag = "Player";
    private void Start()
    {
        int childCount = transform.childCount;
        childObjects = new GameObject[childCount];

        // Добавляем ссылки на дочерние объекты в массив
        for (int i = 0; i < childCount; i++)
        {
            childObjects[i] = transform.GetChild(i).gameObject;
            childObjects[i].SetActive(false);
        }
    }

    
    public void CheckCollision()
    {
        foreach (GameObject collider in childObjects)
        {
            collider.SetActive(true);
        }
        StartCoroutine(WaitAndTurnOff());
    }
    private IEnumerator WaitAndTurnOff()
    {
        yield return new WaitForSeconds(0.05f);
        foreach (GameObject collider in childObjects)
        {
            collider.SetActive(false);
        }
    }

}
