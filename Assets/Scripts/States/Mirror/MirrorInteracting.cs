using FiniteStateMachineBase.StateMachines;
using FiniteStateMachineBase.States;

namespace States.Mirror {
    public class MirrorInteracting : EntityState {
        public MirrorInteracting(IFiniteStateMachine<EntityState> stateMachine) : base(stateMachine) { }
        protected override void Reset() { }
    }
}