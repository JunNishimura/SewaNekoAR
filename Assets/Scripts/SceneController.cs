using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PretiaArCloud;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private ARSharedAnchorManager _relocManager;

    [SerializeField]
    private Text _textLabel;

    [SerializeField]
    private Text _textLabel2;

    [SerializeField]
    private MsgStateManager _msgStateManager;

    [SerializeField]
    private GameObject _cat;

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
                _textLabel.text = "Initializing";
                _cat.SetActive(false);
                break;
            case SharedAnchorState.Stopped:
                _textLabel.text = "Stopped";
                _cat.SetActive(false);
                break;
            case SharedAnchorState.Relocalizing:
                _textLabel.text = "Relocalizing";
                break;
            case SharedAnchorState.Relocalized:
                _textLabel.text = "Relocalized";
                _cat.SetActive(true);
                break;
            // default:
            //     _textLabel.text = "default";
        }
        _msgStateManager.relocState = state;
    }

    private void OnScoreUpdated(float score)
    {
        _textLabel2.text = string.Format("Score: {0:#.##}", score*100);
        _msgStateManager.relocScore = score;
        if (score < 0.35) {
            _cat.SetActive(false);
        } else {
            _cat.SetActive(true);
        }
    }

    // private void OnRelocalized()
    // {
    //     _startButton.gameObject.SetActive(false);
    // }
}
