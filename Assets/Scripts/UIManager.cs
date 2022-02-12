using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button _cameraButton;

    public void OnPressedCameraButton()
    {
        StartCoroutine(CaptureAndSaveScreenShot("test.png"));
    }

    IEnumerator CaptureAndSaveScreenShot(string fileName)
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(ss, "Gallery", fileName, (success, path) => Debug.Log("Media save result: " + success + " " + path));

        Destroy(ss);
    }
}
