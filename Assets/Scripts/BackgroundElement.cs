using System.Linq;
using UnityEngine;

public class BackgroundElement : MonoBehaviour
{
    private Transform[] spriteTransforms;
    private float spriteLength;

    void Awake()
    {
        spriteTransforms = GetComponentsInChildren<Transform>().Skip(1).ToArray();
        spriteLength = Mathf.Abs(spriteTransforms[0].position.x - spriteTransforms[1].position.x);
        //spriteLength = spriteTransforms[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;
    }

    public float GetSpriteLength()
    {
        return spriteLength;
    }

    // Move rightmost sprite to left
    public void WrapToLeft()
    {
        int rightMostIndex = 0;
        for(int i = 1; i < spriteTransforms.Length; i++)
        {
            if(spriteTransforms[i].position.x > spriteTransforms[rightMostIndex].position.x)
            {
                rightMostIndex = i;
            }
        }

        spriteTransforms[rightMostIndex].position =
            new Vector3(spriteTransforms[rightMostIndex].position.x - (spriteTransforms.Length * spriteLength),
            spriteTransforms[rightMostIndex].position.y, spriteTransforms[rightMostIndex].position.z);
    }

    // Move leftmost sprite to right
    public void WrapToRight()
    {
        int leftMostIndex = 0;
        for (int i = 1; i < spriteTransforms.Length; i++)
        {
            if (spriteTransforms[i].position.x < spriteTransforms[leftMostIndex].position.x)
            {
                leftMostIndex = i;
            }
        }

        spriteTransforms[leftMostIndex].position =
            new Vector3(spriteTransforms[leftMostIndex].position.x + (spriteTransforms.Length * spriteLength),
            spriteTransforms[leftMostIndex].position.y, spriteTransforms[leftMostIndex].position.z);
    }
}
