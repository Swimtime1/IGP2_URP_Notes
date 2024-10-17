using UnityEngine;

public class SearchState : EnvironmentInteractionState
{
    #region Variables

    // Boolean Variables
    private bool surfaceFound;

    #endregion
    
    // Constructor
    public SearchState(EnvironmentInteractionContext context, 
        EnvironmentInteractionStateMachine.EEIS estate)
         : base(context, estate)
    {
        EnvironmentInteractionContext Context = context;
    }

    #region Overrides

    public override void EnterState(){ surfaceFound = false; }
    public override void ExitState(){}
    public override void UpdateState(){}
    public override EnvironmentInteractionStateMachine.EEIS GetNextState()
    {
        if(surfaceFound) { return EnvironmentInteractionStateMachine.EEIS.Approach; }
        return StateKey;
    }
    public override void OnTriggerEnter(Collider other)
    {
        StartIkTargetPosTrack(other);
        surfaceFound = true;
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
