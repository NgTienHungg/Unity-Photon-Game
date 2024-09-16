using Photon.Pun;
using TMPro;
using UnityEngine;

namespace Game.Connection
{
    public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_InputField creatInput;
        [SerializeField] private TMP_InputField joinInput;

        public void CreateRoom()
        {
            PhotonNetwork.CreateRoom(creatInput.text);
        }

        public void JoinRoom()
        {
            PhotonNetwork.JoinRoom(joinInput.text);
        }

        public override void OnJoinedRoom()
        {
            PhotonNetwork.LoadLevel(GameConfig.Scene.Game);
        }
    }
}