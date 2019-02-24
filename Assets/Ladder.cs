using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : PickableProp {


	public override IEnumerator Action(){
		if(!Scene.GetScene().Contains("花")){
			this.description = "";
			this.Pick();			
		} 
		yield return null;
	}

	public override void SoundOver(){
		if(!Scene.GetScene().Contains("花")){
			Destroy(this.gameObject);
		} 
	}


}
