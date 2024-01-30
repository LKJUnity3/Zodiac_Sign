using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCaseController : MonoBehaviour
{
    [SerializeField] private GameObject MainSelect;
    [SerializeField] private GameObject ChraSelect;
    [SerializeField] private GameObject StageSelect;
    [SerializeField] private GameObject ShopSelect;
    [SerializeField] private GameObject BackButton;

    public void OnCharSelect()
    {
          ChraSelect.SetActive(true);
          MainSelect.SetActive(false);
    }

    public void OnBackSelect()
    {
        ChraSelect.SetActive(false);
        StageSelect.SetActive(false);
        MainSelect.SetActive(true);
    }
}
