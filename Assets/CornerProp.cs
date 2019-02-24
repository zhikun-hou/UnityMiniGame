using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerProp : Prop {

	public string cornerName;

	public override IEnumerator Action(){
		this.EnterCorner();
		yield return null;
	}

	private void EnterCorner(){
		Corner.GetCorner().Enter(cornerName);
	}

}
