using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour {

	private static Scene _instance;

	private static Transform list;
	private GameObject current;

	private CanvasGroup group;

	public GameObject initScene;

	private ScriptReader reader;

	void Awake(){
		Scene._instance = this;
		Scene.list = this.transform.Find("List");
		this.reader =  new ScriptReader("script");
		this.group = Scene.list.GetComponent<CanvasGroup>();
		int num = Scene.list.transform.childCount;
		for(int i=0;i<num;i++){
			GameObject temp = Scene.list.transform.GetChild(i).gameObject;
			if(temp.activeSelf){
				this.current = temp;
				break;
			}
		}
	}

	void Start () {
		StartCoroutine(test());
	}

	public IEnumerator test(){
		HUD.GetHUD().ShowSkip();
		Menu.GetMenu().BlockOn();
		yield return StartCoroutine(Enter(this.initScene));
		this.BlockOn();
		yield return StartCoroutine(this.reader.Start());
		this.BlockOff();
		Menu.GetMenu().BlockOff();
		HUD.GetHUD().HideSkip();
	}

	public void Next(){
		this.reader.Next();
	}
	
	public void Skip(){
		this.reader.Skip();
	}

	public void LightOn(){
		GameObject[] list = GameObject.FindGameObjectsWithTag("Prop");
		foreach(GameObject obj in list){
			Prop prop = obj.GetComponent<Prop>();
			prop.LightOn();
		}
	}
	public void LightOff(){
		GameObject[] list = GameObject.FindGameObjectsWithTag("Prop");
		
		foreach(GameObject obj in list){
			Prop prop = obj.GetComponent<Prop>();
			prop.LightOff();
		}
	}

	public static Scene GetScene(){
		return Scene._instance;
	}

	public bool Contains(string name){
		GameObject[] list = GameObject.FindGameObjectsWithTag("Prop");
		foreach(GameObject obj in list){
			Prop prop = obj.GetComponent<Prop>();
			if(prop==null) continue;
			if(prop.name==name) return true;
		}
		return false;
	}

	public bool Contains(string name,bool state){
		GameObject[] list = GameObject.FindGameObjectsWithTag("Prop");
		foreach(GameObject obj in list){
			Prop prop = obj.GetComponent<Prop>();
			if(prop.name!=name) continue;
			ControllerProp controller = obj.GetComponent<ControllerProp>();
			if(controller==null) continue;
			if(controller.state==state) return true;
		}
		return false;
	}

	public void PutIn(string name,Vector3 pos){
		GameObject prefab = (GameObject)Resources.Load("Prop/"+name);
		if(!prefab){
			Debug.LogError("道具"+name+"并不存在！");
			return;
		}
		GameObject.Instantiate(prefab,pos,this.transform.rotation);
	}

	public void Remove(string name){
		GameObject[] list = GameObject.FindGameObjectsWithTag("Prop");
		foreach(GameObject obj in list){
			Prop prop = obj.GetComponent<Prop>();
			if(prop.name!=name) continue;
			Destroy(prop.gameObject);
		}
	}

	public void Change(string name){
		Transform scene = Scene.list.Find(name);
		if(scene==null){
			Debug.LogError("Invalid scene name.");
			return;
		}
		StartCoroutine(ChangeScene(scene.gameObject));
	}

	public void BlockOn(){
		this.group.blocksRaycasts = false;
	}

	public void BlockOff(){
		this.group.blocksRaycasts = true;
	}

	private IEnumerator Exit(){
		this.group.blocksRaycasts = false;
		for(int i=100;i>=0;i-=10){
			group.alpha = i/100f;
			yield return new WaitForSeconds(0.04f);
		}
		
		this.current.SetActive(false);
	}

	private IEnumerator Enter(GameObject scene){
		this.current = scene;
		this.current.SetActive(true);
		yield return new WaitForSeconds(0.5f);

		for(int i=0;i<=100;i+=10){
			group.alpha = i/100f;
			yield return new WaitForSeconds(0.04f);
		}
		this.group.blocksRaycasts = true;
	}

	private IEnumerator ChangeScene(GameObject scene){
		yield return StartCoroutine(Exit());
		yield return StartCoroutine(Enter(scene));
		
	} 

}
