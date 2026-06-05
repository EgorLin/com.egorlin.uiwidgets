using EgorLin.UIWidgets.Animations;
using EgorLin.UIWidgets.Components.Base;
using EgorLin.UIWidgets.Core;
using EgorLin.UIWidgets.Core.Base;
using UnityEngine;
using AnimationState = EgorLin.UIWidgets.Animations.AnimationState;

namespace EgorLin.UIWidgets.Components.Basic.DefaultModules {

    public enum AnimationTarget {

        Show = 2,
        Hide = 3,

    }

    public class AnimationsComponentModule : WindowComponentModule {

        [System.Serializable]
        public struct State {

            [Space(2f)]
            public int stateId;
            public WindowObject.AnimationParametersContainer parameters;
            public WindowObjectAnimation.TweenerCustomParameters tweenerParameters;

        }

        [System.Serializable]
        public struct States {

            public State[] items;

        }

        public States states;

        private bool TryGetState(int stateId, out State state) {

            state = default;
            foreach (var st in this.states.items) {
                if (st.stateId == stateId) {
                    state = st;
                    return true;
                }
            }

            return false;

        }

        private struct Closure {

            public System.Action onCancel;
            public System.Action onComplete;
            public System.Action<float> onUpdate;

        }

        public bool Play(int stateId,
                         AnimationTarget animationTarget,
                         TransitionParameters transitionParameters = default,
                         System.Action onComplete = null,
                         System.Action<float> onUpdate = null,
                         System.Action onCancel = null) {

            return this.Play_INTERNAL(stateId, animationTarget, onComplete, onUpdate, onCancel, default, transitionParameters, false);

        }

        public bool Play(int stateId,
                         AnimationTarget animationTarget,
                         WindowObjectAnimation.TweenerCustomParameters tweenerCustomParameters,
                         TransitionParameters transitionParameters = default,
                         System.Action onComplete = null,
                         System.Action<float> onUpdate = null,
                         System.Action onCancel = null) {

            return this.Play_INTERNAL(stateId, animationTarget, onComplete, onUpdate, onCancel, tweenerCustomParameters, transitionParameters, true);

        }

        public bool Break(int stateId) {

            if (this.TryGetState(stateId, out var state) == true) {

                WindowObjectAnimation.BreakState(state.parameters.items);
                return true;

            }

            return false;

        }

        public bool SetResetState(int stateId) {

            if (this.TryGetState(stateId, out var state) == true) {

                WindowObjectAnimation.SetResetState(state.parameters.items);
                return true;

            }

            return false;

        }

        private bool Play_INTERNAL(int stateId,
                         AnimationTarget animationTarget,
                         System.Action onComplete = null,
                         System.Action<float> onUpdate = null,
                         System.Action onCancel = null,
                         WindowObjectAnimation.TweenerCustomParameters tweenerCustomParameters = default,
                         TransitionParameters transitionParameters = default,
                         bool overrideTweenerParameters = false) {

            if (this.TryGetState(stateId, out var state) == true) {

                if (overrideTweenerParameters == false) tweenerCustomParameters = state.tweenerParameters;
                
                // Break states
                WindowObjectAnimation.BreakState(state.parameters.items);
                
                var closure = new Closure() {
                    onCancel = onCancel,
                    onComplete = onComplete,
                    onUpdate = onUpdate,
                };
                WindowObjectAnimation.Play(
                    closure,
                    this.windowComponent,
                    (AnimationState)animationTarget,
                    state.parameters.items,
                    transitionParameters,
                    tweenerCustomParameters,
                    (c) => {
                      
                        c.onComplete?.Invoke();
                      
                    },(c, value) => {
                      
                        c.onUpdate?.Invoke(value);
                      
                    }, (c) => {
                      
                        c.onCancel?.Invoke();
                      
                    });
                
                return true;
                
            }
            
            return false;
            
        }

    }

}
