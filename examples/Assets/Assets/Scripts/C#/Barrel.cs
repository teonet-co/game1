using UnityEngine;
using System.Collections;

public class Barrel : MonoBehaviour {
	
	public int durability;				//How much damage the barrel can take before exploding
	public int damage;					//The amount of damage that the barrel deals once exploded
	public float explosionForce;		//The amount of force put upon the targets once exploded
	public float explosionRadius;		//The blast radius of the barrel
	public GameObject explosion;		//The explosion effect that is instantiated upon explosion
	public AudioClip explosionSound; 	//The audio clip that is played when the barrel explodes
	public AudioSource asource;      	//The barrel's audio source which will play the sounds
	
	void Start ()
	{
		asource.volume = PlayerPrefs.GetFloat("Volume");
	}
	
	void Update ()
	{
		if(durability < 1){
			StartCoroutine(Explode());
		}
	}
	
	IEnumerator Explode ()
	{		
		durability = 1000;
		
		foreach(GameObject x in GameObject.FindGameObjectsWithTag("Alive")){
			if(Vector2.Distance(transform.position, x.transform.position) <= explosionRadius){
				x.SendMessage("Damaged", damage);
				
				float xX = x.transform.position.x - transform.position.x;
				float xY = x.transform.position.y - transform.position.y;

				Rigidbody2D rig = x.GetComponent<Rigidbody2D>();
				rig.AddForce(new Vector2(xX, xY).normalized * explosionForce / Vector2.Distance(transform.position, x.transform.position) * 20);
			}
		}
		
		GameObject e = Instantiate(explosion, transform.position, explosion.transform.rotation) as GameObject;
		Destroy(e, 0.7f);
		
		asource.PlayOneShot(explosionSound);
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
		yield return new WaitForSeconds(2);
		Destroy(gameObject);
	}
	
	public void Damaged (int dmg)
	{
		durability -= dmg;
	}
}
