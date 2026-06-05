 using EgorLin.UIWidgets.Components.Base;
 using EgorLin.UIWidgets.Core;
 using EgorLin.UIWidgets.Essentials.Tutorial.Runtime.Core;

 namespace EgorLin.UIWidgets.Essentials.Tutorial.Runtime.Types.Actions {

    [ComponentModuleDisplayName("Wait for click")]
    public struct WaitForClick : IAction {

        public string text => $"Wait for click before next action";

        public ActionResult Execute(in Context context) {

            WindowSystem.RegisterOnPointerUp(context, static (contextData, cbk) => {
            
                WindowSystem.UnRegisterOnPointerUp(cbk);
                contextData.data.RunActions(contextData, contextData.index + 1);

            });
            
            return ActionResult.Break;

        }

    }

}