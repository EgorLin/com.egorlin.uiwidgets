using EgorLin.UIWidgets.Components.Base;
using EgorLin.UIWidgets.Components.Basic;
using UnityEngine;

namespace EgorLin.UIWidgets.Essentials.Console.Runtime.Windows.Console.Components {

    public class LogsCounterComponent : WindowComponent {

        public CheckboxComponent logsInfo;
        public CheckboxComponent logsWarning;
        public CheckboxComponent logsError;

        public void SetInfo() {

            var consoleScreen = this.GetWindow<ConsoleScreen>();
            
            this.logsInfo.SetCheckedState(consoleScreen.HasLogFilterType(LogType.Log), false);
            this.logsInfo.SetCallback(state => consoleScreen.SetLogFilterType(LogType.Log, state));
            this.logsInfo.Get<TextComponent>().SetValue(consoleScreen.GetCounter(LogType.Log));
            
            this.logsWarning.SetCheckedState(consoleScreen.HasLogFilterType(LogType.Warning), false);
            this.logsWarning.SetCallback(state => consoleScreen.SetLogFilterType(LogType.Warning, state));
            this.logsWarning.Get<TextComponent>().SetValue(consoleScreen.GetCounter(LogType.Warning));
            
            this.logsError.SetCheckedState(consoleScreen.HasLogFilterType(LogType.Error), false);
            this.logsError.SetCallback(state => consoleScreen.SetLogFilterType(LogType.Error, state));
            this.logsError.Get<TextComponent>().SetValue(consoleScreen.GetCounter(LogType.Error) + consoleScreen.GetCounter(LogType.Exception));

        }

    }

}