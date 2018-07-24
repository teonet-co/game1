using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI : MonoBehaviour {
	
	public Text scoreTxt;		//The Text component depicting the current score
	public Text ammoTxt;		//The Text component depicting the player's current ammo
	public Text moneyTxt;		//The Text component depicting the player's current money
	public Text waveTxt;		//The Text component depicting the current wave
	public Text waveTimeTxt;	//The Text component depicting the time left in the wave
	
	public Slider healthBar;	//The Slider which shows the players health 
	public Text hpTxt;			//The Text component depicting the players current and maximum health

	public GameObject pauseScreen;	//The pause screen that will go up when the game has been paused
	public static bool isPaused;	//Is the game currently paused?

	void Update ()
	{
		scoreTxt.text = "Score: " + Game.score;
		ammoTxt.text = "Ammo: " + Player.ammo;
		moneyTxt.text = "Money: $" + Player.money;
		waveTxt.text = "Wave: " + Game.curWave;
		waveTimeTxt.text = "Time Left: " + Game.curWaveTime;
		
		healthBar.value = Player.curHp;
		healthBar.maxValue = Player.maxHp;
		hpTxt.text = Player.curHp + "/" + Player.maxHp;

		if(Input.GetKeyDown(KeyCode.Escape)){
			if(isPaused){
				pauseScreen.active = false;
				isPaused = false;
				Time.timeScale = 1.0f;
			}else{
				pauseScreen.active = true;
				isPaused = true;
				Time.timeScale = 0.0f;
			}
		}
	}

	public void ResumeButton ()
	{
		pauseScreen.active = false;
		isPaused = false;
		Time.timeScale = 1.0f;
	}

	public void MenuButton ()
	{
		isPaused = false;
		Time.timeScale = 1.0f;
		Application.LoadLevel(0);
	}
}
