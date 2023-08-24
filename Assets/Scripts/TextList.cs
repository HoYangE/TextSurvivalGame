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
    private int maxTextCount;

    public void NewText(string text)
    {
        var textObject = Instantiate(textPrefab, listParent.transform);
        textObject.transform.SetAsFirstSibling();
        textObject.GetComponent<TMP_Text>().text = text;
        
        if (listParent.transform.childCount > maxTextCount)
            Destroy(listParent.transform.GetChild(listParent.transform.childCount-1).gameObject);
    }
}
