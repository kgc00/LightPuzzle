using Models;

namespace System.Interfaces {
    public interface ILightColor {
        LightColor LightColor { get; }
        void UpdateLightColor(LightColor colorToProvide);
    }
}