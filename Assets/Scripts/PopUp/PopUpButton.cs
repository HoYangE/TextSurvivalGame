using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpButton : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPopUp;
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private GameObject buttonPrefab;
    
    private string currentPopUpName;
    
    public void ButtonPopUp(string name)
    {
        if (currentPopUpName == name)
        {
            buttonPopUp.SetActive(false);
            currentPopUpName = null;
            return;
        }

        buttonPopUp.SetActive(true);
        currentPopUpName = name;

        var popUp = PopUp.Create(name);
        popUp.Init(content, buttonPrefab);

    }
    
}
