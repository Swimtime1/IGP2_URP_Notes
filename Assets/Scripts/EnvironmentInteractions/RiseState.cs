using UnityEngine;

public class RiseState : EnvironmentInteractionState
{
    // Constructor
    public RiseState(EnvironmentInteractionContext context, 
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
    public override void OnTriggerEnter(Collider other)
    {
        StartIkTargetPosTrack(other);
    }
    public override void OnTriggerStay(Collider other)
    {
        UpdateIkTargetPos(other);
    }
    public override void OnTriggerExit(Collider other)
    {
        ResetIkTargetPosTrack(other);
    }

    #endregion Overrides
}
