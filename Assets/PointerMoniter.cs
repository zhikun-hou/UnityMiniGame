using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

    public class PointerMoniter : MonoBehaviour {

        private static PointerMoniter _moniter;

        private Prop _hover = null;
        private Prop _focus = null;
        private bool _enable = true;

        void Awake(){
            PointerMoniter._moniter = this;
        }

        void Update(){
            if(this._enable == false) return;
            PointerPositionMoniter();
            PointerPressMoniter();
            PointerUpMoniter();
        }

        public static PointerMoniter GetMoniter(){
            return PointerMoniter._moniter;
        }

        public void Enable(){
            this._enable = true;
        }

        public void Disable(){
            this._enable = false;
        }

        private RaycastHit2D[] GetRaycastHits(){
            Camera camera = Camera.main;
            Vector3 pointer = Input.mousePosition;
            Ray ray = camera.ScreenPointToRay(pointer);
            return Physics2D.RaycastAll(ray.origin,ray.direction);
        }

        private Prop GetTopProp(RaycastHit2D[] hits){
            Prop topProp = null;
            for(int i=0;i<hits.Length;i++){
                RaycastHit2D hit = hits[i];
                if(hit.collider.tag != "Prop") continue; //当前Collider并非道具
                topProp = hit.collider.GetComponent<Prop>();
            }
            return topProp;
        }

        public void PointerPositionMoniter(){
            RaycastHit2D[] hits = this.GetRaycastHits();
            Prop topProp = this.GetTopProp(hits);
            if(topProp != this._hover){
                if(this._hover != null) this._hover.OnPointerExit();
                if(topProp != null) topProp.OnPointerEnter();
                this._hover = topProp;
            }
        }

        public void PointerPressMoniter(){
            if(Input.GetMouseButtonDown(0)){
                if(this._hover == null) return;
                this._focus = this._hover;
                this._focus.OnPointerPress();
            }
        }

        public void PointerUpMoniter(){
            if(Input.GetMouseButtonUp(0)){
                if(this._hover == null){
                    this._focus = null;
                    return;
                }
                if(this._hover == this._focus){
                    this._focus.OnPointerClick();
                }
                this._hover.OnPointerUp();
                this._focus = null;
            }
        }


    }