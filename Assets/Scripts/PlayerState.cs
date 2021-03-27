using GameManagement;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerManagement
{
    public class PlayerState : MonoBehaviourPunCallbacks, IPunObservable
    {
        public float Health = 1f;

        [SerializeField]
        GameObject deathParticles;

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
            if (stream.IsWriting) {
                stream.SendNext(Health);
            } else {
                Health = (float)stream.ReceiveNext();
            }
        }


        void Start() {
            Respawn();
        }

        void OnTriggerEnter(Collider other) {
            if (!photonView.IsMine || !other.gameObject.tag.Contains("Player") || other.gameObject == gameObject) {
                return;
            }
            Health -= 1f;
            Debug.Log($"HEALTH:{Health}");
        }


        private void Update() {
            if (Health <= 0) {
                Instantiate(deathParticles, transform.position, Quaternion.identity);
                if (photonView.IsMine) {
                    Respawn();
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape)) {
                GameManager.Instance.LeaveRoom();
            }
        }

        void Respawn() {
            Health = 3;
            float xBounds = 6.5f;
            float yBounds = 2.5f;
            float randomXPos = Random.Range(-xBounds, xBounds);
            float randomYPos = Random.Range(-yBounds, yBounds);
            GetComponent<CharacterController>().enabled = false;
            transform.position = new Vector3(randomXPos, randomYPos, -1);
            GetComponent<CharacterController>().enabled = true;
        }
    }
}
