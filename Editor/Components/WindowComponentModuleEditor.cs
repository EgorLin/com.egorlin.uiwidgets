using EgorLin.UIWidgets.Components.Base;
using UnityEditor;
using UnityEngine;

namespace EgorLin.UIWidgets.Editor.Components {
    [CustomEditor(typeof(WindowComponentModule), editorForChildClasses: true)]
    [CanEditMultipleObjects]
    public class WindowComponentModuleEditor : global::UnityEditor.Editor {

        public void OnEnable() {

            var comp = (Component)this.target;
            comp.hideFlags = HideFlags.HideInInspector;

        }

    }

}
