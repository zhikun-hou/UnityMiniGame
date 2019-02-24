using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerProp : Prop {

	public bool state;

	public string desc_on = null;
	public string desc_off = null;



	public override IEnumerator Action(){
		if(state){
			this.state = false;
			this.Action_Off();
			this.description = this.desc_off;
		} else {
			this.state = true;
			this.Action_On();
			this.description = this.desc_on;
		}
		yield return null;
	}

	public virtual void Action_On(){

	}

	public virtual void Action_Off(){

	}

	


}
