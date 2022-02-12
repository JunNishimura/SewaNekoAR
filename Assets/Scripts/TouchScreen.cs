using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TouchScreen : MonoBehaviour
{
    public GameObject _cat;

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        var touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            if (IsCatHit(touch.position))
            {
                _cat.GetComponent<CatController>().Meow();
            }
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
