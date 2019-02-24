using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Corner : MonoBehaviour {

	private CanvasGroup group;
	private Image mask;

	private static Corner _instance = null;

	private GameObject current = null;
	private static Transform list;

	private 

	void Awake(){
		this.group = this.GetComponent<CanvasGroup>();
		this.mask = this.transform.Find("Mask").GetComponent<Image>();
		Corner._instance = this;
		Corner.list = this.transform.Find("List");
	}

	public static Corner GetCorner(){
		return Corner._instance;
	}

	public void Exit(){
		if(!this.group.blocksRaycasts) return;
		if(!this.current) return;
		StartCoroutine(HideCorner());
	}

	public void Enter(string name){
		Transform corner = Corner.list.Find(name);
		if(corner==null){
			Debug.LogError("Invalid corner name.");
			return;
		}
		this.current = corner.gameObject;
		this.current.SetActive(true);
		StartCoroutine(ShowCorner());
	}

	private IEnumerator ShowCorner(){
		Menu.GetMenu().BlockOn();
		for(int i=0;i<=100;i+=10){
			this.group.alpha = i/100f;
			this.mask.color = new Color(0,0,0,i/200f);
			yield return new WaitForSeconds(0.02f);
		}
		this.group.blocksRaycasts = true;
	}

	private IEnumerator HideCorner(){
		this.group.blocksRaycasts = false;
		for(int i=100;i>=0;i-=10){
			this.group.alpha = i/100f;
			this.mask.color = new Color(0,0,0,i/200f);
			yield return new WaitForSeconds(0.02f);
		}
		this.current.SetActive(false);
		this.current = null;
		Menu.GetMenu().BlockOff();
	}


	public void BlockOn(){
		this.group.blocksRaycasts = false;
	}

	public void BlockOff(){
		this.group.blocksRaycasts = true;
	}

}
