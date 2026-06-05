using EgorLin.UIWidgets.Components.Basic.Base;
using EgorLin.UIWidgets.Modules;
using UnityEngine;

namespace EgorLin.UIWidgets.Components.Basic.ImageModules {
    public class ResourceSpriteLoadModule : ImageComponentModule {

        [ResourceType(typeof(Sprite))]
        public Resource resource;
        
        public override void OnInit() {
            
            base.OnInit();
            
            this.imageComponent.SetImage(this.resource);
            
        }

    }
    
}
