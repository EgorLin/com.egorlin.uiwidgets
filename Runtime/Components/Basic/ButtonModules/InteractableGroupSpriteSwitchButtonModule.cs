using EgorLin.UIWidgets.Components.Basic.Base;
using EgorLin.UIWidgets.Modules;
using UnityEngine;

namespace EgorLin.UIWidgets.Components.Basic.ButtonModules {

    public class InteractableGroupSpriteSwitchButtonModule : ButtonComponentModule {
        
        [System.SerializableAttribute]
        public struct Item {
            
            public ImageComponent source;
            [ResourceType(typeof(Sprite))]
            public Resource disabled;
            [ResourceType(typeof(Sprite))]
            public Resource normal;
            
        }
        
        public Item[] groupItems;

        public override void OnInteractableChanged(bool state) {

            base.OnInteractableChanged(state);

            foreach (var item in this.groupItems) {
                item.source.SetImage(state == true ? item.normal : item.disabled);
            }

        }

    }

}
