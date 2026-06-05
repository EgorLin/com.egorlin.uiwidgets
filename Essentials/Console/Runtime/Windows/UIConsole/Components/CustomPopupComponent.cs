using EgorLin.UIWidgets.Components.Base;
using UnityEngine.UIElements;

namespace EgorLin.UIWidgets.Essentials.Console.Runtime.Windows.UIConsole.Components {

    using Button = UnityEngine.UIElements.Button;

    public class CustomPopupComponent : WindowComponent {

        public UnityEngine.UIElements.UIDocument document;

        private LineInfo lineInfo;
        private UIConsoleComponent uiConsole;
        public VisualElement content;
        public Button close;

        public void SetInfo(UIConsoleComponent component, VisualElement root) {

            this.uiConsole = component;

            this.content = this.document.rootVisualElement.Q<VisualElement>("Content");
            this.close = this.document.rootVisualElement.Q<Button>("CloseButton");
            
            this.close.UnregisterCallback<ClickEvent>(this.Close);
            this.close.RegisterCallback<ClickEvent>(this.Close);
            
            this.content.Add(root);
            
        }

        private void Close(ClickEvent evt) {
            
            this.Hide();
            
        }

    }

}