using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitCommandGiver : MonoBehaviour
{
    [SerializeField] private UnitSelectionHandler unitHandler = null;
    [SerializeField] private LayerMask layerMask = new LayerMask();

    private Camera mainCamera;

    private void Start()
    {
            mainCamera = Camera.main;
    }

    private void Update()
    {
        if(!Mouse.current.leftButton.wasPressedThisFrame)
        {
            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        
        if(!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            return;
        }

        Debug.Log(hit.point);

        TryMove(hit.point);
    }

    //**ERRROR**//
    private void TryMove(Vector3 point)
    {
        Debug.Log(unitHandler.SelectedUnits.Count);
        foreach(Unit unit in unitHandler.SelectedUnits)
        {
            unit.GetUnitMovement().CmdMovement(point);
        }
    }
}