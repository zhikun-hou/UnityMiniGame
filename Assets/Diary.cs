using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diary : Prop {


	public override void BeforeShowDescription(){
		this._showDescription = false;
		HUD hud = HUD.GetHUD();
		hud.BrotherSpeak("难过","姐,你竟然偷看我的日记本QAQ");
		hud.Lose();
	}

}
