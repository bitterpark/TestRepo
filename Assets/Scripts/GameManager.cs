using System;
using System.Collections;


using UnityEngine;
using UnityEngine.SceneManagement;


using Photon.Pun;
using Photon.Realtime;


namespace GameManagement
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        public static GameManager Instance;

        public GameObject playerPrefab;

        /// <summary>
        /// Local player leaves room and returns to start scene
        /// </summary>
        public override void OnLeftRoom() {
            SceneManager.LoadScene(0);
        }


        void Start() {
            Instance = this;
            if (playerPrefab == null) {
                Debug.LogError("Player prefab not set in Game Manager", this);
            } else {
                Debug.LogFormat("Spawning LocalPlayer");
                PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0, 0, -1), Quaternion.identity, 0);
            }
        }

        public void LeaveRoom() {
            Debug.Log("Leaving room");
            PhotonNetwork.LeaveRoom();
        }
    }
}