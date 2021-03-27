using UnityEngine;
using System.Collections;
using Photon.Pun;

namespace PlayerManagement
{
    public class PlayerMovement : MonoBehaviourPun
    {
        #region MonoBehaviour Callbacks

        [SerializeField]
        float movementSpeed;

        void Update() {
            if (photonView.IsMine == false && PhotonNetwork.IsConnected == true) {
                return;
            }

            float hor = Input.GetAxis("Horizontal");
            float vert = Input.GetAxis("Vertical");
            
            GetComponent<CharacterController>().Move(new Vector3(hor, vert, 0) * movementSpeed * Time.deltaTime);
        }


        #endregion
    }
}