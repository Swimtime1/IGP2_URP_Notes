using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKBoughController : MonoBehaviour
{
    #region Variables

    [Header("Locations")]
    [SerializeField] private Vector3 initPos;
    [SerializeField] private Vector3 awayPos;

    #endregion Variables
    
    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
        awayPos = new Vector3(initPos.x, initPos.y + 5, initPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
