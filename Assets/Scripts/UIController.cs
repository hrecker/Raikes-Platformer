using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image healthBar;
    private Image[] healthUIImages;
    private Image[] armorUIImages;
	public Text scoreText;
	private int points = 0;
	public int Points
	{
		get { return points; }
		set { points = value; }
	}

    void Start()
    {
		Image[] uiImages = healthBar.GetComponentsInChildren<Image> ();
		healthUIImages = new Image[3];
		armorUIImages = new Image[3];
		Array.Copy (uiImages, 1, healthUIImages, 0, 3);
		Array.Copy (uiImages, 1 + healthUIImages.Length, armorUIImages, 0, 3);

		foreach (Image armorUIImage in armorUIImages) {
			armorUIImage.gameObject.SetActive (false);
		}

		SceneMessenger.Instance.AddListener (Message.HEALTH_UPDATED, new SceneMessenger.HealthCallback (UpdateHealthUI));
		SceneMessenger.Instance.AddListener (Message.POINTS_RECEIVED, new SceneMessenger.PointsCallback (UpdateScoreUI));

		DontDestroyOnLoad (gameObject);
    }

    public void UpdateHealthUI(int currentHealth, int maxHealth, int currentArmor, int maxArmor)
    {
        for(int i = 0; i < healthUIImages.Length; i++)
        {
            if(currentHealth <= i)
            {
                healthUIImages[i].gameObject.SetActive(false);
            }
            else
            {
                healthUIImages[i].gameObject.SetActive(true);
            }
        }
        for (int i = 0; i < armorUIImages.Length; i++)
        {
            if (currentArmor <= i)
            {
                armorUIImages[i].gameObject.SetActive(false);
            }
            else
            {
                armorUIImages[i].gameObject.SetActive(true);
            }
        }
    }

	public void UpdateScoreUI(int receivedPoints)
    {
		points += receivedPoints;
		string pointsString = string.Format ("{0}", points);
		//The score text should have 7 digits.
		while (pointsString.Length < 7)
        {
			pointsString = "0" + pointsString;
		}
		scoreText.text = pointsString;
	}

}
