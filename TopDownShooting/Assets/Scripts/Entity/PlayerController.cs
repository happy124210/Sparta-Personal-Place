using UnityEngine;

public class PlayerController : BaseController
{
    private Camera camera;

    protected override void Start()
    {
        base.Start();
        camera = Camera.main;
    }

    protected override void HandleAction()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        movementDirection = new Vector2(horizontal, vertical).normalized; // 움직임에 쓸 순수 방향
        
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPos =  camera.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPos - (Vector2)transform.position); // 보는 방향 = 마우스 방향

        lookDirection = (lookDirection.magnitude < .9f) 
            ? Vector2.zero 
            : lookDirection.normalized;// 마우스 위치가 캐릭터랑 너무 겹쳐있는 경우

        lookDirection = (lookDirection.magnitude < .9f) ?  Vector2.zero : lookDirection.normalized;

        isAttacking = Input.GetMouseButton(0);
    }
}
