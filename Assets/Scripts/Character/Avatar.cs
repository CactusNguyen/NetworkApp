using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Character
{
    public class Avatar : NetworkBehaviour
    {
        public static Avatar Ins;

        private void Awake()
        {
            ClientManager.Ins.Avatar = this;
        }

        [ServerRpc]
        public void SubmitServerRpc(string playerName, string result0, string result1, string result2, string result3, string result4, string result5, string result6, string result7, string result8)
        {
            var result = new List<string>();

            if (!string.IsNullOrEmpty(result0))
                result.Add(result0);
            
            if (!string.IsNullOrEmpty(result1))
                result.Add(result1);
            
            if (!string.IsNullOrEmpty(result2))
                result.Add(result2);
            
            if (!string.IsNullOrEmpty(result3))
                result.Add(result3);
            
            if (!string.IsNullOrEmpty(result4))
                result.Add(result4);
            
            if (!string.IsNullOrEmpty(result5))
                result.Add(result5);
            
            if (!string.IsNullOrEmpty(result6))
                result.Add(result6);
            
            if (!string.IsNullOrEmpty(result7))
                result.Add(result7);
            
            if (!string.IsNullOrEmpty(result8))
                result.Add(result8);

            if (ServerManager.Ins != null)
            {
                ServerManager.Ins.HandleSubmit(playerName, result);
            }
        }
    }
}
