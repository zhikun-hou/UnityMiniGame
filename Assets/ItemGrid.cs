using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemGrid : EventTrigger {

    private Item _item = null;

    private Image _icon;
    private Sprite _icon_on;
    private Sprite _icon_off;

    private static AudioClip sound;
    
    private AudioSource soundPlayer;

    public Text _desc;

    public bool isEmpty {
        get { return this._item==null; }
    }

    public bool isOn{
        get { return this._item.active; }
    }

    public delegate void Action(ItemGrid sender,EventArgs e);
    public event Action OnAction;
    
    void Awake(){
        this._icon = this.transform.Find("Icon").GetComponent<Image>();
        this.soundPlayer = this.GetComponent<AudioSource>();
        ItemGrid.sound = Resources.Load<AudioClip>("ItemGrid");
    }

    public override void OnPointerClick(PointerEventData eventData){
        if(this._item==null) return;
        this.soundPlayer.clip = ItemGrid.sound;
        this.soundPlayer.Play();
        if(this._item.active){
            this.Off();
        } else {
            this.On();
        }
        if(this.OnAction!=null) OnAction(this,new EventArgs());
    }

    private void On(){
        Debug.Log("道具"+this._item.name+" ON!");
        this._icon.sprite = this._icon_on;
        this._item.SetActive(true);
        StartCoroutine(HUD.GetHUD().ReadDescription(this._item.description));
    }

    private void Off(){
        Debug.Log("道具"+this._item.name+" OFF!");
        this._icon.sprite = this._icon_off;
        this._item.SetActive(false);
        StartCoroutine(HUD.GetHUD().ReadDescription(this._item.description));
    }

    public ItemGrid PutIn(PickableProp prop){
        if(!this.isEmpty) Debug.LogError("道具格子无空位！");
        this._item = prop.ToItem();
        this._icon_on = prop.icon_on;
        this._icon_off = prop.icon_off;
        this._icon.sprite = this._icon_off;
        this._icon.enabled = true;
        return this;
    }

    

    public Item TakeAway(){
        Item item = this._item;
        this._item = null;
        this._icon.sprite = null;
        this._icon.enabled = false;
        this.OnAction = null;
        return item;
    }

    public bool Contains(string name){
        return this._item.name == name;
    }

    public Item GetItem(){
        return this._item;
    }

    //根据active内发光

}
