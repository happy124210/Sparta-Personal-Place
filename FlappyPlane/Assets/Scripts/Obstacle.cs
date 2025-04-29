using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float highPosY = 1f;
    public float lowPosY = -1f;
    public float holeSizeMin = 1f;
    public float holeSizeMax = 3f;
    public Transform topObject;
    public Transform bottomObject;

    private float widthPadding = 4f;

    private Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoleSize = holeSize / 2;

        topObject.localPosition = new Vector3(0, halfHoleSize);
        bottomObject.localPosition = new Vector3(0, -halfHoleSize);

        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0); // x���� �������� ���� �̵��ϴϱ� padding��ŭ
        placePosition.y = Random.Range(highPosY, lowPosY); // y���� �������� ����

        transform.position = placePosition;

        return placePosition;
    }
}
