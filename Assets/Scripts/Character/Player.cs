using System;
using Unity.Netcode;
using UnityEngine;

namespace Character
{
    public class Player : NetworkBehaviour
    {
        public static Player Main;
        public int _click0;
        public int _click1;

        public static Player Secondary;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (Main == null)
                Main = this;
            else if (Secondary == null)
                Secondary = this;
            else
            {
                Debug.LogError("What?");
            }
        }

        private void Update()
        {
            if (!IsOwner) return;
            
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                Debug.Log(OwnerClientId);
                Vote0ServerRpc((int)OwnerClientId);
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log(OwnerClientId);
                Vote1ServerRpc((int)OwnerClientId);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                MoveServerRpc((int)OwnerClientId);
            }
            
            var input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            transform.Translate(input * (3 * Time.deltaTime));
        }

        [ServerRpc]
        public void Vote0ServerRpc(int playerId)
        {
            Demo.Ins.Vote0(playerId);
        }
        
        [ServerRpc]
        public void Vote1ServerRpc(int playerId)
        {
            Demo.Ins.Vote1(playerId);
        }

        [ServerRpc]
        public void MoveServerRpc(int playerId)
        {
            if (playerId > 0)
                Avatar.Ins.transform.Translate(-1, 0, 0);
            else
            {
                Avatar.Ins.transform.Translate(1, 0, 0);
            }
        }
    }
}