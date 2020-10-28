using System.Collections;
using System.Interfaces;
using DG.Tweening;
using Models;
using UnityEngine;

namespace System.Interactions {
    public class RotationInteraction : MonoBehaviour, IInteractable {
        public bool Rotating { get; private set; }
        private int rotationModifer = 1;
        private float RotationAmount => 180 * rotationModifer;
        public const float Duration = 0.5f;
        public bool Resetting { get; private set; }


        public IEnumerator HandleInteraction() {
            if (Rotating || Resetting) yield break;
            
            Rotating = true;
            Resetting = true;

            var sequence = DOTween.Sequence();            
            InteractionObserver.OnInteractionEvent(transform.position.Snapped());


            sequence.Append(
                gameObject.transform
                    .DORotate(GetNewEuler(), Duration)
                    .SetEase(Ease.OutSine)
                )                
                .AppendCallback(EndRotation)
                .AppendInterval(0.1f)
                .AppendCallback(Test);

            sequence.Play();
        }

        private void Test() {
            Resetting = false;
        }
        private void EndRotation() {
            Rotating = false;
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