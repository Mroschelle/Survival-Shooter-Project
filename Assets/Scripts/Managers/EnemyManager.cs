using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
	public EnemyHealth enemyHealth;


    void Start ()
    {
		enemyHealth = GetComponent<EnemyHealth>();
        //InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }

	public void SetSpawnTime(){
	
	}
	public void SetHealth(int amount){
		enemyHealth.SetHealth (amount);
	}

	public void SpawnHoard (int hoardSize){
		for (int i = 0; i < hoardSize; i++) {
			StartCoroutine("Spawn");
		}                            
	}

    public IEnumerator Spawn ()
    {
        if(playerHealth.currentHealth <= 0f)
        {
            return false;
        }
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
		yield return new WaitForSeconds (spawnTime);
    }
}
