using EgorLin.UIWidgets.Components.Basic.Base;
using UnityEngine;

namespace EgorLin.UIWidgets.Components.Basic.ButtonModules {

    public class InteractableGroupMaterialSwitchButtonModule : ButtonComponentModule {

        [System.SerializableAttribute]
        public struct Item {

            public ImageComponent source;
            public Material disabled;
            public Material normal;

        }

        public Item[] groupItems;

        public override void OnInteractableChanged(bool state) {

            base.OnInteractableChanged(state);

            foreach (var item in this.groupItems) {
                item.source.SetMaterial(state == true ? item.normal : item.disabled);
            }

        }

    }

}
