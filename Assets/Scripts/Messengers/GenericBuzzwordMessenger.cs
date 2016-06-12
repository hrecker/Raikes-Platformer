using UnityEngine;
using System.Collections.Generic;

public class GenericBuzzwordMessenger : MonoBehaviour, IMessenger, IDirected
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

    public HorizontalDirection horizontalDirection
    {
        get { return direction; }
        set { direction = value; }
    }

    public VerticalDirection verticalDirection
    {
        get { return VerticalDirection.NONE; }
        set { }
    }

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
            letters[i].horizontalDirection = direction;
            letters[i].SetSpeed(speed);
            letters[i].GetComponent<BuzzwordLetterMessenger>().SetParentMessenger(this);
            for(int j = i + 1; j < letters.Count; j++)
            {
                foreach(Collider2D hitboxCollider in letters[i].GetComponentInChildren<Hitbox>().BoxColliders)
                {
                    foreach(Collider2D hurtboxCollider in letters[j].GetComponentInChildren<Hurtbox>().BoxColliders)
                    {
                        Physics2D.IgnoreCollision(hitboxCollider, hurtboxCollider);
                    }
                }
                foreach (Collider2D hurtboxCollider in letters[i].GetComponentInChildren<Hurtbox>().BoxColliders)
                {
                    foreach (Collider2D hitboxCollider in letters[j].GetComponentInChildren<Hitbox>().BoxColliders)
                    {
                        Physics2D.IgnoreCollision(hitboxCollider, hurtboxCollider);
                    }
                }
               /* Physics2D.IgnoreCollision(
                    letters[i].GetComponentInChildren<Hitbox>().BoxColliders[0],
                    letters[j].GetComponentInChildren<Hurtbox>().BoxColliders[0]);
                Physics2D.IgnoreCollision(
                    letters[i].GetComponentInChildren<Hurtbox>().BoxColliders[0],
                    letters[j].GetComponentInChildren<Hitbox>().BoxColliders[0]);*/
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

    public void Invoke(Message msg, object[] args)
    {
        switch (msg)
        {
            case Message.LETTER_DESTROYED:
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
