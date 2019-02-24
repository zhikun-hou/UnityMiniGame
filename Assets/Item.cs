

//道具栏中的道具
public class Item {

	private bool _active = false;	public bool active {
		get { return this._active; }
	}

	public readonly string name;
	public readonly string description;

	private Item(){}
	public Item(string name,string description){
		this.name = name;
		this.description = description;
	}

    public void SetActive(bool state){
		this._active = state;
	}

	public Prop ToProp(){
		return new Prop(this.name,this.description);
	}

}
