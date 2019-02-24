using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashProp : Prop {


	public override IEnumerator Action(){
		Image image = this.GetComponent<Image>();
		if(image) image.enabled = false;
		yield return null;
	}

	public override void SoundOver(){
		Destroy(this.gameObject);
	}

}
