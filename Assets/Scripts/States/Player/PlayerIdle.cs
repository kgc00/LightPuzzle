using FiniteStateMachineBase.StateMachines;
using FiniteStateMachineBase.States;

namespace States.Player {
    public class PlayerIdle : EntityState {
        public PlayerIdle(IFiniteStateMachine<EntityState> stateMachine) : base(stateMachine) { }
        protected override void Reset() { }
    }
}