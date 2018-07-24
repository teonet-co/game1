using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shop : MonoBehaviour {
	
	//Shop
	public static bool isOpen;			//Is the shop window currently open
	public static bool canOpen;			//Are you currently able to open the shop window
	public GameObject shop;				//The shop window that can be opened and closed
	
	//Prices
	public int speedPrice;				//The current price to upgrade the player's speed
	public int healthPrice;				//The current price to upgrade the player's health
	
	public int sightPrice;				//The price to buy the Sight upgrade
	public int extendedMagPrice;		//The price to buy the Extended Mag upgrade
	public int laserPrice;				//The price to buy the Laser upgrade
	
	//Stats to increase
	public int speedToIncrease;			//The current speed that will be increased once the player has purchased speed
	public int healthToIncrease;		//The current health that will be increased once the player has purchased health
	
	//Gun Model
	public GameObject sight;			//The Sight sprite that is featured in the shop window
	public GameObject extendedMag;		//The Extended Mag sprite that is featured in the shop window
	public GameObject laser;			//The Laser sprite that is featured in the shop window
	
	//UI
	public Text speedTxt;				//The Text component of the Speed upgrade button in the shop window
	public Text healthTxt;				//The Text component of the Health upgrade button in the shop window
	
	public Text sightTxt;				//The Text component of the Sight upgrade button in the shop window
	public Text extendedMagTxt;			//The Text component of the Extended Mag upgrade button in the shop window
	public Text laserTxt;				//The Text component of the Laser upgrade button in the shop window
	
	void Start ()
	{
		isOpen = false;
	}
	
	void Update ()
	{	
		if(isOpen){
			Text();
		}
	}
	
	void Text ()
	{
		//Player
		speedTxt.text = "Speed - $" + speedPrice;
		healthTxt.text = "Health - $" + healthPrice;
		
		//Gun
		sightTxt.text = "Sight - $" + sightPrice;
		extendedMagTxt.text = "Ext. Mag - $" + extendedMagPrice;
		laserTxt.text = "Laser - $" + laserPrice;
	}
	
	public void Buy (string item){
		//Player
		if(item == "speed"){
			if(Player.money >= speedPrice){
				Player.moveSpeed += speedToIncrease;
				Player.money -= speedPrice;
				speedPrice *= 2;
			}
		}
		if(item == "health"){
			if(Player.money >= healthPrice){
				Player.maxHp += healthToIncrease;
				Player.money -= healthPrice;
				healthPrice *= 2;
			}
		}
		
		//Gun
		if(item == "sight"){
			if(Player.money >= sightPrice){
				sight.active = true;
				Player.money -= sightPrice;
				Player.hasSight = true;
				Player.shootDist = 500;
				sightTxt.transform.parent.gameObject.GetComponent<Button>().interactable = false;
			}
		}
		if(item == "mag"){
			if(Player.money >= extendedMagPrice){
				extendedMag.active = true;
				Player.money -= extendedMagPrice;
				Player.hasExtendedMag = true;
				extendedMagTxt.transform.parent.gameObject.GetComponent<Button>().interactable = false;
			}
		}
		if(item == "laser"){
			if(Player.money >= laserPrice){
				laser.active = true;
				Player.money -= laserPrice;
				Player.hasLaser = true;
				laserTxt.transform.parent.gameObject.GetComponent<Button>().interactable = false;
			}
		}
	}
	
	public void ToggleShop ()
	{
		if(canOpen){
			if(isOpen){
				shop.active = false;
				isOpen = false;
			}else{
				shop.active = true;
				isOpen = true;
			}
		}
	}
}
