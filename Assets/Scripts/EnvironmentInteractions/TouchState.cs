using UnityEngine;

public class TouchState : EnvironmentInteractionState
{
    #region Variables

    // Boolean Variables
    private bool surfaceExited;

    #endregion
    
    // Constructor
    public TouchState(EnvironmentInteractionContext context, 
        EnvironmentInteractionStateMachine.EEIS estate)
         : base(context, estate)
    {
        EnvironmentInteractionContext Context = context;
    }

    #region Overrides

    public override void EnterState()
    {
        surfaceExited = false;
    }
    public override void ExitState(){}
    public override void UpdateState(){}
    public override EnvironmentInteractionStateMachine.EEIS GetNextState()
    {
        // resets if the trigger is exited
        if(surfaceExited) { return EnvironmentInteractionStateMachine.EEIS.Reset; }
        return StateKey;
    }
    public override void OnTriggerEnter(Collider other){}
    public override void OnTriggerStay(Collider other)
    {
        UpdateIkTargetPos(other);
    }
    public override void OnTriggerExit(Collider other)
    {
        surfaceExited = ResetIkTargetPosTrack(other);
    }

    #endregion Overrides
}
