using UnityEngine;

public class SpriteAlternator : MonoBehaviour
{
    public Sprite[] sprites;
    public float switchTime; // how long until the sprite switches

    private float currentTimePassed;
    private int currentIndex; // what index in sprite array is currently being used
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentIndex = -1;
        SwitchSprite();
    }
	
	void Update()
    {
        currentTimePassed += Time.deltaTime;
        if(currentTimePassed >= switchTime)
        {
            currentTimePassed = 0;
            SwitchSprite();
        }
	}

    public void SwitchSprite()
    {
        currentIndex = (currentIndex + 1) % sprites.Length;
        spriteRenderer.sprite = sprites[currentIndex];
    }
}
