using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Obstacle : MonoBehaviour
{
    private GameManager gameManager;

    public float highPosY = 1f;
    public float lowPosY = -1f;
    public float holeSizeMin = 1f;
    public float holeSizeMax = 3f;
    public Transform topObject;
    public Transform bottomObject;

    private float widthPadding = 4f;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoleSize = holeSize / 2;

        topObject.localPosition = new Vector3(0, halfHoleSize);
        bottomObject.localPosition = new Vector3(0, -halfHoleSize);

        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0); // x값은 고정으로 같이 이동하니까 padding만큼
        placePosition.y = Random.Range(lowPosY, highPosY); // y값은 랜덤으로 지정

        transform.position = placePosition;

        return placePosition;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            gameManager.AddScore(1);
        }
    }
}
