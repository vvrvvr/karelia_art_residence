using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelsManager : MonoBehaviour
{
    private GameObject[] modelArray;
    [SerializeField] private GameObject models;
    [SerializeField] private Texture[] modelsTextures = new Texture[0];
    [SerializeField] private Material material;
    private int currentCount = 0;
    private int currentTextureCount = 0;
    private int maxCount;
    void Start()
    {
        CollectModels();
        currentCount = Random.Range(0, maxCount);
        currentTextureCount = Random.Range(0, modelsTextures.Length-1);
    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.U))
        {
            GetRandomModel();
        }
    }

    private void CollectModels()
    {
        if (models != null)
        {
            int childCount = models.transform.childCount;
            maxCount = childCount;
            modelArray = new GameObject[childCount];
            for (int i = 0; i < childCount; i++)
            {
                GameObject child = models.transform.GetChild(i).gameObject;
                modelArray[i] = child;
                child.SetActive(false);
            }
        }
        else
        {
            Debug.Log("модели не найдены");
        }
    }
    public void GetRandomModel()
    {
        int newCount = Random.Range(0, modelArray.Length -1 );

        while(newCount == currentCount)
        {
            newCount = Random.Range(0, modelArray.Length - 1);
        }
        GetRandomTexture();
        modelArray[currentCount].SetActive(false);
        currentCount = newCount;
        modelArray[newCount].SetActive(true);
    }
    public void GetRandomTexture()
    {
        int newCount = Random.Range(0, modelsTextures.Length - 1);

        while (newCount == currentTextureCount)
        {
            newCount = Random.Range(0, modelsTextures.Length - 1);
        }
        currentTextureCount = newCount;
        material.mainTexture = modelsTextures[newCount];
    }
}
