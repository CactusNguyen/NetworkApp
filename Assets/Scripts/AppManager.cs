using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public GameObject ClientUI;
    public GameObject ServerUI;
    public bool IsServer;
    
    private void Start()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.StartServer();
            ServerUI.SetActive(true);
        }
        else
        {
            // NetworkManager.Singleton.StartClient();
            ClientUI.SetActive(true);
        }
    }
}