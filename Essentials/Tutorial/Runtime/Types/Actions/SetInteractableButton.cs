using EgorLin.UIWidgets.Components.Base;
using EgorLin.UIWidgets.Components.Basic;
using EgorLin.UIWidgets.Essentials.Tutorial.Runtime.ComponentModules;
using EgorLin.UIWidgets.Essentials.Tutorial.Runtime.Core;

namespace EgorLin.UIWidgets.Essentials.Tutorial.Runtime.Types.Actions {

    [ComponentModuleDisplayName("UI/Set Interactable Button")]
    public struct SetInteractableButton : IAction {

        public string text => $"Set interactable button with tag `#{this.tag}`";

        public TagComponent tag;
        public bool state;

        public ActionResult Execute(in Context context) {

            this.Do(in context);

            return ActionResult.MoveNext;

        }

        private struct Closure {

            public SetInteractableButton obj;
            public Context context;

        }
        
        public void Do(in Context context) {
            
            var obj = this;

            var state = new Closure() {
                obj = obj,
                context = context,
            };
            if (obj.tag.isList == true) {
                context.window.FindComponent<ListComponent, Closure>(state, static (state, x) => {

                    foreach (var moduleBase in x.componentModules.modules) {
                        if (moduleBase is TutorialListComponentModule module) {
                            if (module.uiTag == state.obj.tag.uiTag) {
                                var button = module.listComponent.GetItem<WindowComponent>(state.obj.tag.listIndex);
                                if (button is IInteractable c) {
                                    c.SetInteractable(state.obj.state);
                                }
                            }
                        }
                    }
                    var tagModule = x.GetModule<TutorialWindowComponentTagComponentModule>();
                    if (tagModule == null) return false;

                    if (tagModule.uiTag == state.obj.tag.uiTag) {
                        var button = x.GetItem<WindowComponent>(state.obj.tag.listIndex);
                        if (button is IInteractable c) {
                            c.SetInteractable(state.obj.state);
                        }
                        return true;
                    }

                    return false;

                });
            } else {
                context.window.FindComponent<ButtonComponent, Closure>(state, static (state, x) => {

                    var tagModule = x.GetModule<TutorialWindowComponentTagComponentModule>();
                    if (tagModule == null) return false;

                    if (tagModule.uiTag == state.obj.tag.uiTag) {

                        x.SetInteractable(state.obj.state);
                        return true;

                    }

                    return false;

                });
            }

        }

    }

}