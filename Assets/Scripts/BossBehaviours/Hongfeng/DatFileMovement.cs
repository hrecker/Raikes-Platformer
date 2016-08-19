using UnityEngine;

public class DatFileMovement : MonoBehaviour
{
    public float speed;

    public void Update()
    {
        transform.position += Vector3.down * speed;
    }
}
