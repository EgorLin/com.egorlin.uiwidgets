using EgorLin.UIWidgets.Components.Base;
using EgorLin.UIWidgets.Core;
using EgorLin.UIWidgets.Core.Base;
using EgorLin.UIWidgets.Essentials.Tutorial.Runtime.Core;

namespace EgorLin.UIWidgets.Essentials.Tutorial.Runtime.Types.Actions {

    [ComponentModuleDisplayName("UI/Show Screen")]
    public struct ShowScreen : IAction {

        public string text => $"Show screen `{this.source}`";

        public WindowBase source;
        public TutorialData onShown;

        public ActionResult Execute(in Context context) {

            var obj = this;
            var contextData = context;
            WindowSystem.Show(this.source,
                                  default,
                              (x) => x.OnEmptyPass(),
                              TransitionParameters.Default.ReplaceCallback(() => {
                                  contextData.system.TryToStart(contextData.window, obj.onShown, TutorialWindowEvent.OnAny, false);
                              }));

            return ActionResult.MoveNext;

        }

    }

}