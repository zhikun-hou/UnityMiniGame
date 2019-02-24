using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
        
    private static HUD _instance = null;
    private static GameObject canvas;

    //用于防止描述显示到一半就再次点击，导致出现文本错乱的bug
    private int _reading = 0;

    private Text desc_text;
    public RectTransform desc_panel;
    public RectTransform pack_panel;
    public Image brother_avatar;
    public Image sister_avatar;

    public Button skip;
    
    public Image win;
    public Image lose;

    public Button back;

    public void Awake(){
        HUD._instance = this;
        HUD.canvas = this.gameObject;
        this.desc_text = this.desc_panel.Find("Panel/Text").GetComponent<Text>();
    }

    public static HUD GetHUD(){
        return HUD._instance;
    }


    public IEnumerator ReadDescription(string desc){
        this._reading++;
        int reading = this._reading;
        
        this.desc_text.text = null;
        for(int i=0;i<desc.Length;i++){
            if(reading != this._reading) yield break;
            this.desc_text.text += desc[i];
            yield return new WaitForSeconds(0.06f);
        }
    }

    public void ShowSkip(){
        this.skip.gameObject.SetActive(true);
    }

    public void HideSkip(){
        this.skip.gameObject.SetActive(false);
    }

    public void SisterSpeak(string emotion,string words){
        this.desc_text.color = new Color32(145,18,18,255);
        StartCoroutine(ReadDescription("姐姐："+words));
        this.sister_avatar.sprite = Resources.Load<Sprite>("Avatar/姐姐"+emotion);
        if(!this.sister_avatar.gameObject.activeSelf){
            StartCoroutine(ShowAvatar(this.sister_avatar,-1));
        }
    }

    public void BrotherSpeak(string emotion,string words){
        this.desc_text.color = new Color32(15,24,95,255);
        StartCoroutine(ReadDescription("弟弟："+words));
        this.brother_avatar.sprite = Resources.Load<Sprite>("Avatar/弟弟"+emotion);
        if(!this.brother_avatar.gameObject.activeSelf){
            StartCoroutine(ShowAvatar(this.brother_avatar,1));
        }
    }

    public void AsideSpeak(string words){
        this.desc_text.color = new Color32(106,59,44,255);
        StartCoroutine(ReadDescription(words));
    }

    public void EndSpeak(){
        StartCoroutine(this.HideAvatar());
        this.AsideSpeak("");
    }

    private IEnumerator ShowAvatar(Image avatar,int sign){
        avatar.gameObject.SetActive(true);
        for(int i=0;i<=100;i+=10){
            avatar.color = new Color(1,1,1,i/100f);
            avatar.GetComponent<RectTransform>().anchoredPosition = new Vector2(sign*i/2f,0);
            yield return new WaitForSeconds(0.02f);
        }
    }

    private IEnumerator HideAvatar(){
        for(int i=100;i>=0;i-=10){
            this.sister_avatar.color = new Color(1,1,1,i/100f);
            this.brother_avatar.color = new Color(1,1,1,i/100f);
            this.brother_avatar.GetComponent<RectTransform>().anchoredPosition = new Vector2(i/2f,0);
            this.sister_avatar.GetComponent<RectTransform>().anchoredPosition = new Vector2(-i/2f,0);
            yield return new WaitForSeconds(0.02f);
        }
        this.sister_avatar.gameObject.SetActive(false);
        this.brother_avatar.gameObject.SetActive(false);
    }

    private IEnumerator ShowLose(){
        back.gameObject.SetActive(true);
        lose.gameObject.SetActive(true);
        for(int i=0;i<=100;i+=10){
            lose.color = new Color(1,1,1,i/100f);
            back.targetGraphic.color = new Color(1,1,1,i/100f);
            yield return new WaitForSeconds(0.04f);
        }
        back.interactable = true;
    }

    public void Lose(){
        Scene.GetScene().BlockOn();
        Corner.GetCorner().BlockOn();
        Menu.GetMenu().BlockOn();
        ItemPack.GetPack().BlockOn();
        StartCoroutine(ShowLose());
    }

    public void Win(){
        Scene.GetScene().BlockOn();
        Corner.GetCorner().BlockOn();
        Menu.GetMenu().BlockOn();
        ItemPack.GetPack().BlockOn();
        StartCoroutine(ShowWin());
    }

    private IEnumerator ShowWin(){
        back.gameObject.SetActive(true);
        win.gameObject.SetActive(true);
        for(int i=0;i<=100;i+=10){
            win.color = new Color(1,1,1,i/100f);
            back.targetGraphic.color = new Color(1,1,1,i/100f);
            yield return new WaitForSeconds(0.04f);
        }
        back.interactable = true;
    }




}