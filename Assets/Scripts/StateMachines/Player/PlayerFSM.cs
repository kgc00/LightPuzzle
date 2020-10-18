using System;
using System.Collections.Generic;
using FiniteStateMachineBase.StateMachines;
using FiniteStateMachineBase.States;
using States.Player;

namespace StateMachines.Player {
    public class PlayerFSM : FSMBehaviour {
        public override Dictionary<Type, IEntityState<EntityState>> States { get; protected set; }
        public override void InitializeStates() {
            States = new Dictionary<Type, IEntityState<EntityState>> {
                {typeof(PlayerIdle), new PlayerIdle(this)},
                {typeof(PlayerMoving), new PlayerMoving(this)}
            };
            CurrentState = States[typeof(PlayerIdle)];
        }
    }
}