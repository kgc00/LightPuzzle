using System;
using System.Collections.Generic;
using FiniteStateMachineBase;
using FiniteStateMachineBase.StateMachines;
using FiniteStateMachineBase.States;
using States;
using States.Mirror;

namespace StateMachines.Mirror {
    public class MirrorFSM : FSMBehaviour {
        public override Dictionary<Type, IEntityState<EntityState>> States { get; protected set; }

        public override void InitializeStates() {
            States = new Dictionary<Type, IEntityState<EntityState>> {
                {typeof(MirrorIdle), new MirrorIdle(this)},
                {typeof(MirrorInteracting), new MirrorInteracting(this)}
            };
            CurrentState = States[typeof(MirrorIdle)];
        }
    }
}