using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image healthBar;
    private Image[] healthUIImages;
	public Image scoreImage;
	public Text scoreText;
	private int points = 0;

    void Awake()
    {
        healthUIImages = healthBar.GetComponentsInChildren<Image>();

        SceneMessenger sceneMessenger = GameObject.FindGameObjectWithTag("SceneMessenger").GetComponent<SceneMessenger>();
        sceneMessenger.AddListener (Message.HEALTH_UPDATED, new SceneMessenger.HealthCallback(UpdateHealthUI));
		sceneMessenger.AddListener (Message.POINTS_RECEIVED, new SceneMessenger.PointsCallback (UpdateScoreUI));
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

	public void UpdateScoreUI(int receivedPoints) {
		points += receivedPoints;
		string pointsString = string.Format ("{0}", points);
		//The score text should have 7 digits.
		while (pointsString.Length < 7) {
			pointsString = "0" + pointsString;
		}
		scoreText.text = pointsString;
	}

}
