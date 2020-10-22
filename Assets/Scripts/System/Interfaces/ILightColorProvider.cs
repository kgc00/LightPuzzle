using Models;

namespace System.Interfaces {
    public interface ILightColorProvider {
        void SetLightColor(ILightColor light);
        LightColor ColorToProvide { get; }
    }
}