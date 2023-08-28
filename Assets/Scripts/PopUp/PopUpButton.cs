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
    [SerializeField]
    private GameObject touchBlock;
    
    private string currentPopUpName;
    
    public GameObject TouchBlock => touchBlock;
    
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
        touchBlock.SetActive(false);
        
        var popUp = PopUp.Create(name);
        popUp.Init(content, buttonPrefab);

    }

    public void UpdatePopUp()
    {
        var popUp = PopUp.Create(currentPopUpName);
        popUp.Init(content, buttonPrefab);
    }
}
