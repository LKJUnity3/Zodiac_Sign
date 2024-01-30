using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCaseController : MonoBehaviour
{
    [SerializeField] GameObject Close;
    [SerializeField] GameObject Open;

    public void OnClick()
    {
        Close.SetActive(false);
        Open.SetActive(true);
    }
}
