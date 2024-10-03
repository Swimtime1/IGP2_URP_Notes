using UnityEngine;

public class TouchState : EnvironmentInteractionState
{
    // Constructor
    public TouchState(EnvironmentInteractionContext context, 
        EnvironmentInteractionStateMachine.EEIS estate)
         : base(context, estate)
    {
        EnvironmentInteractionContext Context = context;
    }

    #region Overrides

    public override void EnterState(){}
    public override void ExitState(){}
    public override void UpdateState(){}
    public override EnvironmentInteractionStateMachine.EEIS GetNextState()
    {
        return StateKey;
    }
    public override void OnTriggerEnter(Collider other){}
    public override void OnTriggerStay(Collider other){}
    public override void OnTriggerExit(Collider other){}

    #endregion Overrides
}
