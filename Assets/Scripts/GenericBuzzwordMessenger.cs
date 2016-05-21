using UnityEngine;
using System.Collections.Generic;

public class GenericBuzzwordMessenger : MonoBehaviour
{
    public float speed;
    public string buzzword;
    public GameObject letterPrefab;
    public float directionChangeTime;
    public HorizontalDirection direction;
    public float letterDistance;

    private List<BuzzwordLetterMovement> letters;
    private int numLetters;
    private int currentLetterToActivate; // index of next letter to activate
    private float currentTimePassed;
    
	void Awake ()
    {
        letters = new List<BuzzwordLetterMovement>();
        for(int i = 0; i < buzzword.Length; i++)
        {
            GameObject newLetter = Instantiate(letterPrefab, transform.position + (i * letterDistance * Vector3.right * (float)direction * -1), Quaternion.identity) as GameObject;
            newLetter.GetComponentInChildren<TextMesh>().text = buzzword[i].ToString();
            letters.Add(newLetter.GetComponent<BuzzwordLetterMovement>());
        }
        foreach(BuzzwordLetterMovement letter in letters)
        {
            letter.SetDirectionChangeTime(directionChangeTime);
            letter.SetHorizontalDirection(direction);
            letter.SetSpeed(speed);
        }
        letters[0].Activate();
        currentLetterToActivate = 1;
	}
	
	void Update ()
    {
        if (currentLetterToActivate < letters.Count)
        {
            currentTimePassed += Time.deltaTime;
            if (currentTimePassed >= directionChangeTime)
            {
                currentTimePassed = 0;
                letters[currentLetterToActivate].Activate();
                currentLetterToActivate++;
            }
        }
	}
}
