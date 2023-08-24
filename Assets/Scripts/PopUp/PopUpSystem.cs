using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PopUp
{
    public static PopUp Create(string name)
    {
        PopUp popUp = name switch
        {
            "Move" => new MovePopUp(),
            "Action" => new ActionPopUp(),
            "Inventory" => new InventoryPopUp(),
            "Setting" => new SettingPopUp(),
            _ => throw new ArgumentOutOfRangeException(nameof(name), name, null)
        };
        return popUp;
    }
    
    protected GameObject ContentOrigin;
    protected GameObject ButtonPrefabOrigin;
    
    public abstract void Init(GameObject content, GameObject buttonPrefab);
}


public class ActionPopUp : PopUp
{
    public override void Init(GameObject content, GameObject buttonPrefab)
    {
        ContentOrigin = content;
        ButtonPrefabOrigin = buttonPrefab;

        Delete();
        SettingButton();
    }
    
    private void Delete()
    {
        for (var i = 0; i < ContentOrigin.transform.childCount; i++)
        {
            UnityEngine.Object.Destroy(ContentOrigin.transform.GetChild(i).gameObject);
        }
    }
    
    private void SettingButton()
    {
    }
}

public class InventoryPopUp : PopUp
{
    public override void Init(GameObject content, GameObject buttonPrefab)
    {
        ContentOrigin = content;
        ButtonPrefabOrigin = buttonPrefab;

        Delete();
        SettingButton();
    }
    
    private void Delete()
    {
        for (var i = 0; i < ContentOrigin.transform.childCount; i++)
        {
            UnityEngine.Object.Destroy(ContentOrigin.transform.GetChild(i).gameObject);
        }
    }
    
    private void SettingButton()
    {
    }
}

public class SettingPopUp : PopUp
{
    public override void Init(GameObject content, GameObject buttonPrefab)
    {
        ContentOrigin = content;
        ButtonPrefabOrigin = buttonPrefab;

        Delete();
        SettingButton();
    }
    
    private void Delete()
    {
        for (var i = 0; i < ContentOrigin.transform.childCount; i++)
        {
            UnityEngine.Object.Destroy(ContentOrigin.transform.GetChild(i).gameObject);
        }
    }
    
    private void SettingButton()
    {
    }
}