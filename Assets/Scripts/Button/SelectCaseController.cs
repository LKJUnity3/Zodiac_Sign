using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;


public class SelectCaseController : MonoBehaviour
{
    [SerializeField] GameObject Close;
    [SerializeField] GameObject Open;
    private float Timer = 0.0f;
    private string isLock;
    public void Awake()
    {
        isLock = PlayerPrefs.GetString("isTrue","true");
    }

    public void OnClick()
    {
        Close.SetActive(false);
        Open.SetActive(true);

    }

    public void PurchaseOnClick()
    {
        if (isLock == "true")
        {
            Timer = 2.0f;
            gameObject.transform.Find("NotBuying").gameObject.SetActive(true);
        }
    }

    public void Update()
    {
        if (Timer > 0.0f)
        {
            Timer -= Time.deltaTime;
        }  
        if (Timer < 0.0f)
        {
            gameObject.transform.Find("NotBuying").gameObject.SetActive(false);
        }
    }
}
