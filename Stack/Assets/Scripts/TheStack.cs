using JetBrains.Annotations;
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

    public GameObject originBlock = null;

    public int Score { get => stackCount; }
    
    public int MaxCombo { get => maxCombo; }
    private int maxCombo = 0;

    public int BestScore {  get => bestScore; }
    private int bestScore;

    public int BestCombo { get => bestCombo; }
    private int bestCombo;

    private const string BestScoreKey = "bestScore";
    private const string BestComboKey = "bestComobo";

    private bool isGameOver = false;

    private void Start()
    {
        if (originBlock == null)
        {
            Debug.LogError("originBlock is NULL");
            return;
        }

        PlayerPrefs.GetInt(BestScoreKey, 0);
        PlayerPrefs.GetInt(BestComboKey, 0);

        prevColor = GetRandomColor();
        nextColor = GetRandomColor();

        prevBlockPosition = Vector3.down; // 첫 블록 배치 전에 기준 위치를 아래로 설정
        SpawnBlock();
        SpawnBlock();
    }

    private void Update()
    {
        if (isGameOver) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (PlaceBlock())
            {
                SpawnBlock();
            }
            else
            {
                // 게임 오버
                Debug.Log("GameOver");
                UpdateScore();
                isGameOver = true;
                GameOverEffect();
            }
        }

        MoveBlock();
        transform.position = Vector3.Lerp(transform.position, desiredPosition, StackMovingSpeed * Time.deltaTime);
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
        isMovingX = !isMovingX;

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
            bool isNegativeNum = (deltaX < 0) ? true : false;

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

                float rubbleHalfScale = deltaX / 2f;
                CreateRubble(
                    new Vector3(isNegativeNum
                            ? lastPosition.x + stackBounds.x / 2 + rubbleHalfScale
                            : lastPosition.x - stackBounds.x / 2 - rubbleHalfScale
                        , lastPosition.y
                        , lastPosition.z),
                    new Vector3(deltaX, 1, stackBounds.y));

                comboCount = 0;
            }
            else
            {
                ComboCheck();
                lastBlock.localPosition = prevBlockPosition + Vector3.up;
            }

        }
        else
        {
            float deltaZ = prevBlockPosition.z - lastPosition.z;
            bool isNegativeNum = (deltaZ < 0) ? true : false;

            deltaZ = Mathf.Abs(deltaZ);
            if (deltaZ > ErrorMargin)
            {
                stackBounds.y -= deltaZ;
                if (stackBounds.y <= 0)
                {
                    return false;
                }

                float middle = (prevBlockPosition.z + lastPosition.z) / 2;
                lastBlock.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

                Vector3 tempPosition = lastBlock.localPosition;
                tempPosition.z = middle; // 중심 옮겨준 후
                lastBlock.localPosition = lastPosition = tempPosition;

                float rubbleHalfScale = deltaZ / 2f;
                CreateRubble(
                    new Vector3(
                        lastPosition.x
                        , lastPosition.y
                        , isNegativeNum
                            ? lastPosition.z + stackBounds.y / 2 + rubbleHalfScale
                            : lastPosition.z - stackBounds.y / 2 - rubbleHalfScale),
                    new Vector3(stackBounds.x, 1, deltaZ));

                comboCount = 0;
            }
            else
            {
                ComboCheck();
                lastBlock.localPosition = prevBlockPosition + Vector3.up;
            }
        }
        secondaryPosition = (isMovingX) ? lastBlock.localPosition.x : lastBlock.localPosition.z;
        
        return true;
    }

    private void CreateRubble(Vector3 pos, Vector3 scale)
    {
        GameObject go = Instantiate(lastBlock.gameObject);
        go.transform.parent = this.transform;

        go.transform.localPosition = pos;
        go.transform.localScale = scale;
        go.transform.localRotation = Quaternion.identity;

        go.AddComponent<Rigidbody>();
        go.name = "Rubble";
    }

    private void ComboCheck()
    {
        if (comboCount > maxCombo)
        {
            maxCombo = comboCount;

            if (comboCount % 5 == 0)
            {
                Debug.Log("5 combo!");
                stackBounds += new Vector3(0.5f, 0.5f);

                stackBounds.x = (stackBounds.x > BoundSize) ? BoundSize : stackBounds.x;
                stackBounds.y = (stackBounds.y > BoundSize) ? BoundSize : stackBounds.y;
            }
        }
    }

    private void UpdateScore()
    {
        if (bestScore < stackCount)
        {
            bestScore = stackCount;
            bestCombo = maxCombo;
        }

        PlayerPrefs.SetInt(BestScoreKey, bestScore);
        PlayerPrefs.SetInt(BestComboKey, bestCombo);
    }

    private void GameOverEffect()
    {
        int childCount = this.transform.childCount;

        for (int i = 1; i < 20 ; i++)
        {
            if (childCount < i) break;

            GameObject go = this.transform.GetChild(childCount - i).gameObject;

            if (go.name.Equals("Rubble")) continue;
            Rigidbody rigid = go.AddComponent<Rigidbody>();
            rigid.AddForce((Vector3.up * Random.Range(0f, 10f) + Vector3.right * (Random.Range(0f, 10f) - 5f)) * 100f);
        }
    }
}
