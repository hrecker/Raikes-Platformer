using UnityEngine;

public class BackgroundScrollController : MonoBehaviour
{
    public BackgroundElement foreground;
    public BackgroundElement middleground;
    public BackgroundElement background;
    public float middlegroundMotionRatio; //How much does the middle ground move compared to the player
    public float backgroundMotionRatio;
    public Transform player;

    private float swapDistance; //How much horizontal distance is traveled before the background wraps
    private float playerPosAtLastForegroundWrap; //Player position the last time the foreground wrapped
    private float playerPosAtLastMiddlegroundWrap;
    private float playerPosAtLastBackgroundWrap;
    private Vector3 previousPlayerPosition;

    void Start()
    {
        previousPlayerPosition = player.transform.position;
        swapDistance = foreground.GetSpriteLength();
        playerPosAtLastBackgroundWrap = player.transform.position.x;
        playerPosAtLastMiddlegroundWrap = player.transform.position.x;
        playerPosAtLastForegroundWrap = player.transform.position.x;
    }

    void Update()
    {
        if(player != null)
        {
            float playerDistanceMoved = player.transform.position.x - previousPlayerPosition.x;
            //Debug.Log(playerDistanceMoved);
            float middlegroundDistance = (playerDistanceMoved * middlegroundMotionRatio);
            float backgroundDistance = (playerDistanceMoved * backgroundMotionRatio);
            //foreground.transform.position = new Vector3(foreground.transform.position.x + playerDistanceMoved, foreground.transform.position.y, foreground.transform.position.z);
            middleground.transform.position = new Vector3(middleground.transform.position.x + middlegroundDistance, middleground.transform.position.y, middleground.transform.position.z);
            background.transform.position = new Vector3(background.transform.position.x + backgroundDistance, background.transform.position.y, background.transform.position.z);

            checkForWrap(ref playerPosAtLastForegroundWrap, swapDistance, foreground);
            checkForWrap(ref playerPosAtLastMiddlegroundWrap, (1 / (1 - middlegroundMotionRatio)) * swapDistance, middleground);
            checkForWrap(ref playerPosAtLastBackgroundWrap, (1 / (1 - backgroundMotionRatio)) * swapDistance, background);

            previousPlayerPosition = player.transform.position;
        }
    }

    private void checkForWrap(ref float playerPosAtLastWrap, float elementSwapDistance, BackgroundElement element)
    {
        float positionDifference = player.transform.position.x - playerPosAtLastWrap;
        if(Mathf.Abs(positionDifference) > elementSwapDistance)
        {
            if(positionDifference > 0)
            {
                playerPosAtLastWrap += elementSwapDistance;
                element.WrapToRight();
            }
            else
            {
                playerPosAtLastWrap -= elementSwapDistance;
                element.WrapToLeft();
            }
        }
        /*if (Mathf.Abs(distanceMoved) > swapDistance)
        {
            if (distanceMoved > 0)
            {
                distanceMoved -= swapDistance;
                element.WrapToRight();
            }
            else
            {
                distanceMoved += swapDistance;
                element.WrapToLeft();
            }
        }*/
    }
}
