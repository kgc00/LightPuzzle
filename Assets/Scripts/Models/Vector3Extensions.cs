using UnityEngine;

namespace Models {
    public static class Vector3Extensions {
        public static Point ToPoint (this Vector3 v) {
            return new Point ((int) Mathf.Round (v.x), (int) Mathf.Round (v.y));
        }
    }
}