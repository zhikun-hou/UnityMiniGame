using System;
using System.Collections;
using System.Xml;
using UnityEngine;


public class ScriptReader {
        
    private XmlReader _reader;

    private bool blocking = false;
    private bool skip = false;

    public ScriptReader(string scriptName){
        XmlReaderSettings setting = new XmlReaderSettings();
		setting.IgnoreComments = true;//忽略注释
		setting.ProhibitDtd = false;//启用DTD验证
		setting.ValidationType = ValidationType.DTD;//使用DTD而非Schema验证


        this._reader = XmlReader.Create(Application.streamingAssetsPath +"/Script/"+scriptName+".xml",setting);
    }


    public IEnumerator Start(){
        HUD hud = HUD.GetHUD();
        while(this._reader.Read()){
            if(this._reader.NodeType != XmlNodeType.Element) continue;
            if(this._reader.Name!="talk") continue;
            
            string name = this._reader.GetAttribute("name");
            string emotion = this._reader.GetAttribute("emotion");
            string words = this._reader.ReadInnerXml();
			switch(name){
				case "姐姐":
                    hud.SisterSpeak(emotion,words);
					break;
				case "弟弟":
                    hud.BrotherSpeak(emotion,words);
					break;
				default:
                    hud.AsideSpeak(words);
                    break;
			}
            if(!this.skip) this.blocking = true;
            yield return new WaitUntil(()=>{
                return !this.blocking;
            });
        }
        hud.EndSpeak();
    }

    public void Next(){
        this.blocking = false;
    }

    public void Skip(){
        this.blocking = false;
        this.skip = true;
    }


    
}