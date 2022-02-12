using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonController : MonoBehaviour
{
    public static bool isDebug = true;

    public void OnClick()
    {
        string objTitle = this.gameObject.name;
        // Debug.Log(objTitle);
        switch (objTitle) {
            case "王子駅前":
                SceneManager.LoadSceneAsync("OjiStationAR");
                break;
            case "花壇前":
                SceneManager.LoadSceneAsync("FlowersAR");
                break;
            case "石鍋商店前":
                SceneManager.LoadSceneAsync("IshinabeStoreAR");
                break;
            case "王子稲荷前":
                SceneManager.LoadSceneAsync("InariShrineAR");
                break;
            case "名主の滝公園前":
                SceneManager.LoadSceneAsync("NanushiParkAR");
                break;
            case "Main":
                SceneManager.LoadSceneAsync("Main");
                break;
            default:
                SceneManager.LoadSceneAsync("_BaseARTemplate");
                break;
        }
    }

    public void TapDebugToggle(bool isOn)
    {
        // bool isDebug = this.gameObject.GetComponent<Toggle>().isOn;
        MenuButtonController.isDebug = isOn;
    }
}
