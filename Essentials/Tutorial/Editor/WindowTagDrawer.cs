using System.Linq;
using EgorLin.UIWidgets.Components.Base;
using EgorLin.UIWidgets.Components.Basic.Base;
using EgorLin.UIWidgets.Core.Base;
using EgorLin.UIWidgets.Core.Layouts;
using EgorLin.UIWidgets.Essentials.Tutorial.Runtime.Core;
using EgorLin.UIWidgets.Modules;
using EgorLin.UIWidgets.Types;
using UnityEditor;
using UnityEngine;

namespace EgorLin.UIWidgets.Essentials.Tutorial.Editor {
    
    public struct TagData {

        public int tagId;
        public WindowLayoutElement element;

    }

    [CustomPropertyDrawer(typeof(Tag))]
    public class WindowTagDrawer : PropertyDrawer {

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            
            return EditorGUIUtility.singleLineHeight * 2f;
            
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

            var windowType = property.serializedObject.FindProperty("forWindowType");
            var windowGuid = windowType.FindPropertyRelative("guid");

            var tagId = property.FindPropertyRelative("id");
            var listIndex = property.FindPropertyRelative("listIndex");

            var window = (LayoutWindowType)AssetDatabase.LoadAssetAtPath<WindowBase>(AssetDatabase.GUIDToAssetPath(windowGuid.stringValue));
            if (window != null) {

                var height = EditorGUIUtility.singleLineHeight;
                var windowLayout = ((LayoutWindowType)window).layouts.items[0].windowLayout;
                var tags = windowLayout.layoutElements.Select(x => new TagData() { tagId = x.tagId, element = x });
                var data = tags.Select(x => x.tagId).ToArray();
                var options = tags.Select(x => x.element.name).ToArray();
                position.height = height;
                tagId.intValue = EditorGUI.IntPopup(position, "Tag", tagId.intValue, options, data);

                var components = window.layouts.items[0].components;
                for (int i = 0; i < components.Length; ++i) {

                    if (components[i].tag == tagId.intValue) {

                        var editorRef = Resource.GetEditorRef<WindowComponent>(components[i].component);
                        if (editorRef is ListBaseComponent) {

                            property.FindPropertyRelative("isList").boolValue = true;
                            position.y += height;
                            listIndex.intValue = EditorGUI.IntField(position, "List Index", listIndex.intValue);

                        } else {
                            
                            property.FindPropertyRelative("isList").boolValue = false;
                            
                        }
                        break;
                        
                    }
                    
                }

            }
            
        }

    }

}