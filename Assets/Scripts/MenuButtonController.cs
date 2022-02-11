using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour
{
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
            default:
                break;
        }
    }
}
