using EgorLin.UIWidgets.Components.Base;

namespace EgorLin.UIWidgets.Components.Basic.Base {

    public abstract class DropdownComponentModule : WindowComponentModule {

        public DropdownComponent dropdownComponent;

        public override void ValidateEditor() {
            
            base.ValidateEditor();
            
            this.dropdownComponent = this.windowComponent as DropdownComponent;
            
        }

    }
    
}
