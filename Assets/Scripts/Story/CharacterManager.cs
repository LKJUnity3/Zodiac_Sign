using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

// 캐릭터 정보 클래스
[System.Serializable]
public class CharacterInfo
{
    public int Choose;                                   //왼쪽 오른쪽 구분 변수
    public string characterName;            // 캐릭터 이름
    public Sprite characterSprite;  // 캐릭터 그림
    public string messages;               // 대화 메시지들

    // 생성자
    public CharacterInfo(int choose ,string name, Sprite sprite, string msgs)
    {
        Choose = choose;
        characterName = name;
        characterSprite = sprite;
        messages = msgs;
    }
}

public class CharacterManager : MonoBehaviour
{
    public SpriteRenderer characterSpriteLeft;    // 캐릭터 그림을 표시할 이미지 UI 요소(왼쪽)
    public SpriteRenderer characterSpriteRight;  // 캐릭터 그림을 표시할 이미지 UI 요소(오른쪽)
    public Text messageText;                  // 대화 메시지를 표시할 텍스트 UI 요소
    public Text nameText;                     // 캐릭터 이름을 표시할 텍스트 UI 요소
    public Button nextButton;                 // 다음 대화로 넘어가는 버튼


    public List<CharacterInfo> characters = new List<CharacterInfo>(); // 캐릭터 정보를 담을 리스트
   
    private int currentCharacterIndex = 0; // 현재 표시 중인 캐릭터의 인덱스

    void Start()
    {
        if (characters.Count > 0)
        {
            DisplayCharacterInfo(characters[currentCharacterIndex]);
        }
        else
        {
            Debug.LogError("캐릭터 정보가 없습니다.");
        }

        // 버튼 클릭 이벤트에 함수 연결
        nextButton.onClick.AddListener(NextMessage);
    }

    public void SetSprite(int choose, Sprite sprite)
    {
        if (choose == 0) // 왼쪽 캐릭터 설정
        {
            characterSpriteLeft.sprite = sprite;
            characterSpriteLeft.enabled = true;
            characterSpriteRight.enabled = false;
        }
        else if (choose == 1) // 오른쪽 캐릭터 설정
        {
            characterSpriteRight.sprite = sprite;
            characterSpriteLeft.enabled = false;
            characterSpriteRight.enabled = true;
        }

        else
        {
            Debug.LogError("잘못된 선택입니다.");
        }


    }

    void DisplayCharacterInfo(CharacterInfo characterInfo)
    {

        SetSprite(characterInfo.Choose, characterInfo.characterSprite);
        messageText.text = characterInfo.messages;                  // 대화 메시지 변경
        nameText.text = characterInfo.characterName;                   // 캐릭터 이름 변경
    }

    public void NextMessage()
    {
        currentCharacterIndex++;

        if (currentCharacterIndex >= characters.Count)
        {
           SceneManager.LoadScene("EnemyScene");
            return;
        }

        DisplayCharacterInfo(characters[currentCharacterIndex]);
    }

}
