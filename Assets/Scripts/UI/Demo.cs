using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class Demo : MonoBehaviour
{
    public static Demo Ins;
    public TMP_Text Text0;
    public TMP_Text Text1;
    private int[] _count0;
    private int[] _count1;

    private void Awake()
    {
        Ins = this;
    }

    private void Start()
    {
        _count0 = new int[2];
        _count1 = new int[2];
    }

    private void Refresh()
    {
        Text0.text = $"Player 0 has clicked button0 {_count0[0]} times, button1 {_count1[0]} times";
        Text1.text = $"Player 1 has clicked button1 {_count0[1]} times, button1 {_count1[1]} times";
    }

    public void Vote0(int playerId)
    {
        _count0[playerId]++;
        Refresh();
        // VoteServerRpc(0, new ServerRpcParams());
    }

    public void Vote1(int playerId)
    {
        _count1[playerId]++;
        Refresh();
        // VoteServerRpc(1, new ServerRpcParams());
    }
    
   [ServerRpc]
   public void VoteServerRpc(int buttonId, ServerRpcParams serverRpcParams)
   {
       Debug.Log(serverRpcParams.Receive.SenderClientId);
       if (serverRpcParams.Receive.SenderClientId == 0)
       {
           _count0[buttonId]++;
       }
       else
       {
           _count1[buttonId]++;
       }
       
       Refresh();
   }
}
