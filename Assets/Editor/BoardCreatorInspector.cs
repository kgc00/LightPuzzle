// using System.Collections;
// using System.Collections.Generic;
// using PreProduction;
// using UnityEditor;
// using UnityEngine;
//
// [CustomEditor (typeof (BoardCreator))]
// public class BoardCreatorInspector : Editor {
//
//     string[] unitNames;
//     int spawnUnitIndex = 0;
//     string fileName = "";
//     string[] tileNames;
//     int spawnTileIndex = 0;
//
//     public BoardCreator Current {
//         get {
//             return (BoardCreator) target;
//         }
//     }
//
//     private void OnEnable () {
//         Current.RefreshUnitTypes ();
//         Current.RefreshTileTypes ();
//     }
//
//     public override void OnInspectorGUI () {
//         unitNames = getUnitNames ();
//         tileNames = getTileNames ();
//
//         DrawDefaultInspector ();
//
//         GUILayout.BeginHorizontal ("box");
//         GUILayout.Label ("Level Name");
//         fileName = GUILayout.TextField (fileName, 25);
//         GUILayout.EndHorizontal ();
//
//         GUILayout.BeginHorizontal ("box");
//         GUILayout.Label ("Spawn Tile");
//         spawnTileIndex = EditorGUILayout.Popup (spawnTileIndex, tileNames);
//         Current.UpdateSelectedTileType (spawnTileIndex);
//         GUILayout.EndHorizontal ();
//
//         GUILayout.BeginHorizontal ("box");
//         GUILayout.Label ("Spawn Unit");
//         spawnUnitIndex = EditorGUILayout.Popup (spawnUnitIndex, unitNames);
//         Current.UpdateSelectedUnitType (spawnUnitIndex);
//         GUILayout.EndHorizontal ();
//
//         // if (GUILayout.Button ("Refresh")) {
//         //     Current.RefreshUnitTypes ();
//         //     Current.RefreshTileTypes ();
//         // }
//         if (GUILayout.Button ("Fill Board")) {
//             Current.FillBoard ();
//         }
//         if (GUILayout.Button ("Clear Board")) {
//             Current.ClearBoard ();
//         }
//         if (GUILayout.Button ("Place Tile: 'P'")) {
//             Current.PlaceSelectedTile (Current.MarkerPosition);
//         }
//
//         if (GUILayout.Button ("Delete Tile: 'L'")) {
//             Current.DeleteTileAt (Current.MarkerPosition);
//         }
//         if (GUILayout.Button ("Place Unit: 'O'")) {
//             Current.PlaceSelectedUnit (Current.MarkerPosition);
//         }
//         if (GUILayout.Button ("Delete Unit: 'K'")) {
//             Current.DeleteUnitAt (Current.MarkerPosition);
//         }
//         if (GUILayout.Button ("Save")) {
//             Current.SetFileName (fileName);
//             Current.Save ();
//         }
//         if (GUILayout.Button ("Load")) {
//             Current.Load ();
//         }
//     }
//
//     private string[] getUnitNames () {
//         List<string> names = new List<string> ();
//         foreach (Unit unit in Current.unitPrefabs) {
//             names.Add (unit.ToString ());
//         }
//         return names.ToArray ();
//     }
//
//     private string[] getTileNames () {
//         List<string> names = new List<string> ();
//         foreach (Tile tile in Current.tilePrefabs) {
//             names.Add (tile.ToString ());
//         }
//         return names.ToArray ();
//     }
// }