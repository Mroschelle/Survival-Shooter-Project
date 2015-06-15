using UnityEngine;
using System.Collections;

public class EnemySpawnManager : MonoBehaviour {
	public EnemyManager zombunnyManager;
	public EnemyManager zombearManager;
	public EnemyManager hellephantManager;
	public Transform zombunnySpawnLocation;
	public GameObject zombunny;
	NetworkView nView;

	void Start() {
		nView = GetComponent<NetworkView>();
	}

	public void SpawnHellephant(int hoardSize){
		nView.RPC("InitHellephant", RPCMode.Others, hoardSize);
	}

	public void SpawnZombunny(int hoardSize){
		nView.RPC("InitZombunny", RPCMode.Others, hoardSize);
		//Network.Instantiate (zombunny, zombunnySpawnLocation.position, zombunnySpawnLocation.rotation,0);
	}

	public void SpawnZombear(int hoardSize){
		nView.RPC("InitZombear", RPCMode.Others, hoardSize);
	}
	public void SetHealth (int amount){
		nView.RPC ("ZombunnyHealth", RPCMode.All, amount);
	}

	
	[RPC]
	void ZombunnyHealth(int amount) {
		zombunnyManager.SetHealth (amount);
	}
	[RPC]
	void InitZombunny(int size){
		zombunnyManager.SpawnHoard (size);;
	}
	[RPC]
	void InitZombear(int size){
		zombearManager.SpawnHoard (size);
	}
	[RPC]
	void InitHellephant(int size){
		hellephantManager.SpawnHoard (size);
	}

}
