using EgorLin.UIWidgets.Components.Basic.Base;

namespace EgorLin.UIWidgets.Components.Basic.TextModules {

    public class TextComponentFormatModule : TextComponentModule {

        public string format;

        public override void OnInit() {
            base.OnInit();
            this.textComponent.SetValueFormat(this.format);
        }

    }
    
}
