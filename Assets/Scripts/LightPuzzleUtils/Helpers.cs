using UnityEngine;

namespace LightPuzzleUtils {
    public static class Helpers {
        
        public static Vector2 ProvideCappedForceV2(Vector2 force, Vector2 velCap, Vector2 dir, Vector2 rigVel) {
            var xForce = ProvideCappedForceX(force.x, velCap.x, dir.x, rigVel.x);
            var yForce = ProvideCappedForceX(force.y, velCap.y, dir.y, rigVel.y);
    
            return new Vector2(xForce, yForce);
        }

        private static  float ProvideCappedForceX(float forceX, float velCapX, float dirX, float rigVelX) =>
            ProvideCappedForce(forceX, velCapX, dirX, rigVelX);

        private static  float ProvideCappedForceY(float forceY, float velCapY, float dirY, float rigVelY) =>
            ProvideCappedForce(forceY, velCapY, dirY, rigVelY);
        private static  float ProvideCappedForce(float force, float velCap, float dir, float rigVel) {
            int CappedMoveVelocity() => 0;
            float NormalMoveVelocity() => dir * force;
            bool HitSpeedCap(float xVel) => Mathf.Abs(xVel) >= velCap;
            bool IsForwardMovement(float xVel) => Mathf.Sign(dir) == Mathf.Sign(xVel);

            return HitSpeedCap(rigVel) && IsForwardMovement(rigVel)
                ? CappedMoveVelocity()
                : NormalMoveVelocity();
        }
    }
}