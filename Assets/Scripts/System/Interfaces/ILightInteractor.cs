using System.Interactions;
using Models;
using UnityEngine;

namespace System.Interfaces {
    public interface ILightInteractor {
        GameObject Behaviour { get; }
        void StopAllCR();
        void Interact(ILightInteractable interactable);
        void HandleDeactivation(LightDivider lightDivider);
        void HandleBlockedInteraction();
        void HandleUnblockedInteraction();
        void HandleReactivation();
    }
}