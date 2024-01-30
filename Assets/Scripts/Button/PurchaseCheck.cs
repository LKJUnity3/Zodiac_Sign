using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseCheck : MonoBehaviour
{
    private string isLock = "true";

    private void Awake()
    {
        PlayerPrefs.SetString("isTrue", isLock);
        if (isLock == "true")
        {
            gameObject.transform.Find("Lock").gameObject.SetActive(true);
        }
    }
}
