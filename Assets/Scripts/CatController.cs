using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatController : MonoBehaviour
{
    [SerializeField]
    private MsgStateManager _msgStateManager;

    void OnBecameVisible() {
        _msgStateManager.isCatVisible = true;
    }

    void OnBecameInvisible() {
        _msgStateManager.isCatVisible = false;
    }
}
