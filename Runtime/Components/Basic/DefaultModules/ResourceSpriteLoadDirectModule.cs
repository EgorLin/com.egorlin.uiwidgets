using EgorLin.UIWidgets.Components.Base;
using EgorLin.UIWidgets.Modules;
using UnityEngine;
using UnityEngine.UI;

namespace EgorLin.UIWidgets.Components.Basic.DefaultModules {

    public class ResourceSpriteLoadDirectModule : WindowComponentModule {

        public ResourceRef<Sprite> resourceRef;
        public Image image;
        
        public override void OnInit() {
            
            base.OnInit();
            
            this.image.sprite = this.resourceRef.Load(this);
            
        }

        public override void OnDeInit() {
            
            this.resourceRef.Unload(this);
            
            base.OnDeInit();
            
        }

    }
    
}
