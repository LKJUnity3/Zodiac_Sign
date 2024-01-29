using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownEnemyController : TopDownCharacterController
{
    GameManager gameManager;

    protected Transform ClosetTarget { get; private set; }      //몬스터가 충돌해야하는 플레이어의 위치

    //protected override void Awake()
    //{
    //    base.Awake();
    //}
    /*
     * TopDownCharacterController 에서 Awake() 함수를 상속 받아 사용하지만 
     * 해당 클래스에서 Awake() 함수를 사용하지 않아 주석 처리
     * 해당 클래스에서 Awake() 함수에 CharacterStatsHandler.cs 를 만들어 해당 stats를 작성하고
     * stat을 받아오는 식으로 작성하면 좋을듯
     */

    protected virtual void Start()      //게임매니저, 플레이어 호출
    {
        gameManager = GameManager.instance;
        ClosetTarget = gameManager.Player;
    }

    protected virtual void FixedUpdate()
    {

    }

    protected float DistanceToTarget()      //플레이어와 몬스터의 거리 계산
    {
        return Vector3.Distance(transform.position, ClosetTarget.position);
    }

    protected Vector2 DirectionToTarget()       //플레이어와 몬스터의 사이의 단위 벡터 계산(방향)
    {
        return (ClosetTarget.position - transform.position).normalized;
    }
}
