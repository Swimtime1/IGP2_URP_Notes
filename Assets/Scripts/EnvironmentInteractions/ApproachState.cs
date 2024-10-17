using UnityEngine;

public class ApproachState : EnvironmentInteractionState
{
    #region Variables

    // Boolean Variables
    private bool surfaceExited;

    // Float Variables
    private float weight, t;

    #endregion
    
    // Constructor
    public ApproachState(EnvironmentInteractionContext context, 
        EnvironmentInteractionStateMachine.EEIS estate)
         : base(context, estate)
    {
        EnvironmentInteractionContext Context = context;
    }

    #region Overrides

    public override void EnterState()
    {
        surfaceExited = false;
        t = 0.0f;
    }
    public override void ExitState(){}
    public override void UpdateState()
    {
        weight = Mathf.Lerp(0.0f, 1.0f, t);
        t += (2 * Time.deltaTime);
    }
    public override EnvironmentInteractionStateMachine.EEIS GetNextState()
    {
        // resets if the trigger is exited
        if(surfaceExited) { return EnvironmentInteractionStateMachine.EEIS.Reset; }
        else if(t >= 1.0f) { return EnvironmentInteractionStateMachine.EEIS.Touch; }
        return StateKey;
    }
    public override void OnTriggerEnter(Collider other){}
    public override void OnTriggerStay(Collider other)
    {
        UpdateIkTargetPos(other);

        // updates the weight only if the contextual trigger hasn't been exited
        if(!surfaceExited) { Context.UpdateWeight(weight); }
    }
    public override void OnTriggerExit(Collider other)
    {
        surfaceExited = ResetIkTargetPosTrack(other);
    }

    #endregion Overrides
}
