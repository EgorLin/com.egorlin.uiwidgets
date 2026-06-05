using EgorLin.UIWidgets.Components.Base;
using EgorLin.UIWidgets.Components.Basic;
using EgorLin.UIWidgets.Core;
using EgorLin.UIWidgets.Essentials.Tutorial.Runtime.ComponentModules;
using EgorLin.UIWidgets.Essentials.Tutorial.Runtime.Core;

namespace EgorLin.UIWidgets.Essentials.Tutorial.Runtime.Types.Actions {

    [ComponentModuleDisplayName("UI/Lock Button")]
    public struct LockButton : IAction {

        public string text => $"Lock button with tag `#{this.tag}`";

        public TagComponent tag;
        public TutorialData nextOnClick;
        public bool blockNextEvents;

        public ActionResult Execute(in Context context) {

            if (this.tag.isList == true) {
                // wait for required index
                var index = this.tag.listIndex;
                if (context.window is ListComponent list) {
                    if (index >= list.Count) {
                        var contextData = context;
                        Utilities.Coroutines.NextFrame(contextData, static (contextData) => { contextData.data.RunActions(contextData, contextData.index); });
                        return ActionResult.Break;
                    }
                }
            }

            this.Do(in context);

            if (this.blockNextEvents == true) {
                return ActionResult.Break;
            }
            
            return ActionResult.MoveNext;

        }

        private void OnComplete(in Context context, WindowComponent buttonComponent) {
            
            var buttonHighlight = buttonComponent?.GetModule<TutorialButtonHighlightComponentModule>();
            if (buttonHighlight != null) {
                buttonHighlight.Do(false);
            }
            
            WindowSystem.CancelWaitInteractables();
            
            if (this.nextOnClick != null) {
                context.system.TryToStart(context.window, this.nextOnClick, TutorialWindowEvent.OnAny, false);
            }

            if (this.blockNextEvents == true) {
                context.data.RunActions(context, context.index + 1);
            }

        }

        private struct Closure {

            public LockButton obj;
            public Context context;

        }
        
        public void Do(in Context context) {
            
            var obj = this;

            var state = new Closure() {
                obj = obj,
                context = context,
            };
            if (obj.tag.isList == true) {
                if (obj.tag.ignoreSearch == true) {
                    var x = context.window as ListComponent;
                    var hasAny = false;
                    foreach (var moduleBase in x.componentModules.modules) {
                        if (moduleBase is TutorialListComponentModule module) {
                            if (module.uiTag == state.obj.tag.uiTag) {
                                var button = module.listComponent.GetItem<WindowComponent>(state.obj.tag.listIndex);
                                if (button is IInteractable c) {
                                    WindowSystem.AddWaitInteractable(() => {
                                        state.obj.OnComplete(state.context, button);
                                        if (x.scrollRect != null) {
                                            x.scrollRect.enabled = true;
                                        }
                                    }, c);
                                    if (x.scrollRect != null) {
                                        x.scrollRect.enabled = false;
                                    }
                                }
                                var buttonHighlight = button.GetModule<TutorialButtonHighlightComponentModule>();
                                if (buttonHighlight != null) {
                                    buttonHighlight.Do(true, button);
                                }
                                hasAny = true;
                            }
                        }
                    }
                    var tagModule = x.GetModule<TutorialWindowComponentTagComponentModule>();
                    if (tagModule != null) {
                        if (tagModule.uiTag == state.obj.tag.uiTag) {
                            var button = x.GetItem<WindowComponent>(state.obj.tag.listIndex);
                            WindowSystem.AddWaitInteractable(() => {
                                state.obj.OnComplete(state.context, button);
                                if (x.scrollRect != null) {
                                    x.scrollRect.enabled = true;
                                }
                            }, button as IInteractable);
                            var buttonHighlight = button.GetModule<TutorialButtonHighlightComponentModule>();
                            if (buttonHighlight != null) {
                                buttonHighlight.Do(true, button);
                            }
                            hasAny = true;
                        }
                    }
                    if (hasAny == true) {
                        if (x.scrollRect != null) {
                            x.scrollRect.enabled = false;
                        }
                    }
                } else {
                    context.window.FindComponent<ListComponent, Closure>(state, static (state, x) => {

                        var hasAny = false;
                        foreach (var moduleBase in x.componentModules.modules) {
                            if (moduleBase is TutorialListComponentModule module) {
                                if (module.uiTag == state.obj.tag.uiTag) {
                                    var button = module.listComponent.GetItem<WindowComponent>(state.obj.tag.listIndex);
                                    if (button is IInteractable c) {
                                        WindowSystem.AddWaitInteractable(() => {
                                            state.obj.OnComplete(state.context, button);
                                            if (x.scrollRect != null) {
                                                x.scrollRect.enabled = true;
                                            }
                                        }, c);
                                    }
                                    var buttonHighlight = button.GetModule<TutorialButtonHighlightComponentModule>();
                                    if (buttonHighlight != null) {
                                        buttonHighlight.Do(true, button);
                                    }
                                    hasAny = true;
                                }
                            }
                        }

                        var tagModule = x.GetModule<TutorialWindowComponentTagComponentModule>();
                        if (tagModule != null) {
                            if (tagModule.uiTag == state.obj.tag.uiTag) {
                                var button = x.GetItem<WindowComponent>(state.obj.tag.listIndex);
                                WindowSystem.AddWaitInteractable(() => {
                                    state.obj.OnComplete(state.context, button);
                                    if (x.scrollRect != null) {
                                        x.scrollRect.enabled = true;
                                    }
                                }, button as IInteractable);
                                var buttonHighlight = button.GetModule<TutorialButtonHighlightComponentModule>();
                                if (buttonHighlight != null) {
                                    buttonHighlight.Do(true, button);
                                }
                                hasAny = true;
                            }
                        }

                        if (hasAny == true) {
                            if (x.scrollRect != null) {
                                x.scrollRect.enabled = false;
                            }
                        }

                        return hasAny;

                    });
                }
            } else {
                if (obj.tag.ignoreSearch == true) {
                    var x = context.window as ButtonComponent;
                    var tagModule = x.GetModule<TutorialWindowComponentTagComponentModule>();
                    if (tagModule == null) return;

                    if (tagModule.uiTag == state.obj.tag.uiTag) {

                        WindowSystem.AddWaitInteractable(() => state.obj.OnComplete(state.context, x), x);
                        var buttonHighlight = x.GetModule<TutorialButtonHighlightComponentModule>();
                        if (buttonHighlight != null) {
                            buttonHighlight.Do(true, x);
                        }

                    }
                } else {
                    context.window.FindComponent<ButtonComponent, Closure>(state, static (state, x) => {

                        var tagModule = x.GetModule<TutorialWindowComponentTagComponentModule>();
                        if (tagModule == null) return false;

                        if (tagModule.uiTag == state.obj.tag.uiTag) {

                            WindowSystem.AddWaitInteractable(() => state.obj.OnComplete(state.context, x), x);
                            var buttonHighlight = x.GetModule<TutorialButtonHighlightComponentModule>();
                            if (buttonHighlight != null) {
                                buttonHighlight.Do(true, x);
                            }

                            return true;

                        }

                        return false;

                    });
                }
            }

        }

    }

}