using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnButton : MonoBehaviour {
	public SliderNumber zombunnyHoardSize;
	public SliderNumber zombearHoardSize;
	public SliderNumber hellephantHoardSize;
	public Button spawnButton;
	EnemySpawnManager spawnManager;

	void Start() {
		spawnManager = GetComponent<EnemySpawnManager> ();
	}

	public void Spawn(){
		Debug.Log ("Enemies spawned");
		int zombunny = zombunnyHoardSize.GetValue ();
		int zombear = zombearHoardSize.GetValue ();
		int hellephant = hellephantHoardSize.GetValue ();
		spawnManager.SpawnHellephant (hellephant);
		spawnManager.SpawnZombear (zombear);
		spawnManager.SpawnZombunny (zombunny);
	}
}
