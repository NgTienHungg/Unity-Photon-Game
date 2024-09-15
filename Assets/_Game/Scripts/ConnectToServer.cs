using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

namespace Game
{
    public class ConnectToServer : MonoBehaviourPunCallbacks
    {
        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to master");
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
            SceneManager.LoadScene(GameConfig.Scene.Lobby);
        }
    }
}