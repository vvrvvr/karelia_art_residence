using UnityEngine;

public class DisplaysManager : MonoBehaviour
{
    void Start()
    {
        GameManager.singleton.DisplaysCount = Display.displays.Length;

        //Debug.Log(Display.displays.Length);
        //Display.displays[0].Activate(1024, 768, 60);
        //Display.displays[1].Activate(800, 600, 60);
        //GameManager.singleton.DisplaysCount = Display.displays.Length;

        // Display.displays[0] is the primary, default display and is always ON, so start at index 1.
        // Check if additional displays are available and activate each.

        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate(800, 600, 60);
            
        }

    }
}
