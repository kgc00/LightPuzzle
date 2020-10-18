using Models;

namespace System.Interfaces {
    public interface IInteractionHistoryProvider {
        InteractionEvent TrackInteraction(IInteractionTracker tracker);
    }
}