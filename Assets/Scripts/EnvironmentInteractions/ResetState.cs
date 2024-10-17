using UnityEngine;

public class ResetState : EnvironmentInteractionState
{
    #region Variables

    // Float Variables
    private float weight, t;

    #endregion
    
    // Constructor
    public ResetState(EnvironmentInteractionContext context, 
        EnvironmentInteractionStateMachine.EEIS estate)
         : base(context, estate)
    {
        EnvironmentInteractionContext Context = context;
        LIK_DefaultPos = Context.GetLeftIKConstraint.data.target.transform.localPosition;
        RIK_DefaultPos = Context.GetRightIKConstraint.data.target.transform.localPosition;
    }

    #region Overrides

    public override void EnterState()
    {
        t = 0.0f;
    }
    public override void ExitState()
    {
        Context.CurrOtherCollider = null;
        Context.ClosestPointFromShoulder = Vector3.positiveInfinity;
        Context.GetLeftIKConstraint.data.target.transform.localPosition = LIK_DefaultPos;
        Context.GetRightIKConstraint.data.target.transform.localPosition = RIK_DefaultPos;
        Context.ResetRigWeight();
    }
    public override void UpdateState()
    {
        weight = Mathf.Lerp(1.0f, 0.0f, t);
        t += (2 * Time.deltaTime);
        Context.UpdateWeight(weight);
    }
    public override EnvironmentInteractionStateMachine.EEIS GetNextState()
    {
        if(t >= 1.0f) { return EnvironmentInteractionStateMachine.EEIS.Search; }
        return StateKey;
    }
    public override void OnTriggerEnter(Collider other){}
    public override void OnTriggerStay(Collider other){}
    public override void OnTriggerExit(Collider other){}

    #endregion Overrides
}
