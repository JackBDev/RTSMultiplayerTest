using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Events;


public class Unit : NetworkBehaviour
{
    [SerializeField] private UnityEvent whenSelected = null;
    [SerializeField] private UnityEvent whenDeselected = null;
    [SerializeField] private UnitMovement unitMovement = null;

    public UnitMovement GetUnitMovement()
    {
        return unitMovement;
    }

    #region Client
    [Client]
    public void Select()
    {
        if(!hasAuthority)
        {
            return;
        }

        whenSelected?.Invoke();
    }

    [Client]
    public void Deselect()
    {
        if(!hasAuthority)
        {
            return;
        }

        whenDeselected?.Invoke();
    }
    #endregion
}
