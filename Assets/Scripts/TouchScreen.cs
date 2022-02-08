using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TouchScreen : MonoBehaviour
{
    public GameObject _cat;
    ARRaycastManager _arRaycastManager;
    List<ARRaycastHit> _hits;

    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();

        _hits = new List<ARRaycastHit>();
    }

    private void Update()
    {
        if (Input.touchCount == 0)
            return;

        if (_arRaycastManager.Raycast(Input.GetTouch(0).position, _hits, TrackableType.Planes))
        {
            Instantiate(_cat, _hits[0].pose.position, Quaternion.identity);
        }
    }
}
