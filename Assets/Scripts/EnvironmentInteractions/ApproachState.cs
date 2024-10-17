using UnityEngine;

public class ApproachState : EnvironmentInteractionState
{
    #region Variables

    // Boolean Variables
    private bool surfaceExited;

    // Float Variables
    private float weight, t;

    // Vector3 Variables
    private Vector3 shoulderPos, ikPos;

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
        // maxes the rig weight to 50%
        weight = Mathf.Lerp(0.0f, 0.5f, t);
        t += (Time.deltaTime);

        shoulderPos = Context.CurrShoulderTransform.position;
        ikPos = Context.CurrIkTargetTransform.position;
    }
    public override EnvironmentInteractionStateMachine.EEIS GetNextState()
    {
        // resets if the trigger is exited
        if(surfaceExited) { return EnvironmentInteractionStateMachine.EEIS.Reset; }
        else if((Vector3.Distance(shoulderPos, ikPos) < (Context.Wingspan / 2))
                && (t >= 0.5f))
        { return EnvironmentInteractionStateMachine.EEIS.Rise; }
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
