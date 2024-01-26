using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatsChageType
{
    Add,
    Multiple,
    Ovverride,
}

[Serializable]

public class CharacterStats 
{
    public StatsChageType statsChageType;
    [Range(0, 100)] public int maxHealth;
    [Range(1f, 20f)] public float MoveSpeed;
    [Range(1, 10)] public int Might; // 기본공격력

    //공격 데이터
    public AttackSO attackSO;

}   
