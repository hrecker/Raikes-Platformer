using UnityEngine;

public class EdgeTextureController : MonoBehaviour
{
    public GameObject edgeTexture;
    public bool topEdge;
    public bool bottomEdge;
    public bool rightEdge;
    public bool leftEdge;
    
    void Awake()
    {
        if(topEdge)
        {
            spawnEdgeTexture(VerticalDirection.UP);
        }
        if(bottomEdge)
        {
            spawnEdgeTexture(VerticalDirection.DOWN);
        }
        if(rightEdge)
        {
            spawnEdgeTexture(HorizontalDirection.RIGHT);
        }
        if(leftEdge)
        {
            spawnEdgeTexture(HorizontalDirection.LEFT);
        }
    }

    private void spawnEdgeTexture(VerticalDirection verticalDirection)
    {
        GameObject edgeInstance = spawnEdgeTexture();
        edgeInstance.GetComponent<EdgeTexture>().vertical = true;
        edgeInstance.GetComponent<EdgeTexture>().horizontal = false;
        edgeInstance.GetComponent<EdgeTexture>().verticalDirection = verticalDirection;
        edgeInstance.GetComponent<MaterialTiling>().xTile = transform.localScale.x;
    }

    private void spawnEdgeTexture(HorizontalDirection horizontalDirection)
    {
        GameObject edgeInstance = spawnEdgeTexture();
        edgeInstance.GetComponent<EdgeTexture>().vertical = false;
        edgeInstance.GetComponent<EdgeTexture>().horizontal = true;
        edgeInstance.GetComponent<EdgeTexture>().horizontalDirection = horizontalDirection;
        edgeInstance.GetComponent<MaterialTiling>().xTile = transform.localScale.y;
    }

    private GameObject spawnEdgeTexture()
    {
        GameObject edgeInstance = Instantiate(edgeTexture, transform.position, Quaternion.identity) as GameObject;
        edgeInstance.transform.parent = transform;
        edgeInstance.GetComponent<EdgeTexture>().parentTexture = transform;
        edgeInstance.GetComponent<EdgeTexture>().grassYScale = 2;
        edgeInstance.GetComponent<MaterialTiling>().yTile = 0.99f;
        edgeInstance.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(0, 0.01f);
        return edgeInstance;
    }
}
