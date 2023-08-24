using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveButton : MonoBehaviour
{
    public Vector2 TargetPos { get; set; }
    private void OnEnable()
    {
        transform.GetChild(0).GetComponent<Button>().onClick.AddListener(Move);
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
        var timeLength = TimeData.Instance.TimeScale * (time * 60);

        while (elapsed <= timeLength)
        {
            yield return null;
            elapsed += Time.deltaTime;
            transform.GetChild(0).GetComponent<Image>().fillAmount = elapsed / timeLength;
        }

        transform.GetChild(0).GetComponent<Image>().fillAmount = 1;
        StatusData.Instance.SetPosition(destination);
        GameManager.Instance.PlayerPosition = TargetPos;
        Init();
    }

    private void Init()
    {
        GameObject.Find("Choice").TryGetComponent<PopUpButton>(out var popUpButton);
        popUpButton.UpdatePopUp();
    }
}
