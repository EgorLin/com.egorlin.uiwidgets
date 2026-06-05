using EgorLin.UIWidgets.Components.Basic.Base;
using UnityEngine;

namespace EgorLin.UIWidgets.Components.Basic.ListModules {

    internal class ListRectTransformChangedInternal : MonoBehaviour {

        public ListBaseComponent listBaseComponent;

        public void OnRectTransformDimensionsChange() {
            
            if (this.listBaseComponent != null) this.listBaseComponent.ForceLayoutChange();
            
        }

    }

}
