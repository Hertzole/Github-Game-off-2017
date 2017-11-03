using UnityEditor;
using UnityEngine;

namespace Hertzole.Github2017.Editor
{
    [CustomEditor(typeof(CameraSwitch))]
    public class CameraSwitchEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawDefaultInspector();
            if (GUILayout.Button("Mimic Main Camera"))
            {
                SerializedProperty position = serializedObject.FindProperty("m_SwitchToPosition");
                SerializedProperty rotation = serializedObject.FindProperty("m_SwitchToRotation");
                Transform camTransform = Camera.main.transform;
                position.vector3Value = camTransform.position;
                rotation.vector3Value = camTransform.eulerAngles;
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
