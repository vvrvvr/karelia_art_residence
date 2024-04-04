using UnityEngine;

public class OptimisationManager : MonoBehaviour
{
    public GameObject fogModels;
    public GameObject finishLight;
    private int isFog = 1;
    private int isLight = 1;

    void Start()
    {
        if(PlayerPrefs.HasKey("isFogModels"))
        {
            isFog = PlayerPrefs.GetInt("isFogModels");
            if (isFog == 0)
            {
                fogModels.SetActive(false);
            }
            else
            {
                fogModels.SetActive(true);
            }
        }
        else
        {
            PlayerPrefs.SetInt("isFogModels", 1);
        }
        
        if(PlayerPrefs.HasKey("isLight"))
        {
            isLight = PlayerPrefs.GetInt("isLight");
            if (isLight == 0)
            {
                finishLight.SetActive(false);
            }
            else
            {
                finishLight.SetActive(true);
            }
        }
        else
        {
            PlayerPrefs.SetInt("isLight", 1);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (isFog == 1)
                {
                    isFog = 0;
                    fogModels.SetActive(false);
                    PlayerPrefs.SetInt("isFogModels", isFog);
                }
                else
                {
                    isFog = 1;
                    fogModels.SetActive(true);
                    PlayerPrefs.SetInt("isFogModels", isFog);
                }
            }
        }

        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (isLight == 1)
                {
                    isLight = 0;
                    finishLight.SetActive(false);
                    PlayerPrefs.SetInt("isLight", isLight);
                }
                else
                {
                    isLight = 1;
                    finishLight.SetActive(true);
                    PlayerPrefs.SetInt("isLight", isLight);
                }
            }
        }
    }
}
