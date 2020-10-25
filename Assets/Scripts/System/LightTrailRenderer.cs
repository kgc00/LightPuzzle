using System.Collections.Generic;
using System.Interactions;
using System.Interfaces;
using LightPuzzleUtils;
using Models;
using UnityEngine;

namespace System {
    [RequireComponent(typeof(IInteractionTracker))]
    [RequireComponent(typeof(LineRenderer))]
    public class LightTrailRenderer : MonoBehaviour, ILightColor {
        private IInteractionTracker tracker;
        private LineRenderer lineRenderer;

        [field: SerializeField] public LightColor LightColor { get; private set; }
        private List<InteractionEvent> history => tracker.History;

        private Color endCol = Color.white;
        private Color startCol = Color.white;
        private Transform trackerTrn;
        private Vector3 trackerPos;
        private float alpha = 1.0f;

        private void Awake() {
            lineRenderer = GetComponent<LineRenderer>();
            tracker = GetComponent<IInteractionTracker>();
            trackerTrn = tracker.Behaviour.transform;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.widthMultiplier = 0.2f;
            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new[] {new GradientColorKey(endCol, 0.0f), new GradientColorKey(startCol, 1.0f)},
                new[] {new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f)}
            );
            lineRenderer.colorGradient = gradient;
        }

        public void UpdateLightColor(LightColor colorToProvide) {
            var c = Helpers.ColorFromLightColor(colorToProvide);

            LightColor = colorToProvide;

            UpdateKeys(c);
        }


        public void UpdateKeys(Color newEndCol) {
            endCol = newEndCol;
            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new[] {new GradientColorKey(endCol, 0.0f), new GradientColorKey(startCol, 1.0f)},
                new[] {new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f)}
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
                // adding 1 to array length to leave 0 index open for assignement
                lineRenderer.positionCount = history.Count + 1;
                var points = new Vector3[history.Count + 1];
                var t = Time.time;
                // using a reverse for loop, start at end of array (oldest event)
                for (int i = points.Length; i-- > 1;) {
                    // removing 1 from i to keep index 0 open while mapping index => history index
                    points[i] = history[i - 1].InteractorSnappedPosition;
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