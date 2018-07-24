using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {
	
	public int highscore;			//Holds the player's highscore
	public int highestWave;			//Holds the player's highest wave
	
	//Pages
	public GameObject menuPage;		//The page which holds all the main menu elements
	public GameObject playPage;		//The page which holds all the play elements
	public GameObject optionsPage;	//The page which holds all the options elements
	
	//UI
	public Text highscoreTxt;		//The Text component that displays the player's highscore and highest wave
	public Slider volumeSlider;		//The Slider which defines the volume
	
	void Start ()
	{
		highscore = PlayerPrefs.GetInt("Highscore");
		highestWave = PlayerPrefs.GetInt("Highest Wave");
		PlayerPrefs.SetFloat("Volume", 1.0f);
	}
	
	void Update ()
	{
		//UI
		highscoreTxt.text = "Highscore: " + highscore + "\nHighest Wave: " + highestWave;
	}
	
	public void SetPage (string page)
	{
		if(page == "menu"){
			menuPage.active = true;
			playPage.active = false;
			optionsPage.active = false;
		}
		if(page == "play"){
			menuPage.active = false;
			playPage.active = true;
			optionsPage.active = false;
		}
		if(page == "options"){
			menuPage.active = false;
			playPage.active = false;
			optionsPage.active = true;
		}
	}
	
	public void PlayMap (string map)
	{
		if(map == "grass"){
			Application.LoadLevel(1);
		}
		if(map == "road"){
			Application.LoadLevel(2);
		}
		
		PlayerPrefs.SetFloat("Volume", volumeSlider.value);
	}

	public void DeleteHighscore ()
	{
		PlayerPrefs.SetInt("Highscore", 0);
		PlayerPrefs.SetInt("Highest Wave", 0);
		
		highscore = 0;
		highestWave = 0;
	}
	
	public void QuitGame ()
	{
		Application.Quit();
	}
}
