using EgorLin.UIWidgets.Components.Base;
using EgorLin.UIWidgets.Core;
using EgorLin.UIWidgets.Essentials.Tutorial.Runtime.Core;

namespace EgorLin.UIWidgets.Essentials.Tutorial.Runtime.Types.Actions {

    [ComponentModuleDisplayName("UI/Reset Interactables state")]
    public struct ResetInteractablesState : IAction {

        public string text => $"Unlock all interactables";

        public ActionResult Execute(in Context context) {

            WindowSystem.CancelWaitInteractables();
                
            return ActionResult.MoveNext;

        }

    }

}