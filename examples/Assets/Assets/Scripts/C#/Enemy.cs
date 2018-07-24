using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public int curHp;				//The enemy's current health
	public int maxHp; 				//The enemy's maximum health
	
	public float moveSpeed;			//The enemy's speed at which it can move at
	public int damage;				//The damage that the enemy will deal when attacking the target
	public float attackDist;		//The distance at which the enemy will stop moving towards the target to attack
	private float attackTimer;		//The timer counting up to the attackRate (seconds)
	public float attackRate;		//The time defining the length between enemy attacks (seconds)
	public float dist;				//The distance at which the enemy is from the target
	
	public int scoreToGive;			//The score that will be added once the enemy is killed
	public int moneyToGive;			//The money that will be added once the enemy is killed
	
	public GameObject target;		//The GameObject of which the enemy will move towards and attack
	public GameObject blood;		//The blood effect which will be instantiated once the enemy is hit
	
	//Audio
	public AudioSource asource;		//The enemy's AudioSource which sounds will be played through
	public AudioClip hitSound;		//The audio clip that will be played when the enemy gets damaged
	
	void Start ()
	{
		target = GameObject.Find("Player");
		moveSpeed = Random.Range(moveSpeed, moveSpeed + 5);
		
		asource.volume = PlayerPrefs.GetFloat("Volume");
	}
	
	void Update ()
	{
		Move();
		
		attackTimer += 1.0f * Time.deltaTime;
		dist = Vector2.Distance(transform.position, target.transform.position);
		
		if(curHp <= 0){
			Destroy(gameObject);
			Game.score += scoreToGive;
			Player.money += moneyToGive;
		}
	}
	
	void Move ()
	{
		if(dist > attackDist){
			transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
		}else{
			if(attackTimer >= attackRate){
				attackTimer = 0.0f;
				Attack();
			}
		}
		
		Vector3 dir = target.transform.position - transform.position; 
		transform.rotation = Quaternion.Euler(new Vector3(0,0,Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg - 90));
	}
	
	void Attack ()
	{
		target.SendMessage("Damaged", damage);
	}
	
	public void Damaged (int dmg)
	{
		curHp -= dmg;
		
		asource.PlayOneShot(hitSound);
		
		GameObject b = Instantiate(blood, transform.position, transform.rotation) as GameObject;
		Destroy(b, 0.2f);
	}

}
