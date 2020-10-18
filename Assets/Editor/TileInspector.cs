// using System.Collections;
// using System.Collections.Generic;
// using UnityEditor;
// using UnityEngine;
//
// [CustomEditor (typeof (Tile))]
// public class TileInspector : Editor {
//     public Tile Current {
//         get {
//             return (Tile) target;
//         }
//         set {
//             OnEnable ();
//         }
//     }
//
//     private void OnEnable () {
//         if (Current == null) {
//             Current = (Tile) Resources.Load ("Prefabs/Tile", typeof (Tile));
//         }
//     }
//
//     public override void OnInspectorGUI () {
//         DrawDefaultInspector ();
//     }
//
// }