using Models;
using UnityEngine;
public static class PointExtensions {

    public static Vector3 ToVector3 (this Point p) {
        // 0 should be replaced with the unit's actualy z index
        return new Vector3 (p.x, p.y, 0);
    }

    // public static Directions ToDirection (this Point p) {
    //     switch (p.ToString ()) {
    //         case "(0,1)":
    //             return Directions.North;
    //         case "(0,-1)":
    //             return Directions.South;
    //         case "(-1,0)":
    //             return Directions.West;
    //         case "(1,0)":
    //             return Directions.East;
    //         default:
    //             return Directions.None;
    //     }
    // }
}