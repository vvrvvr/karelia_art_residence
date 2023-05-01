using UnityEngine;
using TMPro;
using DG.Tweening;

public class DotSensivity : MonoBehaviour
{
    private Dot dot;
    [SerializeField] private TextMeshProUGUI sensivityText;
    [SerializeField] private float scaleTime = 0.2f;
    [SerializeField] private float additionalVal = 100f;
    // private float speedM;

    void Start()
    {
        dot = GetComponent<Dot>();
        if(PlayerPrefs.HasKey("speedMultiplier"))
        {
            dot.speedMultiplier = PlayerPrefs.GetFloat("speedMultiplier");
            sensivityText.text = dot.speedMultiplier.ToString();
        }
        else
        {
            PlayerPrefs.SetFloat("speedMultiplier", dot.speedMultiplier);
            sensivityText.text = dot.speedMultiplier.ToString();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeSpeedMultiplier(additionalVal, true);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ChangeSpeedMultiplier(additionalVal, false);
        }
    }

    public void ChangeSpeedMultiplier(float val, bool isIncrease)
    {
        if(isIncrease)
        {
            dot.speedMultiplier += val;
            PlayerPrefs.SetFloat("speedMultiplier", dot.speedMultiplier);
            sensivityText.text = dot.speedMultiplier.ToString();
            ScaleText();
        }
        else
        {
            dot.speedMultiplier -= val;
            if(dot.speedMultiplier < 0)
                dot.speedMultiplier = 0;
            PlayerPrefs.SetFloat("speedMultiplier", dot.speedMultiplier);
            sensivityText.text = dot.speedMultiplier.ToString();
            ScaleText();
        }
    }

    private void ScaleText()
    {
        sensivityText.transform.DOScale(1.2f, scaleTime);
        sensivityText.transform.DOScale(1f, scaleTime).SetDelay(scaleTime);
    }
}
