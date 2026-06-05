using EgorLin.UIWidgets.Core;
using EgorLin.UIWidgets.Essentials.Console.Runtime.Windows.UIConsole.Components;
using EgorLin.UIWidgets.Types;
using UnityEngine;

namespace EgorLin.UIWidgets.Essentials.Console.Runtime.Windows.UIConsole {

    public class UIConsoleScreen : LayoutWindowType, IConsoleScreen {

        public UnityEngine.UIElements.UIDocument document;
        private UIConsoleComponent consoleComponent;

        private WindowSystemConsoleModule consoleModule;
        private WindowSystemConsole GetConsole() {

            if (this.consoleModule is null == false) return this.consoleModule.console;
            
            var module = WindowSystem.GetWindowSystemModule<WindowSystemConsoleModule>();
            if (module != null) {

                this.consoleModule = module;

            }
            return module.console;

        }
        
        public override void OnInit() {
            
            base.OnInit();

            this.GetLayoutComponent(out this.consoleComponent);

        }

        public void ApplyCommand(string command, bool autoComplete = false) {
            
            this.consoleComponent.ApplyCommand(command, autoComplete);
            
        }
        
        public void AddLine(string text, LogType logType = LogType.Log, bool isCommand = false, bool canCopy = false) {

            var console = this.GetConsole();
            console.AddLine(text, logType, isCommand, canCopy);
            
        }
        
        public override void OnShowBegin() {
            
            base.OnShowBegin();

            this.consoleComponent.SetInfo(this.document);

        }

        public void CloseCustomPopup() {
            
            this.consoleComponent.customPopupComponent.Hide();
            
        }

    }
    
}