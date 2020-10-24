using System.Interactions;
using Models;
using UnityEngine;

namespace System.Interfaces {
    public interface ILightInteractor {
        GameObject Behaviour { get; }
        void Interact(ILightInteractable interactable);
        void HandleDeactivation(LightDivider lightDivider);
    }
}