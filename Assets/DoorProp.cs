using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//门道具，用于切换场景
public class DoorProp : Prop {

    public string sceneName;

    public override IEnumerator Action(){
        Scene.GetScene().Change(sceneName);
        yield return null;
    }
		


}
