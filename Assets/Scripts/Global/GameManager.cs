using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform Player { get; private set; }
    [SerializeField] private string playerTag = "Player";

    private void Awake()
    {
        instance = this;
        Player = GameObject.FindGameObjectWithTag(playerTag).transform;     //tag가 Player인 sprite를 찾아 위치값을 Player 변수에 입력
        //hieracy 상의 모든 object를 검사하기 때문에 update 보단 awake 에서 한 번만 찾아주는게 좋음

    }
}
