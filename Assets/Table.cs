using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : PutableProp {


	public Vector3 _shadePos;


	public override IEnumerator Action(){
		yield return base.Action();
		Scene scene = Scene.GetScene();
		if(scene.Contains("铅笔")&&scene.Contains("小刀")&&scene.Contains("纸")&&scene.Contains("花纹")){
			scene.Remove("铅笔");
			scene.Remove("小刀");
			scene.Remove("纸");
			this.Put("灯罩",this._shadePos);

		}
	}

	
}
