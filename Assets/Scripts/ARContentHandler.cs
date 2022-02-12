using UnityEngine;
using PretiaArCloud;

public class ARContentHandler : MonoBehaviour
{

    [SerializeField]
    private ARSharedAnchorManager _relocManager;

    [SerializeField]
    private GameObject _arContents;

    [SerializeField]
    private GameObject _pretiaMap;

    void OnEnable()
    {
        _relocManager.OnRelocalized += OnRelocalized;
        if (MenuButtonController.isDebug && _pretiaMap != null) {
            _pretiaMap.SetActive(true);
        } else {
            _pretiaMap.SetActive(false);
        }
    }

    void OnDisable()
    {
        _relocManager.OnRelocalized -= OnRelocalized;
    }

    private void OnRelocalized()
    {
        _arContents.SetActive(true);
    }
}