using System;
using System.Collections.Generic;
using FiniteStateMachineBase.States;
using UnityEngine;

namespace FiniteStateMachineBase.StateMachines {
    public abstract class FSMBehaviour : MonoBehaviour, IFiniteStateMachine<EntityState> {
        public abstract Dictionary<Type, IEntityState<EntityState>> States { get; protected set; }
        public IEntityState<EntityState> CurrentState { get; protected set; }
        public abstract void InitializeStates();

        public virtual void ChangeState(IEntityState<EntityState> newState) {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        protected virtual void Awake() => InitializeStates();

        protected void Start() { }
        protected void Update() => CurrentState.Update();

        protected void FixedUpdate() => CurrentState.FixedUpdate();

        protected void LateUpdate() => CurrentState.LateUpdate();
    }
}