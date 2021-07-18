using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Mirror;

public class UnitMovement : NetworkBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;

    private Camera mainCamera;

    #region server

    [Command] private void CmdMovement(Vector3 targetPosition)
    {
        if(!NavMesh.SamplePosition(targetPosition, out NavMeshHit hit, 1f, NavMesh.AllAreas))
        {
            return;
        }

        agent.SetDestination(hit.position);
    }

    #endregion

    #region client

    public override void OnStartAuthority()
    {
        mainCamera = Camera.main;

        //base.OnStartAuthority();
    }

    // Client Callback means that only the client will run update. Server will not run it.
    [ClientCallback] private void Update()
    {
        if(!hasAuthority)
        {
            // If it's not the player's local client, don't do anything.
            return;
        }

        if(!Mouse.current.leftButton.wasPressedThisFrame)
        {
            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if(!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            return;
        }

        CmdMovement(hit.point);
    }

    #endregion
}