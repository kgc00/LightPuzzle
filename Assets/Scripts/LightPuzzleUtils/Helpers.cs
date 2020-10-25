using Models;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace LightPuzzleUtils {
    public static class Helpers {
        public static Vector2 ProvideCappedForceV2(Vector2 force, Vector2 velCap, Vector2 dir, Vector2 rigVel) {
            var xForce = ProvideCappedForceX(force.x, velCap.x, dir.x, rigVel.x);
            var yForce = ProvideCappedForceX(force.y, velCap.y, dir.y, rigVel.y);

            return new Vector2(xForce, yForce);
        }

        private static float ProvideCappedForceX(float forceX, float velCapX, float dirX, float rigVelX) =>
            ProvideCappedForce(forceX, velCapX, dirX, rigVelX);

        private static float ProvideCappedForceY(float forceY, float velCapY, float dirY, float rigVelY) =>
            ProvideCappedForce(forceY, velCapY, dirY, rigVelY);

        private static float ProvideCappedForce(float force, float velCap, float dir, float rigVel) {
            int CappedMoveVelocity() => 0;
            float NormalMoveVelocity() => dir * force;
            bool HitSpeedCap(float xVel) => Mathf.Abs(xVel) >= velCap;
            bool IsForwardMovement(float xVel) => Mathf.Sign(dir) == Mathf.Sign(xVel);

            return HitSpeedCap(rigVel) && IsForwardMovement(rigVel)
                ? CappedMoveVelocity()
                : NormalMoveVelocity();
        }

        public static Color ColorFromLightColor(LightColor colorToProvide) {
            Color c = default;
            switch (colorToProvide) {
                case LightColor.Blue:
                    c = Color.blue;
                    break;
                case LightColor.Red:
                    c = Color.red;
                    break;
                case LightColor.Green:
                    c = Color.green;
                    break;
                case LightColor.Yellow:
                    c = Color.yellow;
                    break;
                case LightColor.Magenta:
                    c = Color.magenta;
                    break;
                case LightColor.White:
                    c = Color.white;
                    break;
            }

            return c;
        }

        public static Vector3 SnappedCollisionPosFromInteractorPos(Transform transform) =>
            (transform.position.Snapped() + transform.up * 0.5f).Snapped();

        public static Vector3 GetMultiCellSnappedCollisionPos(Transform transform, TilemapCollider2D collider2D) =>
            collider2D.bounds.Contains(transform.position)
                ? transform.position.Snapped()
                : SnappedCollisionPosFromInteractorPos(transform);

        public static Vector3 GetMultiCellSnappedCollisionPos(Transform transform, BoxCollider2D collider2D) {
            Debug.Log(collider2D.bounds.Contains(transform.position));
            return collider2D.bounds.Contains(transform.position)
                ? transform.position.Snapped()
                : SnappedCollisionPosFromInteractorPos(transform);
        }
    }
}