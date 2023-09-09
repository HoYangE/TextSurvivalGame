using UnityEngine;
using UnityEngine.Rendering;

public class CameraResolution : MonoBehaviour
{
    private void Fill()
    {
        var cameraComponent = GetComponent<Camera>();
        var rect = cameraComponent.rect;
        var scaleHeight = ((float)Screen.width / Screen.height) / (9 / 19.5f);
        var scaleWidth = 1f / scaleHeight;
        
        if (scaleHeight < 1)
        {
            rect.height = scaleHeight;
            rect.y = (1f - scaleHeight) / 2f;
        }
        else
        {
            rect.width = scaleWidth;
            rect.x = (1f - scaleWidth) / 2f;
        }
        cameraComponent.rect = rect;
    }

    private void Awake()
    {
        Fill();
    }
    
    private void Start()
    {
        RenderPipelineManager.beginFrameRendering += (context, camera) => { GL.Clear(true, true, Color.black); };
    }
}
