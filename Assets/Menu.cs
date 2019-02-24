using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

 	private static Menu _instance = null;
    private RectTransform menu_panel;
    private RectTransform about_panel;

	private Button menu_btn;

    private CanvasGroup group;

    private bool sound = true;
    private AudioSource musicPlayer;

	void Awake(){
        Menu._instance = this;
		this.menu_panel = this.transform.Find("MenuPanel").gameObject.GetComponent<RectTransform>();
		this.about_panel = this.transform.Find("AboutPanel").gameObject.GetComponent<RectTransform>();
		this.menu_btn = this.transform.Find("MenuButton").gameObject.GetComponent<Button>();
        this.group = this.menu_panel.GetComponent<CanvasGroup>();
        GameObject player = GameObject.Find("MusicPlayer");
        if(player) this.musicPlayer = player.GetComponent<AudioSource>();
    }

    public static Menu GetMenu(){
        return Menu._instance;
    }

	public void Show(){
        StartCoroutine(ShowMenu());
	}

	public void Hide(){
        StartCoroutine(HideMenu());
	}

    private IEnumerator ShowMenu(){
		this.menu_btn.interactable = false;
        Scene.GetScene().BlockOn();
        for(int i=0;i<=100;i+=10){
            this.group.alpha = i/100f;
            yield return new WaitForSeconds(0.02f);
        }
        this.group.blocksRaycasts = true;
    }

    private IEnumerator HideMenu(){
        this.group.blocksRaycasts = false;
        for(int i=100;i>=0;i-=10){
            this.group.alpha = i/100f;
            yield return new WaitForSeconds(0.02f);
        }
        Scene.GetScene().BlockOff();
		this.menu_btn.interactable = true;
    }

    public void MenuOpenAbout(){
        this.menu_panel.gameObject.SetActive(false);
        this.about_panel.gameObject.SetActive(true);
    }

    public void MenuCloseAbout(){
        this.about_panel.gameObject.SetActive(false);
        this.menu_panel.gameObject.SetActive(true);
    }

    public void BlockOn(){
        this.menu_btn.interactable = false;
    }

    public void BlockOff(){
        this.menu_btn.interactable = true;
    }

    public void MenuTitle(){
		SceneManager.LoadScene(0);
	}

	public void MenuBack(){
		this.Hide();
	}

    public void SoundSwitch(Image icon){
        sound = !sound;
        if(sound){
            if(this.musicPlayer) this.musicPlayer.Play();
            icon.sprite = Resources.Load<Sprite>("Frame/SoundButtonOn");
        } else {
            if(this.musicPlayer) this.musicPlayer.Stop();
            icon.sprite = Resources.Load<Sprite>("Frame/SoundButtonOff");
        }
    }

    public void ButtonPress(Button btn){
        RectTransform icon = btn.GetComponent<RectTransform>();
        icon.sizeDelta = new Vector2(icon.sizeDelta.x*0.8f,icon.sizeDelta.y*0.8f);
    }

    public void ButtonUp(Button btn){
        RectTransform icon = btn.GetComponent<RectTransform>();
        icon.sizeDelta = new Vector2(icon.sizeDelta.x*1.25f,icon.sizeDelta.y*1.25f);
    }
}
