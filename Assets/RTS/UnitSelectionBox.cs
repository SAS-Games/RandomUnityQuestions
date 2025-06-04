using System;
using UnityEngine;

public class UnitSelectionBox : MonoBehaviour
{
    public Action<Rect> OnSelectionBoxDrawn;
    [SerializeField] private RectTransform m_BoxVisual;

    private Camera _camera;
    private Vector2 _startPosition;
    private Vector2 _endPosition;
    private Rect _selectionBox;

    private void Start()
    {
        _camera = Camera.main;
        ClearSelectionBox();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startPosition = Input.mousePosition;
            _endPosition = _startPosition;
            _selectionBox = new Rect();
            UpdateVisualBox();
        }

        if (Input.GetMouseButton(0))
        {
            _endPosition = Input.mousePosition;
            _selectionBox = GetScreenRect(_startPosition, _endPosition);
            UpdateVisualBox();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnSelectionBoxDrawn?.Invoke(_selectionBox);
            ClearSelectionBox();
        }
    }

    private void UpdateVisualBox()
    {
        m_BoxVisual.gameObject.SetActive(true);

        Vector2 center = (_startPosition + _endPosition) / 2f;
        Vector2 size = new Vector2(
            Mathf.Abs(_startPosition.x - _endPosition.x),
            Mathf.Abs(_startPosition.y - _endPosition.y)
        );

        m_BoxVisual.position = center;
        m_BoxVisual.sizeDelta = size;
    }

    private Rect GetScreenRect(Vector2 start, Vector2 end)
    {
        float xMin = Mathf.Min(start.x, end.x);
        float yMin = Mathf.Min(start.y, end.y);
        float width = Mathf.Abs(start.x - end.x);
        float height = Mathf.Abs(start.y - end.y);

        return new Rect(xMin, yMin, width, height);
    }

    private void ClearSelectionBox()
    {
        _startPosition = _endPosition = Vector2.zero;
        m_BoxVisual.sizeDelta = Vector2.zero;
        m_BoxVisual.gameObject.SetActive(false);
    }
}
