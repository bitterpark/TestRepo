using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;

namespace GameManagement
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private byte maxPlayersPerRoom = 3;

        [SerializeField]
        private GameObject controlPanel;
        [SerializeField]
        private GameObject progressLabel;
        [SerializeField]
        private GameObject errorLabel;

        string gameVersion = "1";
        bool isConnecting;


        void Awake() {
            PhotonNetwork.AutomaticallySyncScene = true;
        }


        void Start() {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
            errorLabel.SetActive(false);
        }


        public void Quit() {
            Application.Quit();
        }


        public void Connect() {
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);
            errorLabel.SetActive(false);

            if (PhotonNetwork.IsConnected) {
                PhotonNetwork.JoinRandomRoom();
            } else {
                isConnecting = PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;

            }
        }



        public override void OnConnectedToMaster() {
            if (isConnecting) {
                PhotonNetwork.JoinRandomRoom();
                isConnecting = false;
            }
        }


        public override void OnDisconnected(DisconnectCause cause) {
            progressLabel.SetActive(false);
            errorLabel.SetActive(false);
            controlPanel.SetActive(true);
            isConnecting = false;
        }

        public override void OnJoinRandomFailed(short returnCode, string message) {
            if (PhotonNetwork.CountOfRooms == 0) {
                PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = maxPlayersPerRoom });
            } else {
                StartCoroutine(ShowJoinErrorMessage(message));
            }
        }

        IEnumerator ShowJoinErrorMessage(string message) {
            progressLabel.SetActive(false);
            controlPanel.SetActive(false);
            errorLabel.SetActive(true);
            yield return new WaitForSeconds(2f);
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
            errorLabel.SetActive(false);
        }


        public override void OnJoinedRoom() {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1) {
                Debug.Log("Loading the room");
                PhotonNetwork.LoadLevel("Arena");
            }
        }

    }

}

