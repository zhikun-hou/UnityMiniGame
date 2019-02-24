using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : PickableProp {


	public Sprite controllerUseable_on;
	public Sprite controllerUseable_off;

	public override void OnItemAction(ItemGrid grid,EventArgs e){
		if(ItemPack.GetPack().isOn("电池")){
			ItemPack.GetPack().TakeAway("电池");
			ItemPack.GetPack().TakeAway("没电的遥控器");
		
			PickableProp controllerUseable = new PickableProp();
			controllerUseable.name = "有电的遥控器";
			controllerUseable.item_desc = "电视遥控器可不能被弟弟发现了。";
			controllerUseable.icon_off = this.controllerUseable_off;
			controllerUseable.icon_on = this.controllerUseable_on;

			ItemPack.GetPack().PutIn(controllerUseable);
		}
	}

}
