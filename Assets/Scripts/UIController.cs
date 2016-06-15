using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image healthBar;
    private Image[] healthUIImages;

    void Awake()
    {
        healthUIImages = healthBar.GetComponentsInChildren<Image>();

        SceneMessenger sceneMessenger = GameObject.FindGameObjectWithTag("SceneMessenger").GetComponent<SceneMessenger>();
        sceneMessenger.AddListener(Message.HEALTH_UPDATED, new SceneMessenger.HealthCallback(UpdateHealthUI));
    }

    public void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        for(int i = 1; i < healthUIImages.Length; i++)
        {
            if(currentHealth < i)
            {
                healthUIImages[i].gameObject.SetActive(false);
            }
            else
            {
                healthUIImages[i].gameObject.SetActive(true);
            }
        }
    }
}
