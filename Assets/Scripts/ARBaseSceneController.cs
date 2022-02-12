using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PretiaArCloud;

public class ARBaseSceneController : MonoBehaviour
{

    [SerializeField]
    private ARSharedAnchorManager _relocManager;

    [SerializeField]
    private GameObject _messageBubble;

    [SerializeField]
    private Text _messageLabel;

    private bool _catIsVisible = false;
    private bool _catLosing = false;
    public bool catIsVisible {
        get { return _catIsVisible; }
        set {
            _catIsVisible = value;
            if (!value) {
                _catLosing = true;
            }
            UpdateMessageBubble();
        }
    }
    private SharedAnchorState _relocState = SharedAnchorState.Initializing;
    public SharedAnchorState relocState {
        get { return _relocState; }
        set {
            _relocState = value;
            UpdateMessageBubble();
        }
    }
    private const float _scoreStepNum = 5;
    private float _relocScore = 0;
    public float relocScore {
        get { return _relocScore; }
        set {
            bool shouldUpdateMessage = false;
            if (
                Mathf.Floor(
                    _relocScore / (1/_scoreStepNum)
                ) != Mathf.Floor(
                    value / (100/_scoreStepNum)
                )
            ) {
                shouldUpdateMessage = true;
            }
            _relocScore = value;
            if (shouldUpdateMessage) { UpdateMessageBubble(); }
        }
    }

    void OnEnable()
    {
        _relocManager.OnSharedAnchorStateChanged += OnSharedAnchorStateChanged;
        _relocManager.OnScoreUpdated += OnScoreUpdated;
        // _relocManager.OnRelocalized += OnRelocalized;
    }

    void OnDisable()
    {
        _relocManager.OnSharedAnchorStateChanged -= OnSharedAnchorStateChanged;
        _relocManager.OnScoreUpdated -= OnScoreUpdated;
        // _relocManager.OnRelocalized -= OnRelocalized;
    }

    private void OnSharedAnchorStateChanged(SharedAnchorState state)
    {
        switch (state)
        {
            case SharedAnchorState.Initializing:
                _messageLabel.text = "Initializing";
                break;
            case SharedAnchorState.Stopped:
                _messageLabel.text = "Stopped";
                break;
            case SharedAnchorState.Relocalizing:
                _messageLabel.text = "Relocalizing";
                break;
            case SharedAnchorState.Relocalized:
                _messageLabel.text = "Relocalized";
                break;
        }
        relocState = state;
    }

    private void OnScoreUpdated(float score)
    {
        relocScore = score;
    }

    // private void OnRelocalized()
    // {
    //     _startButton.gameObject.SetActive(false);
    // }

    private void UpdateMessageBubble()
    {
        if (MenuButtonController.isDebug) {
            switch (_relocState)
            {
                case SharedAnchorState.Initializing:
                    _messageLabel.text = "State: Initializing";
                    break;
                case SharedAnchorState.Stopped:
                    _messageLabel.text = "State: Stopped";
                    break;
                case SharedAnchorState.Relocalizing:
                    _messageLabel.text = "State: Relocalizing";
                    break;
                case SharedAnchorState.Relocalized:
                    _messageLabel.text = "State: Relocalized";
                    break;
            }
            _messageLabel.text += string.Format("\nScore:{0}", _relocScore);
            return;
        }
        if (_catLosing) {
            _messageBubble.SetActive(true);
            _messageLabel.text = "ねこがどこに行ってしまったのだろう……。";
            _catLosing = false;
            return;
        } else if (_catIsVisible) {
            _messageBubble.SetActive(false);
            _messageLabel.text = "";
            if (SystemInfo.supportsVibration) { Handheld.Vibrate(); }
            return;
        }
        _messageBubble.SetActive(true);
        switch (Mathf.Floor(_relocScore/(1/_scoreStepNum))) {
            case 0: // 0% <= Score < 20%
                _messageLabel.text = "どこにもねこが見当たらない……。";
                break;
            case 1: // 20% <= Score < 40%
                _messageLabel.text = "なんとなくねこの気配がする……。\nあちこち見回して探してみよう。";
                break;
            case 2: // 40% <= Score < 60%
                _messageLabel.text = "ねこが近くにいるような気がする。\nもう少しよく見回してみよう。";
                break;
            case 3: // 60% <= Score < 80%
                _messageLabel.text = "ねこの声が聞こえた気がする！\n念入りに見回してみよう。";
                break;
            case 4: // 80% <= Score < 100%
                _messageLabel.text = "もう少しでねこを見つけられる気がする！\n念入りに見回してみよう。";
                break;
            case 5: // 100% = Score
                _messageLabel.text = "きっと近くにねこがいるはずだ！";
                break;
            default:
                _messageLabel.text = _relocScore.ToString();
                break;
        }
    }
}
