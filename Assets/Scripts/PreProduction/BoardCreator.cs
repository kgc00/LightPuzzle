using System.Collections.Generic;
using System.IO;
using Entity;
using Models;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

#if !UNITY_EDITOR
using UnityEditor;
#endif

namespace PreProduction {
    public class BoardCreator : MonoBehaviour {
//         [SerializeField] GameObject tileSelectionIndicatorPrefab;
//         [SerializeField] public List<Tile> tilePrefabs = new List<Tile> ();
//         int currentTileIndex = -1;
//         [SerializeField] public List<Actor> actorPrefabs = new List<Actor> ();
//         int currentUnitIndex = -1;
//         public Point MarkerPosition { get; private set; }
//         Transform marker;
//         string fileName = "boardcreator";
//         [HideInInspector] public EditorInputHandler InputHandler;
//         [SerializeField] int width = 10; // world space x
//         [SerializeField] int depth = 10; // world space y
//
//         [SerializeField] LevelData levelData;
//         private Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile> ();
//         private Dictionary<Point, Actor> actors = new Dictionary<Point, Actor> ();
//         [HideInInspector] public Board.Board Current;
//
//         private void Awake () {
//             GameObject instance = Instantiate (
//                 tileSelectionIndicatorPrefab, transform
//             );
//             marker = instance.transform;
//             MoveAndUpdateMarker (new Point (0, 0));
//             Current = gameObject.AddComponent<Board.Board> ();
//             InputHandler = gameObject.AddComponent<EditorInputHandler> ();
//             InputHandler.Initialize (this);
//         }
//
//         public void ClearBoard () {
//             List<Point> tilePositions = new List<Point> (tiles.Keys);
//             foreach (Point pos in tilePositions) {
//                 DeleteTileAt (pos);
//             }
//
//             List<Point> unitPositions = new List<Point> (actors.Keys);
//             foreach (Point pos in unitPositions) {
//                 DeleteUnitAt (pos);
//             }
//         }
//
//         public void RefreshUnitTypes () {
//             actorPrefabs.Clear ();
//             UnityEngine.Object[] tmp = Resources.LoadAll ("Prefabs", typeof (Actor));
//             for (int i = 0; i < tmp.Length; ++i) {
//                 actorPrefabs.Add ((Actor) tmp[i]);
//             }
//         }
//
//         public void RefreshTileTypes () {
//             tilePrefabs.Clear ();
//             UnityEngine.Object[] tmp = Resources.LoadAll ("Prefabs", typeof (Tile));
//             for (int i = 0; i < tmp.Length; ++i) {
//                 tilePrefabs.Add ((Tile) tmp[i]);
//             }
//         }
//
//         public void SetFileName (string s) {
//             if (s != null && s.Length > 0) {
//                 fileName = s;
//             }
//         }
//
//         public void MoveAndUpdateMarker (Point direction) {
//             MarkerPosition += direction;
//             marker.position = new Vector3 (MarkerPosition.x, MarkerPosition.y, -2);
//         }
//
//         public void FillBoard () {
//             for (int i = 0; i < width; i++) {
//                 for (int j = 0; j < depth; j++) {
//                     PlaceSelectedTile (new Point (i, j));
//                 }
//             }
//         }
//
//         public void PlaceSelectedTile (Point p) {
//             PlaceTile (p, tilePrefabs[currentTileIndex].TypeReference);
//         }
//
//         public void PlaceTile (Point p, TileTypes type) {
//             if (tiles.ContainsKey (p)) {
//                 tiles.Remove (p);
//                 Current.DeleteTileAt (p);
//             }
//
//             Tile tile = Current.CreateTileAt (p, type);
//             tile.transform.parent = gameObject.transform;
//
//             // Put tile in the dictionary
//             tiles.Add (tile.Position, tile);
//         }
//         public void DeleteTileAt (Point p) {
//             if (tiles.ContainsKey (p))
//                 Current.DeleteTileAt (p);
//         }
//         public void UpdateSelectedTileType (int i) {
//             if (tilePrefabs.Count > i && tilePrefabs[i] != null &&
//                 i >= 0
//             ) {
//                 currentTileIndex = i;
//             } else {
//                 Debug.Log ("failed");
//             }
//         }
//         public void PlaceSelectedUnit (Point p) {
//             PlaceUnit (p, actorPrefabs[currentUnitIndex].TypeReference);
//         }
//
//         public void PlaceUnit (Point p, UnitTypes type) {
//             if (actors.ContainsKey (p)) {
//                 Current.DeleteUnitAt (p);
//                 actors.Remove (p);
//             }
//
//             CanvasScaler.Unit unit = Current.CreateUnitAt (p, type);
//             unit.transform.parent = gameObject.transform;
//
//             // Put unit in the dictionary
//             actors.Add (unit.Position, unit);
//         }
//         public void DeleteUnitAt (Point p) {
//             if (actors.ContainsKey (p)) {
//                 Current.DeleteUnitAt (p);
//                 actors.Remove (p);
//             }
//         }
//         public void UpdateSelectedUnitType (int i) {
//             if (actorPrefabs.Count > i && actorPrefabs[i] != null &&
//                 i >= 0
//             ) {
//                 currentUnitIndex = i;
//             } else {
//                 Debug.Log ("failed");
//             }
//         }
//         public void Save () {
//             string filePath = Application.dataPath + "/Resources/Levels";
//             if (!Directory.Exists (filePath))
//                 CreateSaveDirectory ();
//
//             LevelData boardData = ScriptableObject.CreateInstance<LevelData> ();
//
//             boardData.tiles = new List<TileSpawnData> ();
//             foreach (
//                 KeyValuePair<Point, Tile> element in tiles) {
//                 boardData.tiles.Add (new TileSpawnData (element.Key, element.Value.TypeReference));
//             }
//
//             boardData.units = new List<UnitSpawnData> ();
//             foreach (KeyValuePair<Point, Actor> element in actors)
//                 boardData.units.Add (new UnitSpawnData (element.Key, element.Value.TypeReference));
// #if UNITY_EDITOR
//             string fileURI = string.Format (
//                 "Assets/Resources/Levels/{1}.asset",
//                 filePath, fileName);
//             AssetDatabase.CreateAsset (boardData, fileURI);
// #endif
//         }
//         void CreateSaveDirectory () {
// #if UNITY_EDITOR
//             string filePath = Application.dataPath + "/Resources";
//             if (!Directory.Exists (filePath))
//                 AssetDatabase.CreateFolder ("Assets", "Resources");
//             filePath += "/Levels";
//             if (!Directory.Exists (filePath))
//                 AssetDatabase.CreateFolder ("Assets/Resources", "Levels");
//             AssetDatabase.Refresh ();
// #endif
//         }
//         public void Load () {
//             Current.CreateUnitFactory ();
//             ClearBoard ();
//             if (levelData == null)
//                 return;
//
//             foreach (TileSpawnData data in levelData.tiles) {
//                 PlaceTile (data.location, data.tileRef);
//             }
//
//             foreach (UnitSpawnData data in levelData.units) {
//                 PlaceUnit (data.location, data.unitRef);
//             }
//         }
    }
}