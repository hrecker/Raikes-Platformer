using UnityEngine;

public class MaterialTiling : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public float xTile;
    public float yTile;

	void Start ()
    {
        Material material = meshRenderer.material;
        if (xTile == 0 && yTile == 0)
        {
            xTile = transform.localScale.x;
            yTile = transform.localScale.y;
        }
        material.mainTextureScale = new Vector2(xTile, yTile);
    }
}
