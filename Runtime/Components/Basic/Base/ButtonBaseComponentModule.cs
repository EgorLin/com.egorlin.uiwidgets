using EgorLin.UIWidgets.Components.Base;

namespace EgorLin.UIWidgets.Components.Basic.Base {

    public abstract class ButtonComponentModule : WindowComponentModule {

        public ButtonComponent buttonComponent;

        public override void ValidateEditor() {
            
            base.ValidateEditor();
            
            this.buttonComponent = this.windowComponent as ButtonComponent;
            
        }

    }
    
}
