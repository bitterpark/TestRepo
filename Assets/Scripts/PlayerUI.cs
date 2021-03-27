using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace PlayerManagement
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField]
        private Text playerNameText;

        [SerializeField]
        private Slider playerHealthSlider;

        [SerializeField]
        private PlayerState state;


		private void Awake() {
            SetTarget(state);
		}

		void Update() {
            if (playerHealthSlider != null) {
                playerHealthSlider.value = state.Health;
            }
        }


        void SetTarget(PlayerState state) {
            if (state == null) {
                Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
                return;
            }

            this.state = state;
            if (playerNameText != null) {
				playerNameText.text = state.photonView.IsMine ? "Me" : "Enemy";
            }
        }


    }
}