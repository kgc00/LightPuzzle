// using Models;
// using PreProduction;
// using UnityEngine;
// [System.Serializable]
// public class EditorInputHandler : MonoBehaviour {
//     BoardCreator boardCreator;
//
//     public void Initialize (BoardCreator _boardCreator) {
//         this.boardCreator = _boardCreator;
//     }
//
//     private void Update () {
//         HandleInput ();
//     }
//
//     public void HandleInput () {
//         if (Input.GetKeyDown (KeyCode.W)) {
//             boardCreator.MoveAndUpdateMarker (new Point (0, 1));
//         } else if (Input.GetKeyDown (KeyCode.A)) {
//             boardCreator.MoveAndUpdateMarker (new Point (-1, 0));
//         } else if (Input.GetKeyDown (KeyCode.S)) {
//             boardCreator.MoveAndUpdateMarker (new Point (0, -1));
//         } else if (Input.GetKeyDown (KeyCode.D)) {
//             boardCreator.MoveAndUpdateMarker (new Point (1, 0));
//         }
//         // // function calls
//         else if (Input.GetKeyDown (KeyCode.O)) {
//             boardCreator.PlaceSelectedUnit (boardCreator.MarkerPosition);
//         } else if (Input.GetKeyDown (KeyCode.K)) {
//             boardCreator.DeleteUnitAt (boardCreator.MarkerPosition);
//         } else if (Input.GetKeyDown (KeyCode.P)) {
//             boardCreator.PlaceSelectedTile (boardCreator.MarkerPosition);
//         } else if (Input.GetKeyDown (KeyCode.L)) {
//             boardCreator.DeleteTileAt (boardCreator.MarkerPosition);
//         }
//
//     }
//
// }