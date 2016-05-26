using UnityEngine;

public class PlayerSpriteChanger : MonoBehaviour
{
    public Sprite standSprite;
    public Sprite jumpSprite;
    public Sprite fallSprite;

    private SpriteRenderer spriteRenderer;

    public void SetSprite(PlayerState playerState)
    {
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        switch(playerState)
        {
            case PlayerState.FALL:
                spriteRenderer.sprite = fallSprite;
                break;
            case PlayerState.JUMP:
                spriteRenderer.sprite = jumpSprite;
                break;
            case PlayerState.STAND:
                spriteRenderer.sprite = standSprite;
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
}
