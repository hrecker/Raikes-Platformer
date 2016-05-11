using UnityEngine;

public class SpriteAlternator : MonoBehaviour
{
    public Sprite[] sprites;
    public float switchTime; // how long until the sprite switches

    private float currentTimePassed;
    private int currentIndex; // what index in sprite array is currently being used
    private SpriteRenderer spriteRenderer;
    private bool active;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        active = true;
    }

    void Start()
    {
        currentIndex = -1;
        SwitchSprite();
    }
	
	void Update()
    {
        if (active)
        {
            currentTimePassed += Time.deltaTime;
            if (currentTimePassed >= switchTime)
            {
                currentTimePassed = 0;
                SwitchSprite();
            }
        }
	}

    public void SwitchSprite()
    {
        currentIndex = (currentIndex + 1) % sprites.Length;
        spriteRenderer.sprite = sprites[currentIndex];
    }

    public void SetActive(bool active)
    {
        this.active = active;
        currentTimePassed = 0;
        currentIndex = -1;
        SwitchSprite();
    }
}
