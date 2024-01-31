using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    [SerializeField] private int Stage;

    public void Onclick()
    {
        PlayerPrefs.SetInt("Stage", Stage);
        if (Stage == 1)
        {
            SceneManager.LoadScene($"Ep_{Stage}");
        }
        else
        {
            SceneManager.LoadScene($"EnemyScene");
        }
        
    }
}
