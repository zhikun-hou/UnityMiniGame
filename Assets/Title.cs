using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject musicPlayer = GameObject.Find("MusicPlayer");
		DontDestroyOnLoad(musicPlayer);
	}
	


	public void Exit(){
		Application.Quit();
	}

	public void Go(){
		SceneManager.LoadScene(1);
	}

}
