using EgorLin.UIWidgets.Components.Base;
using EgorLin.UIWidgets.Components.Basic.Base;
using EgorLin.UIWidgets.Core;
using UnityEngine;

namespace EgorLin.UIWidgets.Components.Basic.TextModules {

    [ComponentModuleDisplayName("Animate Value")]
    public class TextComponentModuleAnimateValue : TextComponentModule {

        public float animationTime = 1f;
        public bool roundToInt = true;
        public string formatValue;

        private bool setValueInternal;

        private struct Closure {

            public TextComponentModuleAnimateValue module;
            public SourceValue sourceValue;
            public string strFormat;

        }
        
        public override void OnSetValue(double prevValue, double value, SourceValue sourceValue, string strFormat) {

            if (this.setValueInternal == true) return;

            var closureData = new Closure() {
                module = this,
                sourceValue = sourceValue,
                strFormat = strFormat,
            };

            var tweener = WindowSystem.GetTweener();
            tweener.Stop(this.textComponent);
            tweener.Add(closureData, this.animationTime, (float)prevValue, (float)value).Tag(this.textComponent).OnUpdate((closure, val) => {

                closure.module.setValueInternal = true;
                if (closure.sourceValue == SourceValue.Digits) {

                    if (string.IsNullOrEmpty(closure.module.formatValue) == true) {

                        if (closure.module.roundToInt == true) {

                            closure.module.textComponent.SetValue_INTERNAL(Mathf.RoundToInt(val), out _);

                        } else {

                            closure.module.textComponent.SetValue_INTERNAL(val, out _);

                        }

                    } else {

                        if (closure.module.roundToInt == true) {

                            closure.module.textComponent.SetText_INTERNAL(Mathf.RoundToInt(val).ToString(closure.module.formatValue));

                        } else {

                            closure.module.textComponent.SetText_INTERNAL(val.ToString(closure.module.formatValue));

                        }

                    }

                } else if (closure.sourceValue == SourceValue.Seconds) {
                    
                    closure.module.textComponent.SetText_INTERNAL(new TextComponent.TimeFormatFromSeconds() { format = closure.strFormat }.GetValue(val));
                    
                } else if (closure.sourceValue == SourceValue.Milliseconds) {
                    
                    closure.module.textComponent.SetText_INTERNAL(new TextComponent.TimeFormatFromMilliseconds() { format = closure.strFormat }.GetValue(val));
                    
                }

                closure.module.setValueInternal = false;

            });

        }

    }
    
}