using FiniteStateMachineBase.StateMachines;
using FiniteStateMachineBase.States;

namespace States.Player {
    public class PlayerMoving : EntityState {
        public PlayerMoving(IFiniteStateMachine<EntityState> stateMachine) : base(stateMachine) { }
        protected override void Reset() { }
    }
}