using UnityEngine;

public class ItemPack : MonoBehaviour {
    
    private static ItemPack _instance;
    private ItemGrid[] _grids;

    private CanvasGroup group;

    void Awake(){
        this.group = this.GetComponent<CanvasGroup>();
    }

    public void Start(){
        ItemPack._instance = this;
        this._grids = this.transform.GetComponentsInChildren<ItemGrid>();
    }

    public static ItemPack GetPack(){
        return ItemPack._instance;
    }

    public ItemGrid PutIn(PickableProp prop){
        for(int i=0;i<this._grids.Length;i++){
			if(this._grids[i].isEmpty){
                return this._grids[i].PutIn(prop);
			}
		}
        Debug.LogError("道具栏溢出");
        return null;
    }



    public Item TakeAway(string itemName){
        ItemGrid grid = this.Find(itemName);
        if(grid) return grid.TakeAway();
        return null;
    }

    private int Has(string itemName){
        for(int i=0;i<this._grids.Length;i++){
			if(this._grids[i].isEmpty) continue;
            if(this._grids[i].Contains(itemName)) return i;
		}
        return -1;
    }

    public ItemGrid Find(string itemName){
        for(int i=0;i<this._grids.Length;i++){
			if(this._grids[i].isEmpty) continue;
            if(this._grids[i].Contains(itemName)) return this._grids[i];
		}
        return null;
    }

    public bool Contains(string itemName){
        return this.Has(itemName)>-1;
    }

    public bool isOn(string itemName){
        int i = this.Has(itemName);
        if(i==-1) return false;
        return this._grids[i].isOn;
    }

    public void BlockOn(){
        this.group.blocksRaycasts = false;
    }

    public void BlockOff(){
        this.group.blocksRaycasts = true;
    }


}