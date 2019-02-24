using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : ControllerProp {

	public Light light;
	public override void Action_Off(){
		light.LightOff();
		Scene.GetScene().LightOff();
	}

	public override void Action_On(){
		light.LightOn();
	}

	public override void BeforeShowDescription(){
		if(Scene.GetScene().Contains("带灯罩的灯")){
			this._showDescription = false;
		} else if(this.state) {
			Scene.GetScene().LightOn();
		}
		
	}

}
