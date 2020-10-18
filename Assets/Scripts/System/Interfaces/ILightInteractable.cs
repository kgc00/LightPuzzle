using System.Collections;

namespace System.Interfaces {
    public interface ILightInteractable {
        IEnumerator HandleInteraction(ILightInteractor interactor);
    }
}