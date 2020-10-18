using FiniteStateMachineBase.StateMachines;

namespace FiniteStateMachineBase.States {
    public interface IEntityState<T> where T : IEntityState<T> {
        IFiniteStateMachine<T> StateMachine { get; }
        void Enter();
        void Update();
        void LateUpdate();
        void FixedUpdate();
        void Exit();
    }
}