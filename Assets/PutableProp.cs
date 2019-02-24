using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutableProp : Prop {

	public string[] _putableItem;
	public Vector3[] _putPos;
	public string[] _targetProp;

	public override IEnumerator Action(){
		this.TryPut();
		yield return null;
	}

	public void TryPut(){
		ItemPack pack = ItemPack.GetPack();
		
		for(int i=0;i<this._putableItem.Length;i++){
			string item = this._putableItem[i];
			if(!pack.isOn(item)) continue;
			pack.TakeAway(item);
			this.Put(this._targetProp[i],this._putPos[i]);
			break;
		}
	}

	public void Put(string name,Vector3 pos){
		GameObject prefab = Resources.Load<GameObject>("Corner/"+name);
		RectTransform prop = GameObject.Instantiate(prefab,this.transform).GetComponent<RectTransform>();
		prop.anchoredPosition3D = pos;
	}


}
