using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cupboard : ControllerProp {

	public Sprite spriteOn;
	public Sprite spriteOff;

	public override void Action_On(){
		this.GetComponent<Image>().sprite = this.spriteOn;
		
		int children = this.transform.childCount;
		for(int i=0;i<children;i++){
			this.transform.GetChild(i).gameObject.SetActive(true);
		} 
	}

	public override void Action_Off(){
		this.GetComponent<Image>().sprite = this.spriteOff;

		int children = this.transform.childCount;
		for(int i=0;i<children;i++){
			this.transform.GetChild(i).gameObject.SetActive(false);
		} 
	}

	public override IEnumerator ShowDescription(){
		if(this.state && !Scene.GetScene().Contains("没电的遥控器")){
			this.desc_on = "";
			this.description = "";
		}
		yield return StartCoroutine(base.ShowDescription());
	}

}
