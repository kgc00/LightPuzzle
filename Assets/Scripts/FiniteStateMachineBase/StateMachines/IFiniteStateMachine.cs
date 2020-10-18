using System;
using System.Collections.Generic;
using FiniteStateMachineBase.States;

namespace FiniteStateMachineBase.StateMachines {
    public interface IFiniteStateMachine<T> where T : IEntityState<T> {
        Dictionary<Type, IEntityState<T>> States { get; }
        IEntityState<T> CurrentState { get; }

        void InitializeStates();

        void ChangeState(IEntityState<T> newState);
    }
}