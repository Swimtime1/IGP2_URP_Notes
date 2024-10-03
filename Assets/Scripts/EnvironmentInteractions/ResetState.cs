using UnityEngine;

public class ResetState : EnvironmentInteractionState
{
    // Constructor
    public ResetState(EnvironmentInteractionContext context, 
        EnvironmentInteractionStateMachine.EEIS estate)
         : base(context, estate)
    {
        EnvironmentInteractionContext Context = context;
    }

    #region Overrides

    public override void EnterState()
    {
        Debug.Log("ENTERING RESET STATE");
    }
    public override void ExitState(){}
    public override void UpdateState()
    {
        Debug.Log("UPDATING RESET STATE");
    }
    public override EnvironmentInteractionStateMachine.EEIS GetNextState()
    {
        return StateKey;
    }
    public override void OnTriggerEnter(Collider other){}
    public override void OnTriggerStay(Collider other){}
    public override void OnTriggerExit(Collider other){}

    #endregion Overrides
}
