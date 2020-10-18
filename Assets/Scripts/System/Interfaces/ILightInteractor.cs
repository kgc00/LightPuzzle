using Models;
using UnityEngine;

namespace System.Interfaces {
    public interface ILightInteractor {
        GameObject Behaviour { get; }
        LightColor Color { get; }
        void Interact(ILightInteractable interactable);
    }
}