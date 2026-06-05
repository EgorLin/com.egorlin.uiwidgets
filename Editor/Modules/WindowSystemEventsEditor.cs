using EgorLin.UIWidgets.Core;
using EgorLin.UIWidgets.Editor.Utilities;
using EgorLin.UIWidgets.Modules;
using EgorLin.UIWidgets.Utilities;
using UnityEditor;
using UnityEngine;

namespace EgorLin.UIWidgets.Editor.Modules {
    [CustomEditor(typeof(WindowSystemEvents))]
    public class WindowSystemEventsEditor : global::UnityEditor.Editor {

        public void OnEnable() {

            EditorApplication.update += this.Repaint;

        }

        public override void OnInspectorGUI() {

            GUILayoutExt.DrawComponentHeader(this.serializedObject, "EXT", () => {
                
                GUILayout.Label("WindowSystem Internal module.\nEvents system.", GUILayout.Height(36f));
                
            }, new Color(0.4f, 0.2f, 0.7f, 1f));

            var target = this.target as WindowSystemEvents;

            global::EgorLin.UIWidgets.Editor.Utilities.GUILayoutExt.DrawHeader("Registered Events");
            this.DrawRegistry(target.registry);
            foreach (var item in target.registriesGeneric) {
                this.DrawRegistry(item);
            }

        }

        private void DrawRegistry(WindowSystemEvents.RegistryBase registry) {
            
            global::EgorLin.UIWidgets.Editor.Utilities.GUILayoutExt.DrawHeader($"Registry: {registry.GetType().Name}");
            foreach (var item in registry.GetObjects()) {

                var count = registry.GetCount(item.Key);
                if (count == 0) continue;
                global::EgorLin.UIWidgets.Editor.Utilities.GUILayoutExt.Box(2f, 2f, () => {

                    if (item.Value.instance == null) {
                        
                        GUILayout.Label($"Object: {item.Value.name}");
                        
                    } else {

                        EditorGUILayout.ObjectField("Object", item.Value.instance, typeof(Object), allowSceneObjects: true);

                    }

                    UIWSMath.GetKey(item.Key, out var hash, out var evt);
                    GUILayout.Label($"Event: {(WindowEvent)evt} ({(registry.ContainsCache(item.Key) == true ? "Multiple" : "Once")}) count: {count}");

                });

            }
            
        }

    }

}