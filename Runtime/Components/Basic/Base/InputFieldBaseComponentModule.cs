using EgorLin.UIWidgets.Components.Base;

namespace EgorLin.UIWidgets.Components.Basic.Base {

    public abstract class InputFieldComponentModule : WindowComponentModule {

        public InputFieldComponent inputFieldComponent;

        public override void ValidateEditor() {
            
            base.ValidateEditor();
            
            this.inputFieldComponent = this.windowComponent as InputFieldComponent;
            
        }

    }
    
}
