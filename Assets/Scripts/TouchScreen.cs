using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TouchScreen : MonoBehaviour
{
    public GameObject _catPrefab;

    ARRaycastManager _arRaycastManager;
    ARPlaneManager _arPlaneManager;
    GameObject _spawnedCat;

    void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        _arPlaneManager = GetComponent<ARPlaneManager>();
    }

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        var touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            if (_spawnedCat == null)
            {
                var hits = new List<ARRaycastHit>();
                if (_arRaycastManager.Raycast(touch.position, hits, TrackableType.Planes))
                {
                    SpawnObject(hits[0].pose.position);
                }
            }
            else
            {
                if (IsCatHit(touch.position))
                {
                    _spawnedCat.GetComponent<CatController>().Meow();
                }
            }
        }
    }

    /// <summary>
    /// spawn cat object into the world.
    /// </summary>
    /// <param name="hitPos"></param>
    void SpawnObject(Vector3 hitPos)
    {
        // カメラの方向を向くように生成オブジェクトを回転させる
        Vector3 camDirection = Camera.main.transform.position - hitPos;
        float angle = Mathf.Atan2(camDirection.x, camDirection.z) * Mathf.Rad2Deg;
        Vector3 rotVec = Vector3.up * angle;
        _spawnedCat = Instantiate(_catPrefab, hitPos, Quaternion.Euler(rotVec));
        foreach (ARPlane plane in _arPlaneManager.trackables)
        {
            Destroy(plane.gameObject);
        }
    }

    /// <summary>
    /// return true if the user touched the cat.
    /// </summary>
    /// <param name="touchPosOnScreen"></param>
    /// <returns></returns>
    bool IsCatHit(Vector2 touchPosOnScreen)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosOnScreen);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit))
        {
            if (raycastHit.collider.CompareTag("Cat"))
            {
                return true;
            }
        }

        return false;
    }
}
