using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheStack : MonoBehaviour
{
    private const float BoundSize = 3.5f; // 블럭 사이즈
    private const float MovingBoundSize = 3f; // 블럭 이동량
    private const float StackMovingSpeed = 5f; // 스택 자체의 이동 속도 (카메라)
    private const float BlockMovingSpeed = 3.5f; // 블럭 이동 속도
    private const float ErrorMargin = 0.1f; // 성공 판정 허용 오차

    public GameObject originBlock = null;

    private Vector3 prevBlockPosition;
    private Vector3 desiredPosition;
    private Vector3 stackBounds = new Vector2(BoundSize, BoundSize);

    private Transform lastBlock = null;
    private float blockTransition = 0f;
    private float secondaryPosition = 0f;

    private int stackCount = -1;
    private int comboCount = 0;

    private void Start()
    {
        if (originBlock == null)
        {
            Debug.LogError("originBlock is NULL");
            return;
        }

        prevBlockPosition = Vector3.down; // 첫 블록 배치 전에 기준 위치를 아래로 설정
        SpawnBlock();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnBlock();
        }

        transform.position = Vector3.Lerp(transform.position, desiredPosition, (StackMovingSpeed * Time.deltaTime));
    }


    private bool SpawnBlock()
    {
        if (lastBlock != null)
        {
            prevBlockPosition = lastBlock.localPosition;
        }

        GameObject newBlock = null;
        Transform newTransform = null;

        newBlock = Instantiate(originBlock);

        if (newBlock == null)
        {
            Debug.LogError("newBlock instantiate failed");
            return false;
        }

        newTransform = newBlock.transform;
        newTransform.parent = this.transform;
        newTransform.localPosition = prevBlockPosition + Vector3.up;
        newTransform.rotation = Quaternion.identity;
        newTransform.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

        stackCount++;
        desiredPosition = Vector3.down * stackCount;
        blockTransition = 0f;

        lastBlock = newTransform;

        return true;
    }
}
