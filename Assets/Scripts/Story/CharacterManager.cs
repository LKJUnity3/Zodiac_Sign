using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

// ĳ���� ���� Ŭ����
[System.Serializable]
public class CharacterInfo
{
    public string characterName;    // ĳ���� �̸�
    public SpriteRenderer characterSprite;  // ĳ���� �׸�
    public string[] messages;       // ��ȭ �޽�����

    // ������
    public CharacterInfo(string name, SpriteRenderer sprite, string[] msgs)
    {
        characterName = name;
        characterSprite = sprite;
        messages = msgs;
    }
}

public class CharacterManager : MonoBehaviour
{
    public SpriteRenderer characterSprite;    // ĳ���� �׸��� ǥ���� �̹��� UI ���
    public Text messageText;        // ��ȭ �޽����� ǥ���� �ؽ�Ʈ UI ���
    public Text name;

    public List<CharacterInfo> characters = new List<CharacterInfo>(); // ĳ���� ������ ���� ����Ʈ

    private int currentCharacterIndex = 0; // ���� ǥ�� ���� ĳ������ �ε���

    void Start()
    {
        if (characters.Count > 0)
        {
            DisplayCharacterInfo(characters[currentCharacterIndex]);
        }
        else
        {
            Debug.LogError("ĳ���� ������ �����ϴ�.");
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
            messageText.text = "��ȭ �޽����� �����ϴ�.";
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
        // ��ȭ ������ ����Ʈ�� �߰�
        characters.Add(new CharacterInfo("����", null, new string[] {
            "������, �� �ɵ� ���ϱ� �ʹ� ������?"
        }));

        characters.Add(new CharacterInfo("����", null, new string[] {
            "��, ���� ���ڴ�. ������ ���� �ִ� �͵� �ʹ� �ູ��."
        }));

        characters.Add(new CharacterInfo("�Ҿƹ���", null, new string[] {
            "��, ���� ������ ���ư��ڲٳ�. ���ᶧ�� �� �Ǿ�����."
        }));
    }

    void Awake()
    {
        InitializeCharacters(); // ĳ���� �ʱ�ȭ
    }
}
