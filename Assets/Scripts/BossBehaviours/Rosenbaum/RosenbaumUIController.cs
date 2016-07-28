using UnityEngine;
using UnityEngine.UI;

public class RosenbaumUIController : MonoBehaviour
{
    public Image bossHealthBar;
    public Image attackDelayBar;
    public Text attackWarningText;

    private bool warningActive;
    private float warningDuration;
    private float currentTimePassed;
    
	void Start ()
    {
        setGraphicAlpha(attackDelayBar, 0);
        setGraphicAlpha(attackWarningText, 0);
	}
	
	void Update ()
    {
	    if(warningActive)
        {
            currentTimePassed += Time.deltaTime;
            if(currentTimePassed >= warningDuration)
            {
                deactivateWarning();
            }
            else
            {
                setBarWidth(attackDelayBar, warningDuration - currentTimePassed, warningDuration);
            }
        }
	}

    public void ActivateWarning(float duration)
    {
        setGraphicAlpha(attackDelayBar, 1);
        setGraphicAlpha(attackWarningText, 1);
        setBarWidth(attackDelayBar, 1, 1);
        warningActive = true;
        warningDuration = duration;
        currentTimePassed = 0;
    }

    private void deactivateWarning()
    {
        setGraphicAlpha(attackDelayBar, 0);
        setGraphicAlpha(attackWarningText, 0);
        warningActive = false;
    }

    //Set width of boss health bar
    public void SetBossHealthWidth(int currentHealth, int maxHealth)
    {
        setBarWidth(bossHealthBar, currentHealth, maxHealth);
    }

    // used to hide and show UI objects
    private void setGraphicAlpha(Graphic g, float alpha)
    {
        g.color = new Color(g.color.r, g.color.g, g.color.b, alpha);
    }

    private void setBarWidth(Graphic bar, float currentValue, float maxValue)
    {
        bar.GetComponent<RectTransform>().localScale = new Vector3(currentValue / maxValue, 1, 1);
    }
}
