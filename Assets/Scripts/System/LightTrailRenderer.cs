using System.Collections.Generic;
using System.Interactions;
using System.Interfaces;
using Models;
using UnityEngine;

namespace System {
    [RequireComponent(typeof(IInteractionTracker))]
    [RequireComponent(typeof(LineRenderer))]
    public class LightTrailRenderer : MonoBehaviour {
        private IInteractionTracker tracker;
        private LineRenderer lineRenderer;
        private List<InteractionEvent> history => tracker.History;

        public Color c1 = Color.yellow;
        public Color c2 = Color.red;
        private Transform trackerTrn;
        private Vector3 trackerPos;
        private void Awake() {
            lineRenderer = GetComponent<LineRenderer>();
            tracker = GetComponent<IInteractionTracker>();
            trackerTrn = tracker.Behaviour.transform;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.widthMultiplier = 0.2f;
            float alpha = 1.0f;
            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            );
            lineRenderer.colorGradient = gradient;

        }


        private void LateUpdate() {
            trackerPos = trackerTrn.position;
            
            // there is some jenk when drawing a line where two points are too close together, or the angle
            // between the points is too extreme.
            //
            // adding a check to see that we aren't sitting in one place (such as at a gate or wall)
            // before i draw the final connection from most recent history event to obj pos
            if (Vector3.Distance(history[0].InteractorSnappedPosition, trackerPos) > 0.05f) {
                lineRenderer.positionCount = history.Count + 1;
                var points = new Vector3[history.Count + 1];
                var t = Time.time;
                for (int i = points.Length; i-- > 1;) {
                    points[i] = history[i-1].InteractorSnappedPosition;
                }
                points[0] = new Vector3(trackerPos.x, trackerPos.y, 0);
                lineRenderer.SetPositions(points);
            }
            else {           
                lineRenderer.positionCount = history.Count;
                var points = new Vector3[history.Count];
                var t = Time.time;
                for (int i = points.Length; i-- > 1;) {
                    points[i] = history[i].InteractorSnappedPosition;
                }
                points[0] = new Vector3(trackerPos.x, trackerPos.y, 0);
                lineRenderer.SetPositions(points);
            }
        }
    }
}