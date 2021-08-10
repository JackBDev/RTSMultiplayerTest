using System;
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

    public static event Action<Unit> ServerOnUnitSpawned;
    public static event Action<Unit> ServerOnUnitDespawned;
    public static event Action<Unit> AuthorityOnUnitSpawned;
    public static event Action<Unit> AuthorityOnUnitDespawned;

    public UnitMovement GetUnitMovement()
    {
        return unitMovement;
    }

    #region Server
    [Server]
    public override void OnStartServer()
    {
        ServerOnUnitSpawned?.Invoke(this);
    }

    [Server]
    public override void OnStopServer()
    {
        ServerOnUnitDespawned?.Invoke(this);
    }

    #endregion

    #region Client
    [Client]
    public override void OnStartClient()
    {
        if(!isClientOnly || !hasAuthority)
        {
            return;
        }

        AuthorityOnUnitSpawned?.Invoke(this);
    }

    [Client]
    public override void OnStopClient()
    {
        if(!isClientOnly || !hasAuthority)
        {
            return;
        }

        AuthorityOnUnitDespawned?.Invoke(this);
    }

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
