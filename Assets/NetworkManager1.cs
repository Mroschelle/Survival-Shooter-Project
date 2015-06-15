using UnityEngine;
using System.Collections;

public class NetworkManagerSS : MonoBehaviour {
	public string IP = "127.0.0.1 ";
	public int Port = 25001;
	public EnemyManager Spawner;
	public NetworkView nView;
	void Start() {
		nView = GetComponent<NetworkView>();
	}
	public void CreateServer() {
		if (Network.peerType == NetworkPeerType.Disconnected) {
			bool useNat = !Network.HavePublicAddress ();
			Network.InitializeServer (10, Port, useNat);
			Debug.Log("Server");
		}
	}
	public void ConnectClient() {
		if (Network.peerType == NetworkPeerType.Disconnected) {
			Network.Connect (IP, Port);
			nView.RPC ("Con",RPCMode.Server);
		}
	}

	public void SpawnEnemy(){
		nView.RPC ("Enemy", RPCMode.All);
	}
	public void Disconnect(){
		Network.Disconnect(250);
	}
	[RPC]
	void Enemy(){
		if (Network.isClient) {
			Spawner.Spawn ();
			Debug.Log("Enemy Spawned");
		}
	}

	[RPC]
	void Con(){
		Debug.Log ("Connected");
	}


}
