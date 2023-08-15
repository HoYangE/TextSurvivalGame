using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextList : MonoBehaviour
{   
    [SerializeField]
    private GameObject listParent;
    [SerializeField]
    private GameObject textPrefab;

    [SerializeField]
    private Vector3 startPosition;
    [SerializeField]
    private int maxTextCount;

    public void NewText(string text)
    {
        var textObject = Instantiate(textPrefab, listParent.transform);
        textObject.transform.SetAsFirstSibling();
        textObject.GetComponent<TMP_Text>().text = text;
        PushText();
    }

    private void PushText()
    {
        for (var i = 0; i < listParent.transform.childCount; i++)
        {
            if (i >= maxTextCount)
            {
                Destroy(listParent.transform.GetChild(i).gameObject);
                break;
            }

            var textObject = listParent.transform.GetChild(i);
            textObject.transform.localPosition = new Vector3(0, startPosition.y - 70 * i, 0);
        }
    }
}
