using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerNameSetting : MonoBehaviour
{
    [SerializeField] private InputField PlayerName;
    private string PlayerNameText = null;
    [SerializeField] GameObject Panel;
    [SerializeField] Text PanelText;

    private void Awake()
    {

    }

    public void OnPlayerNameSetting()
    {
        PlayerNameText = PlayerName.text.Trim();
        if (PlayerNameText.Length > 2 && PlayerNameText.Length <=10)
        {
            PlayerPrefs.SetString("PlayerName", PlayerNameText);
            SceneManager.LoadScene("SelectScene");
        }
        else
        {
            Panel.SetActive(true);
            Panel.GetComponentInChildren<Text>().text = "이름을 올바르게 입력해주세요. 2~10자 공백, 띄어쓰기는 안됩니다.";
        }
    }


}
