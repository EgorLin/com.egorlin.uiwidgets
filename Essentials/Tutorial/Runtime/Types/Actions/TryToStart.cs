using EgorLin.UIWidgets.Components.Base;
using EgorLin.UIWidgets.Essentials.Tutorial.Runtime.Core;

namespace EgorLin.UIWidgets.Essentials.Tutorial.Runtime.Types.Actions {

    [ComponentModuleDisplayName("Try to start")]
    public struct TryToStart : IAction {

        public TutorialData tutorialData;
        public string text => $"Try to start tutorial data";

        public ActionResult Execute(in Context context) {

            if (this.tutorialData != null) {
                context.system.TryToStart(context.window, this.tutorialData, TutorialWindowEvent.OnAny, false);
            }
            
            return ActionResult.MoveNext;

        }

    }

}