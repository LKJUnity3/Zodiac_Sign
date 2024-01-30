using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

// 캐릭터 정보 클래스
[System.Serializable]
public class CharacterInfo
{
    public string characterName;    // 캐릭터 이름
    public SpriteRenderer characterSprite;  // 캐릭터 그림
    public string[] messages;       // 대화 메시지들

    // 생성자
    public CharacterInfo(string name, SpriteRenderer sprite, string[] msgs)
    {
        characterName = name;
        characterSprite = sprite;
        messages = msgs;
    }
}

public class CharacterManager : MonoBehaviour
{
    public SpriteRenderer characterSprite;    // 캐릭터 그림을 표시할 이미지 UI 요소
    public Text messageText;        // 대화 메시지를 표시할 텍스트 UI 요소
    public Text name;

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
    }

    void DisplayCharacterInfo(CharacterInfo characterInfo)
    {
        characterSprite = characterInfo.characterSprite;

        if (characterInfo.messages.Length > 0)
        {
            messageText.text = characterInfo.messages[0];
            name.text = characterInfo.characterName[0].ToString();
        }
        else
        {
            messageText.text = "대화 메시지가 없습니다.";
        }
    }

    public void NextMessage()
    {
        currentCharacterIndex++;

        if (currentCharacterIndex >= characters.Count)
        {
            currentCharacterIndex = 0;
        }

        DisplayCharacterInfo(characters[currentCharacterIndex]);
    }

    void InitializeCharacters()
    {
        // 대화 정보를 리스트에 추가
        characters.Add(new CharacterInfo("오빠", null, new string[] {
            "동생아, 이 꽃들 보니까 너무 예쁘지?"
        }));

        characters.Add(new CharacterInfo("동생", null, new string[] {
            "응, 정말 예쁘다. 오빠랑 같이 있는 것도 너무 행복해."
        }));

        characters.Add(new CharacterInfo("할아버지", null, new string[] {
            "자, 이제 집으로 돌아가자꾸나. 저녁때가 다 되었구나."
        }));
    }

    void Awake()
    {
        InitializeCharacters(); // 캐릭터 초기화
    }
}
