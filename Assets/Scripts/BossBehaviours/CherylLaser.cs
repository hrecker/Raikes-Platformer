using UnityEngine;

public class CherylLaser : MonoBehaviour
{
    public float startYScale;
    public float fullYScale;
    public float xScale;
    public float expandDelay;
    public float lifetime;

    private BoxCollider2D boxCollider;
    private float currentDelayPassed;

    void Start()
    {
        Destroy(gameObject, lifetime);
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
        else
        {
            boxCollider.enabled = true;
            transform.localScale = new Vector2(xScale, fullYScale);
        }
    }
}