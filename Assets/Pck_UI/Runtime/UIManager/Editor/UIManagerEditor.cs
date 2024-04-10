

using UnityEditor;
namespace BW.GameCode.UI
{
    [CustomEditor(typeof(UIManager))]
    public class UIManagerEditor : Editor
    {
        public override void OnInspectorGUI() {
            var tar = target as UIManager;
            base.OnInspectorGUI();
            EditorGUILayout.LabelField(tar.GetStackLayer());
        }
    }
}