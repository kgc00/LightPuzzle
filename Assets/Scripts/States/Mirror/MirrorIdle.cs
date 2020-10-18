using FiniteStateMachineBase.StateMachines;
using FiniteStateMachineBase.States;

namespace States.Mirror {
    public class MirrorIdle : EntityState {
        public MirrorIdle(IFiniteStateMachine<EntityState> stateMachine) : base(stateMachine) { }

        protected override void Reset() { }
    }
}