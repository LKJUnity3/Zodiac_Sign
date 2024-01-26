using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class TopDownRangeEnemyController : TopDownEnemyController
{
    [SerializeField] private float followRange = 15f;
    [SerializeField] private float shootRange = 10f;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        //attackSO 생성한 이후 작성해야함
        //IsAttacking = false;
        //if (distance <= followRange)
        //{
        //    if (distance <= shootRange)
        //    {
        //        int layerMaskTarget = Stats.CurrentStates.attackSO.target;
        //        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 11f, (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);
        //        //안 보이는 물리 레이저 발사해 몬스터와 플레이어 사이에 막힌 지형이 있는지 검사
        //        if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
        //        {
        //            CallLookEvent(direction);
        //            CallMoveEvent(Vector2.zero);
        //            IsAttacking = true;
        //        }
        //        else
        //        {
        //            CallMoveEvent(direction);
        //        }
        //    }
        //    else
        //    {
        //        CallMoveEvent(direction);
        //    }
        //}
        //else
        //{
        //    CallMoveEvent(direction);
        //}
    }
}


