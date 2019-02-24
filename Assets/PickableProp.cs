using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickableProp : Prop {

	public Sprite icon_on;
	public Sprite icon_off;




	public override IEnumerator Action(){
		this.Pick();
		yield return null;
	}

	protected void Pick(){
		Debug.Log("正在拾取道具："+this.name);
		ItemGrid grid = ItemPack.GetPack().PutIn(this);
		grid.OnAction += this.OnItemAction;
		Image image = this.GetComponent<Image>();
		if(image) image.enabled = false;
	}

	public override void SoundOver(){
		Destroy(this.gameObject);
	}
	
	public virtual void OnItemAction(ItemGrid grid,EventArgs args){
		Debug.Log("正在使用道具格子："+grid.name);
	}


}
