using EgorLin.UIWidgets.Core.Base;
using UnityEngine;
#if UNITY_EDITOR
using EgorLin.UIWidgets.Tools.Editor;
using UnityEditor;
#endif

namespace EgorLin.UIWidgets.Tools
{
    [AddComponentMenu("UI.Windows/Add Component Draft")]
    public class OpenAddWindowComponent : WindowObject {
#if UNITY_EDITOR
    private void Reset() {
        CreateComponentDraftWindow.Open(this.gameObject);
        EditorApplication.delayCall += () => {
            if (this != null) DestroyImmediate(this, true);
        };
    }
#endif
    }
}