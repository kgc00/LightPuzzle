using System;
using UnityEngine;

namespace Models {
    public static class Vector3Extensions {
        public static Point ToPoint (this Vector3 v) {
            return new Point ((int) Mathf.Round (v.x), (int) Mathf.Round (v.y));
        }
        
        public static Vector3 Snapped(this  Vector3 v) => new Vector3(Rounded(v.x),Rounded(v.y),Rounded(v.z));

        // https://stackoverflow.com/questions/1329426/how-do-i-round-to-the-nearest-0-5
        private static float Rounded(float value) => (float) Math.Round(value * 2, MidpointRounding.AwayFromZero) / 2;
    }
}