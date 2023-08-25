using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveButton : MonoBehaviour
{
    public Vector2 TargetPos { get; set; }
    
    private PopUpButton _popUpButton;
    
    private void OnEnable()
    {
        transform.GetChild(0).GetComponent<Button>().onClick.AddListener(Move);
        GameObject.Find("Choice").TryGetComponent<PopUpButton>(out _popUpButton);
        _popUpButton.TouchBlock.SetActive(false);
    }

    private void Move()
    {
        var text = transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text;
        var destination = text.Split(':')[0];
        var time = int.Parse(text.Split(':')[1]);
        StartCoroutine(StartMoveCoroutine(destination, time));
    }

    IEnumerator StartMoveCoroutine(string destination, int time)
    {
        var elapsed = 0.0f;
        var timeLength = TimeManager.Instance.TimeScale * (time * 60);
        _popUpButton.TouchBlock.SetActive(true);
        
        while (elapsed <= timeLength)
        {
            yield return null;
            elapsed += Time.deltaTime;
            transform.GetChild(0).GetComponent<Image>().fillAmount = elapsed / timeLength;
            Debug.Log("11");
        }

        transform.GetChild(0).GetComponent<Image>().fillAmount = 1;
        StatusManager.Instance.SetPosition(destination);
        GameManager.Instance.PlayerPosition = TargetPos;
        Init();
    }

    private void Init()
    {
        _popUpButton.UpdatePopUp();
        _popUpButton.TouchBlock.SetActive(false);
    }
    
}
