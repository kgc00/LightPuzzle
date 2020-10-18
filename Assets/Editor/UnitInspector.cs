// using System.Collections;
// using System.Collections.Generic;
// using UnityEditor;
// using UnityEngine;
//
// [CustomEditor (typeof (Unit))]
// public class UnitInspector : Editor {
//     public Unit Current {
//         get {
//             return (Unit) target;
//         }
//         set {
//             OnEnable ();
//         }
//     }
//
//     private void OnEnable () {
//         if (Current == null) {
//             Current = (Unit) Resources.Load ("Prefabs/Unit", typeof (Unit));
//         }
//     }
//
//     public override void OnInspectorGUI () {
//         DrawDefaultInspector ();
//
//     }
// }