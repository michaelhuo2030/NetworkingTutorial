using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnHolaCountChanged))]
    int OnHolaCount = 1;

    void HandleMovement()
    {
        if (isLocalPlayer)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal * 0.1f, moveVertical * 0.1f, 0);
            transform.position = transform.position + movement;
        }
    }

    void Update()
    {
        HandleMovement();
        if(isLocalPlayer && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Send Hola to server from client.");
            Hola();
        }

        if(isServer && transform.position.y > 10  )
        {
            Toohigh();
        }
    }

    [Command]
    void Hola()
    {
        Debug.Log("Received Hola from client.");
        OnHolaCount += 1;
        RecHola();
        
    }

    [TargetRpc]
    void RecHola()
    {
        Debug.Log("Received Hola from server.");
    }

    [ClientRpc]
    void Toohigh()
    {
        Debug.Log("toohigh.");
    }

    void OnHolaCountChanged(int oldcount, int newcount)
    {
        Debug.Log($"we had {oldcount} holas, now we have {newcount} holas. ");
    }
}
