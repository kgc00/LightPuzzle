using Models;

namespace System.Interfaces {
    public interface ILightInteractor {
        LightColor Color { get; }
        void Interact(ILightInteractable interactable);
    }
}