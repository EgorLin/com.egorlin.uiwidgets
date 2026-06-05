using UnityEditor;
using UnityEngine;
using WindowLayout = EgorLin.UIWidgets.Core.Layouts.WindowLayout;
using WindowObject = EgorLin.UIWidgets.Core.Base.WindowObject;

namespace EgorLin.UIWidgets.Editor.Core {
    [InitializeOnLoad]
    public static class EditorChangesTracker {

        static EditorChangesTracker() {
            
            global::UnityEditor.ObjectChangeEvents.changesPublished -= EditorChangesPublished;
            global::UnityEditor.ObjectChangeEvents.changesPublished += EditorChangesPublished;
            
        }

        private static void EditorChangesPublished(ref ObjectChangeEventStream stream) {
            for (int i = 0; i < stream.length; ++i) {
                var evt = stream.GetEventType(i);
                if (evt == global::UnityEditor.ObjectChangeKind.CreateGameObjectHierarchy) {
                    stream.GetCreateGameObjectHierarchyEvent(i, out var data);
                    var obj = global::UnityEditor.EditorUtility.InstanceIDToObject(data.instanceId) as GameObject;
                    if (obj == null) continue;
                    {
                        var layout = obj.GetComponentInParent<WindowLayout>(true);
                        if (layout?.ApplyTagsEditor() == true) {
                            global::UnityEditor.EditorUtility.SetDirty(layout.gameObject);
                        }
                    }
                    {
                        var component = obj.GetComponent<WindowObject>();
                        component?.ValidateEditor(true, false);
                    }
                } else if (evt == global::UnityEditor.ObjectChangeKind.ChangeGameObjectStructure) {
                    stream.GetChangeGameObjectStructureEvent(i, out var data);
                    var obj = global::UnityEditor.EditorUtility.InstanceIDToObject(data.instanceId) as GameObject;
                    if (obj == null) continue;
                    {
                        var layout = obj.GetComponentInParent<WindowLayout>(true);
                        if (layout?.ApplyTagsEditor() == true) {
                            global::UnityEditor.EditorUtility.SetDirty(layout.gameObject);
                        }
                    }
                    {
                        var component = obj.GetComponent<WindowObject>();
                        component?.ValidateEditor(true, false);
                    }
                } else if (evt == global::UnityEditor.ObjectChangeKind.DestroyGameObjectHierarchy) {
                    stream.GetDestroyGameObjectHierarchyEvent(i, out var data);
                    var obj = global::UnityEditor.EditorUtility.InstanceIDToObject(data.instanceId) as GameObject;
                    if (obj == null) continue;
                    {
                        var layout = obj.GetComponentInParent<WindowLayout>(true);
                        if (layout?.ApplyTagsEditor() == true) {
                            global::UnityEditor.EditorUtility.SetDirty(layout.gameObject);
                        }
                    }
                    {
                        var component = obj.GetComponent<WindowObject>();
                        component?.ValidateEditor(true, false);
                    }
                }
            }
        }

    }

}