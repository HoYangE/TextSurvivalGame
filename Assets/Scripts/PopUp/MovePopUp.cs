using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
            UnityEngine.Object.Destroy(ContentOrigin.transform.GetChild(i).gameObject);
        }
    }
    
    private void SettingButton()
    {
        var csvData = CSVReader.Read("WorldData");

        foreach (var t in csvData)
        {
            var currentPos = GameManager.Instance.PlayerPosition;
            var targetPos = new Vector2(int.Parse(t["X"].ToString()), int.Parse(t["Y"].ToString()));
            var distance = (int)Vector2.Distance(targetPos, currentPos);
            
            if(distance is <= 0 or > 24)
                continue;
            
            var gameObject = Object.Instantiate(ButtonPrefabOrigin, ContentOrigin.transform);

            var text = t["Name"] + " : " + distance;
            gameObject.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>().text = text;
            gameObject.AddComponent<MoveButton>();
            gameObject.GetComponent<MoveButton>().TargetPos = targetPos;
        }
    }
}
