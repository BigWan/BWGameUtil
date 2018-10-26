using UnityEngine;
using System.Collections;

namespace BW.Core {

    public class SimpleState<TId> {

        public TId id;
        public StateFunc Enter, Update, Leave;

        public SimpleState(TId id, StateFunc enter, StateFunc update, StateFunc leave) {
            this.id = id;
            this.Enter = enter;
            this.Update = update;
            this.Leave = leave;
        }

    }

}