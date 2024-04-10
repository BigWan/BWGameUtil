using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEditor;
using UnityEditor.UI;

namespace BW.GameCode.UI
{
    [CustomEditor(typeof(BWButton))]
    public class BWButtonEditor : ButtonEditor
    {
        SerializedProperty buttonTransitions;

        protected override void OnEnable() {
            base.OnEnable();
            buttonTransitions = serializedObject.FindProperty("m_buttonTransitions");
        }
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            base.serializedObject.Update();
            EditorGUILayout.PropertyField(buttonTransitions);
            base.serializedObject.ApplyModifiedProperties();

        }
    }
}