using UnityEngine;

public class BlueScreenComputerMessenger : MonoBehaviour, IMessenger
{
    private Hitbox[] hitBoxes;
    private Hurtbox[] hurtBoxes;
    private Turnbox turnBox;
    private SpriteRenderer spriteRenderer;
	private BlueScreenComputerMovement movement;
	public int pointsReceived = 10;

    public Sprite movingSprite;
    public Sprite stoppedSprite;

    void Start()
    {
        hitBoxes = GetComponentsInChildren<Hitbox>();
        hurtBoxes = GetComponentsInChildren<Hurtbox>();
        turnBox = GetComponentInChildren<Turnbox>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponent<BlueScreenComputerMovement>();
        Invoke(Message.STARTED_MOVEMENT, null);
    }

    public void Invoke(Message msg, object[] args)
    {
		
        switch (msg)
        {
            case Message.HIT_OTHER:
                Debug.Log("Blue screen computer hit another object");
                break;
            case Message.HIT_BY_OTHER:
                Debug.Log("Blue screen compuer received hit");
				Destroy(gameObject);
				SceneMessenger.Instance.Invoke (Message.POINTS_RECEIVED, new object[] { this.pointsReceived });
                break;
            case Message.TURN:
                movement.Turn();
                break;
            case Message.BOUNCE:
                movement.Jump();
                break;

            case Message.STOPPED_MOVEMENT:
                ActivateHitBox();
                turnBox.Deactivate();
                spriteRenderer.sprite = stoppedSprite;
                break;
            case Message.STARTED_MOVEMENT:
                ActivateHurtBox();
                turnBox.Activate();
                spriteRenderer.sprite = movingSprite;
                break;
             
        }
    }

    private void ActivateHitBox()
    {
        foreach(Hitbox hitbox in hitBoxes)
		{
            hitbox.Activate();
        }
        foreach (Hurtbox hurtbox in hurtBoxes)
        {
            hurtbox.Deactivate();
        }
    }

    private void ActivateHurtBox()
    {
        foreach (Hitbox hitbox in hitBoxes)
		{
            hitbox.Deactivate();
        }
        foreach (Hurtbox hurtbox in hurtBoxes)
        {
            hurtbox.Activate();
        }
    }
}
