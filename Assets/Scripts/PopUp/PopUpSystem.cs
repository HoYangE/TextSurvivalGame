using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Object = UnityEngine.Object;

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

public class MovePopUp : PopUp
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
            Object.Destroy(ContentOrigin.transform.GetChild(i).gameObject);
        }
    }
    
    private void SettingButton()
    {
        var csvData = CSVReader.Read("WorldData");

        foreach (var t in csvData)
        {
            var distance = (int)Vector2.Distance(new Vector2(int.Parse(t["X"].ToString()), int.Parse(t["Y"].ToString())), new Vector2(0, 0));
            
            if(distance is <= 0 or > 24)
                continue;
            
            var gameObject = Object.Instantiate(ButtonPrefabOrigin, ContentOrigin.transform);

            var text = t["Name"] + " : " + distance;
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = text;
        }
    }
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
            Object.Destroy(ContentOrigin.transform.GetChild(i).gameObject);
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
            Object.Destroy(ContentOrigin.transform.GetChild(i).gameObject);
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
            Object.Destroy(ContentOrigin.transform.GetChild(i).gameObject);
        }
    }
    
    private void SettingButton()
    {
    }
}