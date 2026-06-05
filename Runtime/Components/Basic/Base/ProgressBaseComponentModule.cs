using EgorLin.UIWidgets.Components.Base;

namespace EgorLin.UIWidgets.Components.Basic.Base {

    public abstract class ProgressComponentModule : WindowComponentModule {

        public ProgressComponent progressComponent;

        public override void ValidateEditor() {
            
            base.ValidateEditor();
            
            this.progressComponent = this.windowComponent as ProgressComponent;
            
        }

        public virtual void OnValueChanged(float f) {
            
        }

    }
    
}
