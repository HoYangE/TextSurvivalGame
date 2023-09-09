using UnityEngine;
using Firebase;
using Firebase.Messaging;

#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class FirebaseManager : MonoBehaviour
{
    private FirebaseApp _app;
    
    private void Start()
    {
        
#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }
#endif
        
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                _app = FirebaseApp.DefaultInstance;
                
                FirebaseMessaging.TokenReceived += OnTokenReceived;
                
                FirebaseMessaging.MessageReceived += OnMessageReceived;
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + task.Result);
            }
        });
    }
    
    void OnTokenReceived(object sender, TokenReceivedEventArgs token)
    {
        if(token != null)
            Debug.LogFormat("[FIREBASE]Received Registration Token: {0}", token.Token);
    }
    
    void OnMessageReceived(object sender, MessageReceivedEventArgs token)
    {
        if(token != null && token.Message != null && token.Message.From != null)
            Debug.LogFormat("[FIREBASE]Received a new message from: {0}, Title: {1}, Text: {2}", token.Message.From, token.Message.Notification.Title, token.Message.Notification.Body);
    }
    
}
