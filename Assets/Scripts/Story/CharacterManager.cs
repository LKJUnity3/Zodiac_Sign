using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

// ĳ���� ���� Ŭ����
[System.Serializable]
public class CharacterInfo
{
    public int Choose;                                   //���� ������ ���� ����
    public string characterName;            // ĳ���� �̸�
    public Sprite characterSprite;  // ĳ���� �׸�
    public string messages;               // ��ȭ �޽�����

    // ������
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
    public SpriteRenderer characterSpriteLeft;    // ĳ���� �׸��� ǥ���� �̹��� UI ���(����)
    public SpriteRenderer characterSpriteRight;  // ĳ���� �׸��� ǥ���� �̹��� UI ���(������)
    public Text messageText;                  // ��ȭ �޽����� ǥ���� �ؽ�Ʈ UI ���
    public Text nameText;                     // ĳ���� �̸��� ǥ���� �ؽ�Ʈ UI ���
    public Button nextButton;                 // ���� ��ȭ�� �Ѿ�� ��ư


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

        // ��ư Ŭ�� �̺�Ʈ�� �Լ� ����
        nextButton.onClick.AddListener(NextMessage);
    }

    public void SetSprite(int choose, Sprite sprite)
    {
        if (choose == 0) // ���� ĳ���� ����
        {
            characterSpriteLeft.sprite = sprite;
            characterSpriteLeft.enabled = true;
            characterSpriteRight.enabled = false;
        }
        else if (choose == 1) // ������ ĳ���� ����
        {
            characterSpriteRight.sprite = sprite;
            characterSpriteLeft.enabled = false;
            characterSpriteRight.enabled = true;
        }

        else
        {
            Debug.LogError("�߸��� �����Դϴ�.");
        }


    }

    void DisplayCharacterInfo(CharacterInfo characterInfo)
    {

        SetSprite(characterInfo.Choose, characterInfo.characterSprite);
        messageText.text = characterInfo.messages;                  // ��ȭ �޽��� ����
        nameText.text = characterInfo.characterName;                   // ĳ���� �̸� ����
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
