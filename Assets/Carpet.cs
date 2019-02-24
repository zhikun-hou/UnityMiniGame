using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carpet : PickableProp {

	public override void OnItemAction(ItemGrid grid,EventArgs e){

		if(ItemPack.GetPack().isOn("铅笔")){
			HUD hud = HUD.GetHUD();
			hud.BrotherSpeak("难过","用地毯画魔法阵，你不怕挨揍吗O_O");
			hud.Lose();
		}
	}

}
