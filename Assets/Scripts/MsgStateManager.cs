using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PretiaArCloud;

public class MsgStateManager : MonoBehaviour
{
    bool _catIsVisible = false;
    bool _catLosing = false;
    public bool isCatVisible {
        get {
            return _catIsVisible;
        }
        set {
            _catIsVisible = value;
            if (!value) {
                _catLosing = true;
            }
            ChangeState();
        }
    }

    SharedAnchorState _relocState = SharedAnchorState.Initializing;
    public SharedAnchorState relocState {
        get {
            return _relocState;
        }
        set {
            _relocState = value;
            ChangeState();
        }
    }
    float _relocScore = 0;
    public float relocScore {
        get {
            return _relocScore;
        }
        set {
            _relocScore = value;
            ChangeState();
        }
    }
    
    Text thisObject;

    void OnEnable()
    {
        thisObject = this.gameObject.GetComponent<Text>();
    }

    void ChangeState()
    {
        if (_catLosing) {
            thisObject.text = "猫はどこだろう……";
            _catLosing = false;
            return;
        } else if (_catIsVisible) {
            thisObject.text = "あっ猫だ！\nかわいいね";
            if (SystemInfo.supportsVibration)
            {
                Handheld.Vibrate();
            }
            return;
        }

        switch (_relocState) {
            case SharedAnchorState.Initializing:
            case SharedAnchorState.Stopped:
                thisObject.text = "近くに猫が見あたらないようだ\nいろいろな場所を探してみよう";
                break;
            case SharedAnchorState.Relocalizing:
                if (_relocScore < 0.35) {
                    thisObject.text = "近くに猫が見あたらないようだ\nいろいろな場所を探してみよう";
                } else {
                    thisObject.text = "どこからか猫の鳴き声が聞こえた気がする！\nあたりを見回して探してみよう";
                }
                break;
            case SharedAnchorState.Relocalized:
                thisObject.text = "猫の鳴き声が聞こえた方向を探してみよう";
                break;
        }
    }
}
