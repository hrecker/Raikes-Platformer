using UnityEngine;

public class MiniLeenKiatMessenger : MonoBehaviour, IMessenger
{
    public Hitbox bodyHitBox;
    public Hurtbox bodyHurtBox;
    public Hurtbox headHurtBox;
    private Turnbox turnBox;
    public Sprite[] shortSprites;

    private MiniLeenKiatMovement movement;
    private BoxCollider2D bodyCollider;
	private SpriteAlternator spriteAlternator;
	public int pointsReceived = 10;

    void Start()
    {
        spriteAlternator = GetComponent<SpriteAlternator>();
        movement = GetComponent<MiniLeenKiatMovement>();
        bodyCollider = GetComponent<BoxCollider2D>();
        turnBox = GetComponentInChildren<Turnbox>();
    }

    public void Invoke(Message msg, object[] args)
    {
        switch (msg)
        {
            case Message.HIT_OTHER:
                //Debug.Log("MiniLeenKiat hit another object");
                break;
            case Message.HIT_BY_OTHER:
                //Debug.Log("MiniLeenKiat received hit");
                if(!movement.Squished)
                {
                    squish();
                }
                else
                {
					SceneMessenger.Instance.Invoke (Message.POINTS_RECEIVED, new object[] { this.pointsReceived });
                    Destroy(gameObject);
                }
                break;
            case Message.TURN:
                movement.Turn();
                break;
            case Message.BOUNCE:
                movement.Jump();
                break;
        }
    }

    private void squish()
    {
        movement.Squish();
        // move head hurtbox and body hitbox/hurtbox down
        headHurtBox.BoxColliders[0].offset /= 2;
        bodyHitBox.BoxColliders[0].offset /= 2;
        bodyHurtBox.BoxColliders[0].offset /= 2;

        // shrink body hitbox and hurtbox, turnbox, and regular collider
        Vector2 hitboxBoundSize = ((BoxCollider2D)bodyHitBox.BoxColliders[0]).size;
        ((BoxCollider2D)bodyHitBox.BoxColliders[0]).size = new Vector2(hitboxBoundSize.x, hitboxBoundSize.y / 2);
        Vector2 hurtboxBoundSize = ((BoxCollider2D)bodyHurtBox.BoxColliders[0]).size;
        ((BoxCollider2D)bodyHurtBox.BoxColliders[0]).size = new Vector2(hurtboxBoundSize.x, hurtboxBoundSize.y / 2);
        Vector2 bodyBoundSize = bodyCollider.size;
        bodyCollider.size = new Vector2(bodyBoundSize.x, bodyBoundSize.y / 2);
        foreach(Collider2D turnBoxCollider in turnBox.BoxColliders)
        {
            Vector2 turnboxBoundSize = ((BoxCollider2D)turnBoxCollider).size;
            ((BoxCollider2D)turnBoxCollider).size = new Vector2(turnboxBoundSize.x, turnboxBoundSize.y / 2);
        }

        // change sprites
        spriteAlternator.sprites = shortSprites;
        spriteAlternator.SwitchSprite();
        spriteAlternator.switchTime /= 2;
    }
}
