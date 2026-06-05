using EgorLin.UIWidgets.Components.Base;
using EgorLin.UIWidgets.Modules;
using UnityEngine;
using UnityEngine.UI;

namespace EgorLin.UIWidgets.Components.Basic.DefaultModules {

    public class ResourceTextureLoadDirectModule : WindowComponentModule {

        public ResourceRef<Texture> resourceRef;
        public RawImage image;
        
        public override void OnInit() {
            
            base.OnInit();
            
            this.image.texture = this.resourceRef.Load(this);
            
        }

        public override void OnDeInit() {
            
            this.resourceRef.Unload(this);
            
            base.OnDeInit();
            
        }

    }
    
}
