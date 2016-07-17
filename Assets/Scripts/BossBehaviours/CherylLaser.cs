using UnityEngine;

public class CherylLaser : MonoBehaviour
{
    public float startYScale;
    public float fullYScale;
    public float xScale;
    public float expandDelay;
    public float activeLifetime;

    private BoxCollider2D boxCollider;
    private float currentDelayPassed;

    void Start()
    {
        transform.localScale = new Vector2(xScale, startYScale);
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
    }

    void Update()
    {
        if(currentDelayPassed < expandDelay)
        {
            currentDelayPassed += Time.deltaTime;
        }
        else // Expand the laser
        {
            boxCollider.enabled = true;
            transform.localScale = new Vector2(xScale, fullYScale);
            Destroy(gameObject, activeLifetime);
        }
    }
}