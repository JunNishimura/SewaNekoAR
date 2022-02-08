using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TouchScreen : MonoBehaviour
{
    public GameObject cube;
    ARRaycastManager arRaycastManager;
    List<ARRaycastHit> hits;

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();

        hits = new List<ARRaycastHit>();
    }

    private void Update()
    {
        if (Input.touchCount == 0)
            return;

        if (arRaycastManager.Raycast(Input.GetTouch(0).position, hits))
        {
            if (hits[0].hitType == TrackableType.Planes)
            {
                Instantiate(cube, hits[0].pose.position, Quaternion.identity);
            }
        }
    }
}
