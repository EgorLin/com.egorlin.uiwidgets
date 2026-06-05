using EgorLin.UIWidgets.Components.Base;
using EgorLin.UIWidgets.Components.Basic.Base;

namespace EgorLin.UIWidgets.Components.Basic.ButtonModules {

    public class InteractableContainersComponentModule : ButtonComponentModule {

        public WindowComponent normalContainer;
        public WindowComponent disabledContainer;

        public override void OnInteractableChanged(bool state) {
            
            base.OnInteractableChanged(state);

            this.normalContainer?.ShowHide(state == true);
            this.disabledContainer?.ShowHide(state == false);
            
        }

    }

}
