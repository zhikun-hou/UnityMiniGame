using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Television : Prop {

	public GameObject screen;

	public override IEnumerator Action(){
		ItemPack pack = ItemPack.GetPack();
		if(pack.isOn("有电的遥控器")){
			pack.TakeAway("有电的遥控器");
			screen.SetActive(true);
		}
		yield return null;
	}
}
