using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Vector3[] sectionPositions = new Vector3[0];
    [SerializeField] private GameObject[] sections = new GameObject[0];
    private List<GameObject> sectionList = new List<GameObject>();

    void Start()
    {
        
    }

    public void ClearSections()
    {
        foreach (var section in sectionList)
        {
            Destroy(section);
        }
        sectionList.Clear();
    }

    private void OnDisable()
    {
        ClearSections();
    }
    private void OnEnable()
    {
        Helpers helpers = new Helpers();
        foreach (var position in sectionPositions)
        {
            var currentSection = helpers.GetRandomArrayElement(sections);
            var newSection = Instantiate(currentSection, transform.TransformPoint(position), Quaternion.identity, transform);
            sectionList.Add(newSection);
        }
    }


}
