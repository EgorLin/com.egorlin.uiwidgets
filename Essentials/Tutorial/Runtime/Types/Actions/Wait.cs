using EgorLin.UIWidgets.Components.Base;
using EgorLin.UIWidgets.Essentials.Tutorial.Runtime.Core;

namespace EgorLin.UIWidgets.Essentials.Tutorial.Runtime.Types.Actions {

    [ComponentModuleDisplayName("Wait")]
    public struct Wait : IAction {

        public string text => $"Wait for `{this.seconds}` seconds before next action";

        public float seconds;

        public ActionResult Execute(in Context context) {

            var contextData = context;
            Utilities.Coroutines.WaitTime(contextData, this.seconds, static (contextData) => {
            
                contextData.data.RunActions(contextData, contextData.index + 1);
    
            });
            
            return ActionResult.Break;

        }

    }

}