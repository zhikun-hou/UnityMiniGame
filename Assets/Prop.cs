using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//场景中的道具
public class Prop : MonoBehaviour {

	public string name;
    public string description;

	public string item_desc;

	public Sprite lightOn;
	public Sprite lightOff;
	private Image render;

	private static AudioClip sound;
	private AudioSource soundPlayer;
	private bool playing = false;

	protected bool _showDescription = true;

	public void Awake(){
		this.soundPlayer = this.GetComponent<AudioSource>();
	    Prop.sound = Resources.Load<AudioClip>("PropAction");
		this.render = this.GetComponent<Image>();
	}

	public Prop(){}
	public Prop(string name,string description){
		this.name = name;
		this.description = description;
	}

	public void LightOn(){
		if(this.lightOn==null) return;
		this.render.sprite = this.lightOn;
	}

	public void LightOff(){
		if(this.lightOff==null) return;
		this.render.sprite = this.lightOff;
	}

    public virtual void OnPointerClick(){
        Debug.Log("PointerClick:"+name);
		StartCoroutine(this.PlaySound());
		StartCoroutine(this.Action());
		StartCoroutine(this.ShowDescription());
    }

	public virtual void OnPointerEnter(){
		Debug.Log("PointerEnter:"+name);
	}
	public virtual void OnPointerExit(){
		Debug.Log("PointerExit:"+name);
	}
	public virtual void OnPointerPress(){
		Debug.Log("PointerPress:"+name);
	}
	public virtual void OnPointerUp(){
		Debug.Log("PointerUp:"+name);
	}

	public IEnumerator PlaySound(){
		this.soundPlayer.clip = Prop.sound;
		this.soundPlayer.Play();

		while(this.soundPlayer.isPlaying){
			yield return null;
		}
		this.SoundOver();
	}

	public virtual void SoundOver(){

	}

	public virtual IEnumerator Action(){
		yield return null;
	}

	public virtual IEnumerator OnDescribOver(){
		yield return null;
	}

	public Item ToItem(){
		return new Item(this.name,this.item_desc);
	}


	public override string ToString(){
        return this.name;
    }

	public virtual IEnumerator ShowDescription(){
		this.BeforeShowDescription();
		if(!this._showDescription) yield break;
		if(this.description!=null){
			yield return StartCoroutine(HUD.GetHUD().ReadDescription(this.description));
		}
		yield return StartCoroutine(this.OnDescribOver());
	}


	public virtual void BeforeShowDescription(){

	}

}
