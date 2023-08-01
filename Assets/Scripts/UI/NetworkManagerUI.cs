using System;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class NetworkManagerUI : MonoBehaviour
    {
        [SerializeField] private Button _serverButton;
        [SerializeField] private Button _hostButton;
        [SerializeField] private Button _clientButton;
        [SerializeField] private TMP_InputField _idAddress;
        [SerializeField] private TMP_InputField _port;
        [SerializeField] private UnityTransport _transport;

        private void Awake()
        {
            _serverButton.onClick.AddListener(() => NetworkManager.Singleton.StartServer());
            _hostButton.onClick.AddListener(() => NetworkManager.Singleton.StartHost());
            _clientButton.onClick.AddListener(() => NetworkManager.Singleton.StartClient());
            _idAddress.onValueChanged.AddListener((address) => _transport.ConnectionData.Address = address);
            _port.onValueChanged.AddListener(port => _transport.ConnectionData.Port = ushort.Parse(port));
        }
    }
}