using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor: MonoBehaviour
{
    public static MouseCursor Instance = null;

    [SerializeField] private Texture2D _cursorTexture;
    [SerializeField] private bool _hotSpotIsCenter = false;
    [SerializeField] private Vector3 _adjustHotSpot = Vector3.zero;

    private Vector3 _hotSpot;
    private Vector2 _mousePos;
    public Vector2 MousePos
    {
        get => _mousePos;
        set => value = _mousePos;
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("이미 MouseCursor 인스턴스를 사용중입니다.");
        }
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine("MyCursor");
    }

    private void Update()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(_mousePos);
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
