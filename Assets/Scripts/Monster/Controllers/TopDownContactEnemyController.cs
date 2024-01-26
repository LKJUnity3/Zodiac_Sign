using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TopDownContactEnemyController : TopDownEnemyController
{
    [SerializeField][Range(0f, 100f)] private float followRange;        //반경 100픽셀 범위의 변수 생성(몬스터 인식 범위로 사용)
    [SerializeField] private string targetTag = "Player";       //Player를 따라가야하므로 tag로 찾기 위해 tag값 저장
    private bool _isCollidingWithTarget;     //Player와 충돌 여부 판단 bool 값

    [SerializeField] private SpriteRenderer characterRenderer;      

    //protected virtual void Awake()
    //{

    //}
    //TopDownEnemyController 에서 Awake 사용하지 않아 주석처리

    protected override void Start()     //TopDownEnemyController의 start()를 재정의
    {
        base.Start();
    }

    protected override void FixedUpdate()       //TopDownEnemyController의 FixedUpdate() 를 재정의
    {
        base.FixedUpdate();

        Vector2 direction = Vector2.zero;

        if (DistanceToTarget() < followRange)       //몬스터 인식 범위 내로 들어갈 경우, 플레이어 방향으로 방향 변경
        {
            direction = DirectionToTarget();
        }

        CallMoveEvent(direction);       
        Rotate(direction);      
    }


    private void Rotate(Vector2 direction)      //몬스터가 바라보는 방향에 따라 sprite회전하도록 하는 함수
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        characterRenderer.flipX = Mathf.Abs(rotZ) > 90f;

    }

}
