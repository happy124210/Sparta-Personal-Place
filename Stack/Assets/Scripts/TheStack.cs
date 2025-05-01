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

    private Color prevColor;
    private Color nextColor;

    private bool isMovingX;

    private void Start()
    {
        if (originBlock == null)
        {
            Debug.LogError("originBlock is NULL");
            return;
        }

        prevColor = GetRandomColor();
        nextColor = GetRandomColor();

        prevBlockPosition = Vector3.down; // 첫 블록 배치 전에 기준 위치를 아래로 설정
        SpawnBlock();
        SpawnBlock();
    }


    private void Update()
    {
        MoveBlock();
        if (PlaceBlock())
        {
            if (Input.GetMouseButtonDown(0))
            {
                SpawnBlock();
            }
        }
        else
        {
            Debug.Log("GameOver");
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
        ColorChange(newBlock);

        if (newBlock == null)
        {
            Debug.LogError("newBlock instantiate failed");
            return false;
        }

        newTransform = newBlock.transform;
        newTransform.parent = this.transform;
        newTransform.localPosition = prevBlockPosition + Vector3.up;
        newTransform.localRotation = Quaternion.identity;
        newTransform.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

        stackCount++;
        desiredPosition = Vector3.down * stackCount;
        blockTransition = 0f;

        lastBlock = newTransform;

        return true;
    }

    private Color GetRandomColor()
    {
        float r = Random.Range(100f, 250f) / 255f;
        float g = Random.Range(100f, 250f) / 255f;
        float b = Random.Range(100f, 250f) / 255f;

        return new Color(r, g, b);
    }

    private void ColorChange(GameObject go)
    {
        Color applyColor = Color.Lerp(prevColor, nextColor, (stackCount % 11) / 10f);
        Renderer rn = go.GetComponent<Renderer>();
        if (rn == null)
        {
            Debug.LogError("renderer is NULL");
        }

        rn.material.color = applyColor;
        Camera.main.backgroundColor = applyColor - new Color(0.1f, 0.1f, 0.1f);
        if (applyColor.Equals(nextColor))
        {
            prevColor = nextColor;
            nextColor = GetRandomColor();
        }
    }

    private void MoveBlock()
    {
        blockTransition += Time.deltaTime * BlockMovingSpeed;
        float movePosition = Mathf.PingPong(blockTransition, BoundSize) - BoundSize / 2;
        
        if (isMovingX)
        {
            lastBlock.localPosition = new Vector3(movePosition * MovingBoundSize, stackCount, secondaryPosition);
        }
        else
        {
            lastBlock.localPosition = new Vector3(secondaryPosition, stackCount, movePosition * MovingBoundSize);
        }
    }

    private bool PlaceBlock()
    {
        Vector3 lastPosition = lastBlock.localPosition;

        if (isMovingX)
        {
            float deltaX = prevBlockPosition.x - lastPosition.x;

            deltaX = Mathf.Abs(deltaX);
            if (deltaX > ErrorMargin)
            {
                stackBounds.x -= deltaX;
                if (stackBounds.x <= 0)
                {
                    return false;
                }

                float middle = (prevBlockPosition.x + lastPosition.x) / 2;
                lastBlock.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

                Vector3 tempPosition = lastBlock.localPosition;
                tempPosition.x = middle; // 중심 옮겨준 후
                lastBlock.localPosition = lastPosition = tempPosition;

            }
            else
            {
                lastBlock.localPosition = prevBlockPosition + Vector3.up;
            }

        }
        else
        {
            float deltaZ = prevBlockPosition.z - lastPosition.z;

            deltaZ = Mathf.Abs(deltaZ);
            if (deltaZ > ErrorMargin)
            {
                stackBounds.x -= deltaZ;
                if (stackBounds.x <= 0)
                {
                    return false;
                }

                float middle = (prevBlockPosition.z + lastPosition.z) / 2;
                lastBlock.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

                Vector3 tempPosition = lastBlock.localPosition;
                tempPosition.z = middle; // 중심 옮겨준 후
                lastBlock.localPosition = lastPosition = tempPosition;
            }
            else
            {
                lastBlock.localPosition = prevBlockPosition + Vector3.up;
            }
        }
        secondaryPosition = (isMovingX) ? lastBlock.localPosition.x : lastBlock.localPosition.z;
        
        return true;
    }
}
