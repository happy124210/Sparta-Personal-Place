using System;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivot;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection {get {return movementDirection;}}
    
    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection {get {return lookDirection;}}
    
    private Vector2 knockback =  Vector2.zero;
    private float knockbackDuration = 0.0f;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);
        
        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.deltaTime;
        }
    }

    protected virtual void FixedUpdate()
    {
        Movement(movementDirection);
    }

    protected virtual void HandleAction()
    {
        
    }

    private void Movement(Vector2 direction)
    {
        direction *= 5; // stat으로 대체
        if (knockbackDuration > 0.0f) // 넉백을 적용해야한다면
        {
            direction *= 0.2f; // 이동속도 줄여주고
            direction += knockback; // 넉백 힘을 방향에 추가
        }
        
        _rigidbody.velocity = direction; //속도에 적용
    }

    private void Rotate(Vector2 direction)
    {
        float rot2 = Mathf.Atan2(direction.y, direction.x) *  Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rot2) > 90f; // 90도보다 크면 왼쪽
        
        characterRenderer.flipX = isLeft;

        if (weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0f, 0f, rot2);
        }
    }

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
    }

}
