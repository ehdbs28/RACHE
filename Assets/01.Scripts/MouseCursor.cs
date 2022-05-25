using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor: MonoBehaviour
{
    [SerializeField] private Texture2D _cursorTexture;
    [SerializeField] private bool _hotSpotIsCenter = false;
    [SerializeField] private Vector3 _adjustHotSpot = Vector3.zero;

    private Vector3 _hotSpot;

    private void Start()
    {
        StartCoroutine("MyCursor");
    }

    IEnumerator MyCursor()
    {
        yield return new WaitForEndOfFrame();

        if (_hotSpotIsCenter)
        {
            _hotSpot.x = _cursorTexture.width / 2;
            _hotSpot.y = _cursorTexture.height / 2;
        }
        else
        {
            _hotSpot = _adjustHotSpot;
        }

        Cursor.SetCursor(_cursorTexture, _hotSpot, CursorMode.Auto);
    }
}
