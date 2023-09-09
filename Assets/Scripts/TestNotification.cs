using System;
using UnityEngine;
using Unity.Notifications.Android;

public class TestNotification : MonoBehaviour
{
    private void Start()
    {
        Show();
    }

    private void Show()
    {
#if UNITY_ANDROID
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Channel Name",
            Importance = Importance.High,
            Description = "Channel Description"
        };
    
        AndroidNotificationCenter.RegisterNotificationChannel(channel);
        
        var notification = new AndroidNotification()
        {
            Title = "Title",
            Text = "Text",
            SmallIcon = "icon_0",
            LargeIcon = "icon_1",
            FireTime = System.DateTime.Now.AddSeconds(5)
        };
        
        AndroidNotificationCenter.SendNotification(notification, "channel_id");
#endif
    }
}
