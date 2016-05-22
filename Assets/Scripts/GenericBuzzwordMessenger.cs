using UnityEngine;
using System.Collections.Generic;

public class GenericBuzzwordMessenger : MonoBehaviour, IMessenger
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
    private int lettersRemaining;
    
	void Awake ()
    {
        letters = new List<BuzzwordLetterMovement>();
        for(int i = 0; i < buzzword.Length; i++)
        {
            GameObject newLetter = Instantiate(letterPrefab, transform.position + (i * letterDistance * Vector3.right * (float)direction * -1), Quaternion.identity) as GameObject;
            newLetter.GetComponentInChildren<TextMesh>().text = buzzword[i].ToString();
            newLetter.transform.SetParent(transform);
            letters.Add(newLetter.GetComponent<BuzzwordLetterMovement>());
        }
        lettersRemaining = letters.Count;
        for(int i = 0; i < letters.Count; i++)
        {
            letters[i].SetDirectionChangeTime(directionChangeTime);
            letters[i].SetHorizontalDirection(direction);
            letters[i].SetSpeed(speed);
            letters[i].GetComponent<BuzzwordLetterMessenger>().SetParentMessenger(this);
            for(int j = i + 1; j < letters.Count; j++)
            {
                Physics2D.IgnoreCollision(
                    letters[i].GetComponentInChildren<Hitbox>().HitboxCollider,
                    letters[j].GetComponentInChildren<Hurtbox>().HurtboxCollider);
                Physics2D.IgnoreCollision(
                    letters[i].GetComponentInChildren<Hurtbox>().HurtboxCollider,
                    letters[j].GetComponentInChildren<Hitbox>().HitboxCollider);
            }
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
                if(letters[currentLetterToActivate] != null)
                {
                    letters[currentLetterToActivate].Activate();
                }
                currentLetterToActivate++;
            }
        }
	}

    public void Invoke(string msg, object[] args)
    {
        switch (msg)
        {
            case "LetterDestroyed":
                lettersRemaining--;
                if(lettersRemaining <= 0)
                {
                    Destroy(gameObject);
                }
                break;
            default:
                break;
        }
    }
}
