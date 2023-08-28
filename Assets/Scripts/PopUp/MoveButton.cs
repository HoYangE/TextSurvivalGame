using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveButton : MonoBehaviour
{
    public MovePopUpData MovePopUpData { get; set; } = new MovePopUpData();
    
    private PopUpButton _popUpButton;
    
    private void Start()
    {
        GameObject.Find("Choice").TryGetComponent(out _popUpButton);
        
        if(string.IsNullOrEmpty(DataManager.Instance.MovePopUpData.name))
            DoMoving();
        else
            CantMoving();
    }

    private void Move()
    {
        StartMove();
    }

    private void DoMoving()
    {
        transform.GetChild(0).GetComponent<Button>().onClick.AddListener(Move);
        _popUpButton.TouchBlock.SetActive(false);
    }
    
    private void CantMoving()
    {
        _popUpButton.TouchBlock.SetActive(true);
        if (DataManager.Instance.MovePopUpData.name == MovePopUpData.name &&
            DataManager.Instance.MovePopUpData.position == MovePopUpData.position)
            StartCoroutine(StartMoveCoroutine());
    }

    private void StartMove()
    {
        DataManager.Instance.MovePopUpData = MovePopUpData;
        DataManager.Instance.MovePopUpData.time = Time.realtimeSinceStartup;

        StartCoroutine(StartMoveCoroutine());
        GameManager.Instance.StartMoving();
    }
    
    IEnumerator StartMoveCoroutine()
    {
        _popUpButton.TouchBlock.SetActive(true);
        
        while (GameManager.Instance.MoveNormTime <= 1)
        {
            yield return null;
            transform.GetChild(0).GetComponent<Image>().fillAmount = GameManager.Instance.MoveNormTime;
        }
    }
}
