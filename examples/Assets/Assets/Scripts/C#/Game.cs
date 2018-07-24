using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour {
	
	//Wave
	public static int curWave;				//The wave that the player has currently reached
	public static float curWaveTime;		//The time remaining in the current wave (seconds)
	public float waveTime;					//Depicts how long a wave will go on for (seconds)
	public GameObject[] spawnPoints;		//An array holding all enemy spawn points on the map
	public float spawnRate;					//The rate of which an enemy will spawn (seconds before each spawn)
	private float spawnRateTimer;			//The timer that counts down from the defined spawnRate (seconds)
	private int prevSpawn;					//An index number from spawnPoints, which is the last point that an enemy spawned at
	public static bool waveInProgress;		//Is the wave currently in progress
	public static int score;				//The current score
	
	//Pickups
	public GameObject[] pickups;			//An array holding all the available pickups
	public float pickupRate;				//The rate of which a pickup will spawn (seconds before each spawn)
	private float pickupRateTimer;			//The timer that counts down from the defined pickupRate (seconds)
	
	//Enemy
	public GameObject[] enemies;			//An array holding the different types of enemies that can be spawned on the map
	
	//UI
	public Button nextWaveButton;			//The button that when pressed, will begin the next wave
	
	void Start ()
	{
		BeginNextWave();
		UI.isPaused = false;
	}
	
	void Update ()
	{
		if(waveInProgress){
			spawnRateTimer += 1.0f * Time.deltaTime;
			pickupRateTimer += 1.0f * Time.deltaTime;
			curWaveTime -= 1.0f * Time.deltaTime;
			
			if(spawnRateTimer >= spawnRate){
				spawnRateTimer = 0.0f;
				SpawnEnemy();
			}
			
			if(pickupRateTimer >= pickupRate){
				pickupRateTimer = 0.0f;
				SpawnPickup();
			}
			
			if(curWaveTime <= 0){
				EndWave();
			}
		}
	}

	public void BeginNextWave ()
	{
		waveInProgress = true;
		curWave ++;
		spawnRate -= 0.3f;
		curWaveTime = waveTime;
		Shop.canOpen = false;
		nextWaveButton.gameObject.active = false;
		
		Player.curHp = Player.maxHp;
		
		if(Player.hasExtendedMag){
			Player.ammo = Player.startAmmo * 2;
		}else{
			Player.ammo = Player.startAmmo;
		}
	}
	
	void EndWave ()
	{
		waveInProgress = false;
		nextWaveButton.gameObject.active = true;
		Shop.canOpen = true;
		curWaveTime = 0.0f;
	}
	
	void SpawnEnemy ()
	{
		int ranEnemy;
		int ranSpawn;
		
		ranEnemy = Random.Range(0, enemies.Length);
		ranSpawn = Random.Range(0, spawnPoints.Length);
		
		if(ranSpawn == prevSpawn){
			SpawnEnemy();
			return;
		}
		
		Instantiate(enemies[ranEnemy], spawnPoints[ranSpawn].transform.position, transform.rotation);
		prevSpawn = ranSpawn;
	}
	
	void SpawnPickup ()
	{
		Vector3 pos = new Vector3(0,0,0);
		int pickup;
		
		pos.x = Random.Range(-220, 220);
		pos.y = Random.Range(-220, 220);
		
		pickup = Random.Range(0, pickups.Length);
		
		Instantiate(pickups[pickup], pos, transform.rotation);
	}
	
	public void GoToMenu ()
	{
		Application.LoadLevel(0);
	}
}
