using EgorLin.UIWidgets.Components.Base;
using UnityEngine;

namespace EgorLin.UIWidgets.Components.Basic.Base {
    public abstract class TextComponentModule : WindowComponentModule {

        public TextComponent textComponent;

        public override void ValidateEditor() {
            
            base.ValidateEditor();
            
            this.textComponent = this.windowComponent as TextComponent;
            
        }

        public virtual void OnSetValue(double prevValue, double value, SourceValue sourceValue, string strFormat) { }

        public virtual void SetValue(double value, SourceValue sourceValue = SourceValue.Digits, TimeResult timeValueResult = TimeResult.None, TimeResult timeShortestVariant = TimeResult.None) { }

        public virtual string SetText(string text) {
            return text;
        }
        
        public virtual void OnSetText(string prevText, string text) { }

        public virtual void OnSetColor(Color color) { }

    }
    
}
