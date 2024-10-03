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

    [Space(20)]
    [SerializeField] private CharacterController _characterController;

    #endregion Constraints

    #endregion Variables

    #region Before First Frame

    void Awake()
    {
        ValidateConstraints();

        _context = new EnvironmentInteractionContext(_leftIkConstraint, 
            _rightIkConstraint, _leftMRConstraint, _rightMRConstraint, 
            _characterController);

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
        float yCenter = bColl.center.y + (0.5f * wingspan);
        float zCenter = bColl.center.z + (0.5f * wingspan);
        bColl.center = new Vector3(bColl.center.x, yCenter, zCenter);
        bColl.isTrigger = true;
    }

    #endregion Before First Frame
}