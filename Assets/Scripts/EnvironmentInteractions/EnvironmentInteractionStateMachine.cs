using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Assertions;

public class EnvironmentInteractionStateMachine : StateManager<EnvironmentInteractionStateMachine.EEIS>
{
    #region Variables
    
    // 
    public enum EEIS
    {
        Search,
        Approach,
        Rise,
        Touch,
        Reset,
    }

    private EnvironmentInteractionContext _context;

    #region Constraints
    
    [Header("Constraints")]
    [SerializeField] private TwoBoneIKConstraint _leftIkConstraint;
    [SerializeField] private TwoBoneIKConstraint _rightIkConstraint;
    [SerializeField] private MultiRotationConstraint _leftMRConstraint;
    [SerializeField] private MultiRotationConstraint _rightMRConstraint;

    #endregion Constraints

    [Space(20)]
    [SerializeField] private CharacterController _characterController;

    #endregion Variables

    #region Before First Frame

    void Awake()
    {
        ValidateConstraints();

        _context = new EnvironmentInteractionContext(_leftIkConstraint, 
            _rightIkConstraint, _leftMRConstraint, _rightMRConstraint, 
            _characterController, transform.root);

        InitializeStates();
        ConstructEnvironDetectCollider();
    }

    // Makes sure that each of the private variables has been initialized
    private void ValidateConstraints()
    {
        Assert.IsNotNull(_leftIkConstraint, "_leftIkConstraint is null");
        Assert.IsNotNull(_rightIkConstraint, "_rightIkConstraint is null");
        Assert.IsNotNull(_leftMRConstraint, "_leftMRConstraint is null");
        Assert.IsNotNull(_rightMRConstraint, "_rightMRConstraint is null");
        Assert.IsNotNull(_characterController, "_characterController is null");
    }

    // Adds States to inherited StateManager "States" dictionary and Sets Initial State
    private void InitializeStates()
    {
        States.Add(EEIS.Search, new SearchState(_context, EEIS.Search));
        States.Add(EEIS.Approach, new ApproachState(_context, EEIS.Approach));
        States.Add(EEIS.Rise, new RiseState(_context, EEIS.Rise));
        States.Add(EEIS.Touch, new TouchState(_context, EEIS.Touch));
        States.Add(EEIS.Reset, new ResetState(_context, EEIS.Reset));

        CurrentState = States[EEIS.Reset];
    }

    // Creates a Collider to detect Environmental Constructs that trigger different states
    private void ConstructEnvironDetectCollider()
    {
        float wingspan = _characterController.height;

        BoxCollider bColl = gameObject.AddComponent<BoxCollider>();
        bColl.size = new Vector3(wingspan, wingspan, wingspan);
        float yCenter = _characterController.center.y + (0.25f * wingspan);
        float zCenter = _characterController.center.z + (0.5f * wingspan);
        bColl.center = new Vector3(_characterController.center.x, yCenter, zCenter);
        bColl.isTrigger = true;
    }

    #endregion Before First Frame

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if(_context != null && _context.ClosestPointFromShoulder != null)
        {
            Gizmos.DrawSphere(_context.ClosestPointFromShoulder, 0.03f);
        }
    }
}
