using EgorLin.UIWidgets.Components.Base;
using EgorLin.UIWidgets.Components.Basic.Base;
using EgorLin.UIWidgets.Core;

namespace EgorLin.UIWidgets.Essentials.Tutorial.Runtime.ComponentModules {
    [ComponentModuleDisplayName("Essentials.Tutorial/Button")]
    public class TutorialButtonComponentModule : ButtonComponentModule {

        public string uiTag;
        public WindowComponent highlight;

        public override void ValidateEditor() {
            
            base.ValidateEditor();

            if (this.highlight != null) {

                this.highlight.hiddenByDefault = true;
                this.highlight.AddEditorParametersRegistry(new EditorParametersRegistry(this) {
                    holdHiddenByDefault = true,
                });

            }

        }

    }

}
