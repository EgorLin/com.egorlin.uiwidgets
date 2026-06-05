using EgorLin.UIWidgets.Editor.Utilities;
using EgorLin.UIWidgets.Modules;
using UnityEditor;
using UnityEngine;

namespace EgorLin.UIWidgets.Editor.Modules {
    [CustomEditor(typeof(WindowSystemPools))]
    public class WindowSystemPoolsEditor : global::UnityEditor.Editor {

        public void OnEnable() {

            EditorApplication.update += this.Repaint;

        }

        public override void OnInspectorGUI() {

            GUILayoutExt.DrawComponentHeader(this.serializedObject, "EXT", () => {
                
                GUILayout.Label("WindowSystem Internal module.\nPooling system.", GUILayout.Height(36f));
                
            }, new Color(0.4f, 0.2f, 0.7f, 1f));

            var target = this.target as WindowSystemPools;

            global::EgorLin.UIWidgets.Editor.Utilities.GUILayoutExt.DrawHeader("Registered prefabs");
            foreach (var item in target.registeredPrefabs) {

                GUILayout.Label(item.ToString());

            }

            global::EgorLin.UIWidgets.Editor.Utilities.GUILayoutExt.DrawHeader("Instances on scene");
            foreach (var item in target.instanceOnSceneToPrefab) {

                global::EgorLin.UIWidgets.Editor.Utilities.GUILayoutExt.Box(2f, 2f, () => {

                    GUILayout.Label($"Prefab: {item.Value}");
                    EditorGUILayout.ObjectField("Object", item.Key, typeof(Object), allowSceneObjects: true);

                });

            }

            global::EgorLin.UIWidgets.Editor.Utilities.GUILayoutExt.DrawHeader("Instances in pool");
            foreach (var item in target.prefabToPooledInstances) {

                global::EgorLin.UIWidgets.Editor.Utilities.GUILayoutExt.Box(2f, 2f, () => {

                    GUILayout.Label($"Prefab: {item.Key}");
                    foreach (var comp in item.Value) {

                        EditorGUILayout.ObjectField(comp, typeof(Object), true);

                    }

                });

            }

        }

    }

}