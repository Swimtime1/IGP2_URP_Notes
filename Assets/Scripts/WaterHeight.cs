using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHeight : MonoBehaviour
{
    #region Variables

    // Transform Variables
    [SerializeField] private Transform player;

    // Float Variables
    private float xPos, zPos;
    private float yDiff;

    #endregion Variables
    
    // Start is called before the first frame update
    void Start()
    {
        xPos = transform.position.x;
        zPos = transform.position.z;
        yDiff = player.position.y - transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(xPos, (player.position.y - yDiff), zPos);
    }
}
