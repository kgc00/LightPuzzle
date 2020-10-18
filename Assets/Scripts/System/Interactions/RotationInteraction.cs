using System.Collections;
using System.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace System.Interactions {
    public class RotationInteraction : MonoBehaviour, IInteractable {
        private bool rotating;
        private int rotationModifer = 1;
        private float RotationAmount => 90f * rotationModifer;
        public const float Duration = 0.5f;

        public IEnumerator HandleInteraction() {
            if (rotating) yield break;

            rotating = true;

            var sequence = DOTween.Sequence();

            sequence.Append(
                gameObject.transform
                    .DORotate(GetNewEuler(), Duration)
                    .SetEase(Ease.OutSine)
            ).AppendCallback(EndRotation);

            sequence.Play();
        }

        private void EndRotation() {
            rotating = false;
            FlipRotationDirection();
        }

        private Vector3 GetNewEuler() =>
            new Vector3(0, 0, CalculateNewAngle(gameObject.transform.rotation.eulerAngles.z));

        private float CalculateNewAngle(float angle) => (angle - RotationAmount) % 360f;

        /// <summary>
        /// Flip direction so mirror only rotates back and forth from original position to new position.
        /// </summary>
        private void FlipRotationDirection() {
            rotationModifer *= -1;
        }
    }
}