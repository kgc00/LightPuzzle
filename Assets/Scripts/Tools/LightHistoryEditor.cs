using System;
using System.Interfaces;
using UnityEditor;

namespace Tools {
    [CustomEditor(typeof(LightInteractionTracker))]
    public class LightHistoryEditor : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            // var light = (IInteractionTracker) target;
            //
            // if (light == null) return;
            //
            // Repaint();
            //
            // EditorGUILayout.LabelField("History", EditorStyles.boldLabel);
            // EditorGUILayout.Space();
            // EditorGUILayout.TextField($"Name: {light.Behaviour.name}");
        }
    }
}