using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    public int DisplaysCount = 1;
    public bool Disp2HasControl = true;

    private void Awake()
    {
        if (!singleton)
        {
            singleton = this;
            //DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
