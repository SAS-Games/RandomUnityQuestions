using System.Collections.Generic;
using UnityEngine;

public class UnitsHandler : MonoBehaviour
{
    [SerializeField] private UnitSelectionBox m_UnitSelectionBox;
    [SerializeField] private LayerMask m_GroundLayer;
    [SerializeField] private List<Unit> unitList = new List<Unit>();
    private List<Unit> selectedUnit = new List<Unit>();
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
        m_UnitSelectionBox.OnSelectionBoxDrawn += SelectUnitsInsideRect;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, m_GroundLayer))
            {
                SetUnitsDestination(hitInfo.point);
            }
        }
    }

    private void SetUnitsDestination(Vector3 destination)
    {
        foreach (var unit in selectedUnit)
        {
            unit.SetDestination(destination);
        }
    }


    private void SelectUnitsInsideRect(Rect rect)
    {
        selectedUnit.Clear();
        foreach (var unit in unitList)
        {
            if (rect.Contains(_camera.WorldToScreenPoint(unit.transform.position)))
            {
                selectedUnit.Add(unit);
            }
        }
    }
}
