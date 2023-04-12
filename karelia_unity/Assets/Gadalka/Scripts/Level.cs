using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Vector3[] sectionPositions = new Vector3[0];
    [SerializeField] private GameObject[] sections = new GameObject[0];
    [SerializeField] private GameObject Sections;
    
    void Start()
    {
        Helpers helpers = new Helpers();
        foreach (var position in sectionPositions)
        {
            var currentSection = helpers.GetRandomArrayElement(sections);
            Instantiate(currentSection, transform.TransformPoint(position), Quaternion.identity, transform);
            //currentSection.transform.SetParent(Sections.transform);
        }
    }
}
