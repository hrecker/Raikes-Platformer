using UnityEngine;

public class PlayerSpriteChanger : MonoBehaviour
{
    public Sprite standSprite;
    public Sprite jumpSprite;
    public Sprite fallSprite;
    public float switchTime; // how long until the sprite switches when flashing

    private float currentTimePassed;
    private Sprite currentSprite;
    private bool flashActive; // Is the player flashing

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    public void SetSprite(PlayerState playerState)
    {
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        switch(playerState)
        {
            case PlayerState.FALL:
                currentSprite = fallSprite;
                if(!flashActive)
                {
                    spriteRenderer.sprite = fallSprite;
                }
                break;
            case PlayerState.JUMP:
                currentSprite = jumpSprite;
                if (!flashActive)
                {
                    spriteRenderer.sprite = jumpSprite;
                }
                break;
            case PlayerState.STAND:
                currentSprite = standSprite;
                if (!flashActive)
                {
                    spriteRenderer.sprite = standSprite;
                }
                break;
        }
    }

    public void FlipSprite(HorizontalDirection direction)
    {
        if(direction == HorizontalDirection.LEFT)
        {
            spriteRenderer.flipX = true;
        }
        else if(direction == HorizontalDirection.RIGHT)
        {
            spriteRenderer.flipX = false;
        }
    }

    void Update()
    {
        if (flashActive)
        {
            currentTimePassed += Time.deltaTime;
            if (currentTimePassed >= switchTime)
            {
                currentTimePassed = 0;
                flashSprite();
            }
        }
    }

    public void ActivateFlash()
    {
        currentTimePassed = 0;
        flashActive = true;
        flashSprite();
    }

    public void DeactivateFlash()
    {
        flashActive = false;
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = currentSprite;
        }
        currentTimePassed = 0;
    }

    private void flashSprite()
    {
        if(spriteRenderer.sprite == null)
        {
            spriteRenderer.sprite = currentSprite;
        }
        else
        {
            spriteRenderer.sprite = null;
        }
    }
    
}
