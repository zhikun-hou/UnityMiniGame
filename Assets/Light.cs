using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Light : ControllerProp {

public Sprite LightWithShade_on;
public Sprite LightWithShde_off;

public Image circle;
public Image ray;

public override IEnumerator Action(){
		if(this.name=="带灯罩的灯") yield break;
		if(!Scene.GetScene().Contains("花架")){
			this.description = "灯好高啊...";
		} else if(this.state){
			this.description = "通电作业不太安全。";
		} else {
			this.description = "这下就可以安装灯罩了。";
			if(ItemPack.GetPack().isOn("灯罩")){
				ItemPack.GetPack().TakeAway("灯罩");
				this.lightOff = this.LightWithShde_off;
				this.lightOn = this.LightWithShade_on;
				this.GetComponent<Image>().sprite = this.lightOff;
				this.name = "带灯罩的灯";
				this.description = "完成了！我需要来试下效果。";
			}
		}

		yield return null;
	}


	public void LightOn(){
		this.state = true;
		if(this.name=="带灯罩的灯"){
			HUD hud = HUD.GetHUD();
			hud.BrotherSpeak("高兴","真是太棒了，这个魔法阵真漂亮。");
			hud.Win();
			StartCoroutine(ShowEffect());
		}
	}

	private IEnumerator ShowEffect(){
		circle.gameObject.SetActive(true);
		ray.gameObject.SetActive(true);
		for(int i=0;i<=100;i+=10){
			circle.color = new Color(1,1,1,i/100f);
			ray.color = new Color(1,1,1,i/200f);
			yield return new WaitForSeconds(0.04f);
		}
	}

	public void LightOff(){
		this.state = false;
	}




}
