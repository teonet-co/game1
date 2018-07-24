using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
	
	public PickupType type;			//The type of pickup
	
	public int hpToGive;			//The health to give to the player once it has picked this up
	public int ammoToGive;			//The ammo to give to the player once it has picked this up
	public int moneyToGive;			//The money to give to the player once it has picked this up
	
	public AudioClip pickupSound;	//The sound that plays once the player picks up the pickup
	
	void OnTriggerEnter2D (Collider2D col)
	{
		if(col.gameObject.name == "Player"){
			if(type == PickupType.Health){ 
				Player.curHp += hpToGive;
			}
			if(type == PickupType.Ammo){
				Player.ammo += ammoToGive;
			}
			if(type == PickupType.Money){
				Player.money += moneyToGive;
			}
			
			col.gameObject.GetComponent<AudioSource>().PlayOneShot(pickupSound);
			
			Destroy(gameObject);
		}
	}
	
	public enum PickupType {
		Health,
		Ammo,
		Money,
	}
}
