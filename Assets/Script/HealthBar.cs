using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar instance;
    public Image health;
    void Awake()
    {
        instance = this;
    }

    public void UpdateHealthBar(float demage)
    {
        health.fillAmount = demage;

        if (health.fillAmount < 0.5f)
        {
            health.color = Color.red;
        }
        else
        {
            health.color = Color.green;
        }
    }
}
