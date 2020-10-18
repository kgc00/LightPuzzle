using FiniteStateMachineBase.StateMachines;

namespace FiniteStateMachineBase.States {
    public abstract class EntityState : IEntityState<EntityState> {
        public IFiniteStateMachine<EntityState> StateMachine { get; protected set; }

        protected EntityState(IFiniteStateMachine<EntityState> stateMachine) {
            StateMachine = stateMachine;
        }

        public virtual void Enter() { }

        public virtual void Update() { }

        public virtual void LateUpdate() { }

        public virtual void FixedUpdate() { }

        public virtual void Exit() => Reset();

        protected abstract void Reset();
    }
}