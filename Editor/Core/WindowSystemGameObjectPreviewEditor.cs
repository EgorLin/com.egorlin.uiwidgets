using EgorLin.UIWidgets.Core;
using UnityEditor;
using UnityEngine;

namespace EgorLin.UIWidgets.Editor.Core {

    [CustomPreview(typeof(GameObject))]
    public class WindowSystemGameObjectPreviewEditor : ObjectPreview {

        private static global::UnityEditor.Editor editor;
        private static Object obj;

        public override void Initialize(Object[] targets) {
            
            base.Initialize(targets);
            
            this.ValidateEditor(targets);
            
        }

        private void ValidateEditor(Object[] targets) {

            if (targets.Length > 1) {
                
                this.Reset();
                return;

            }
            
            var targetGameObject = this.target as GameObject;
            if (targetGameObject == null) {
                
                this.Reset();
                return;
                
            }

            var hasPreview = targetGameObject.GetComponent<IHasPreview>();
            if (hasPreview == null) {

                this.Reset();
                return;

            }

            if (WindowSystemGameObjectPreviewEditor.editor == null || WindowSystemGameObjectPreviewEditor.obj != targetGameObject) {
                
                WindowSystemGameObjectPreviewEditor.obj = targetGameObject;
                WindowSystemGameObjectPreviewEditor.editor = global::UnityEditor.Editor.CreateEditor((Object)hasPreview);
                
            }
            
        }

        private void Reset() {

            WindowSystemGameObjectPreviewEditor.obj = null;
            WindowSystemGameObjectPreviewEditor.editor = null;

        }
        
        public override GUIContent GetPreviewTitle() {

            if (WindowSystemGameObjectPreviewEditor.editor != null) {

                return WindowSystemGameObjectPreviewEditor.editor.GetPreviewTitle();

            }
            
            return base.GetPreviewTitle();
            
        }

        public override bool HasPreviewGUI() {
            
            return WindowSystemGameObjectPreviewEditor.editor != null && WindowSystemGameObjectPreviewEditor.editor.HasPreviewGUI();

        }
        
        public override void OnInteractivePreviewGUI(Rect r, GUIStyle background) {
            
            if (WindowSystemGameObjectPreviewEditor.editor != null) WindowSystemGameObjectPreviewEditor.editor.OnInteractivePreviewGUI(r, background);
            
        }

        public override void OnPreviewGUI(Rect r, GUIStyle background) {
            
            if (WindowSystemGameObjectPreviewEditor.editor != null) WindowSystemGameObjectPreviewEditor.editor.OnPreviewGUI(r, background);
            
        }

    }

}
