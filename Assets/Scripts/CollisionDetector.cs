using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    #region Variables

    // Material Variables
    [SerializeField] private Material dissolveMat;

    // Boolean Variables
    [SerializeField] private bool cubeChanged;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        cubeChanged = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Changes the cube to a pyramid
        if((other.gameObject.CompareTag("Dissolve")) && (!cubeChanged))
        {
            cubeChanged = true;
            StartCoroutine(Dissolve());
        }
    }

    #region Coroutines

    // Dissolves anything with the dissolveMat Material, changes it to a 
    // pyramid, and undissolves it
    private IEnumerator Dissolve()
    {
        // dissolves
        for(float i = 0f; i <= 1.0f; i += 0.1f)
        {
            dissolveMat.SetFloat("_Alpha_Clip_Threshold", i);
            yield return new WaitForSeconds(0.1f);
        }

        // change to pyramid

        // undissolves
        for(float i = 1.0f; i >= 0.0f; i -= 0.1f)
        {
            dissolveMat.SetFloat("_Alpha_Clip_Threshold", i);
            yield return new WaitForSeconds(0.1f);
        }
        dissolveMat.SetFloat("_Alpha_Clip_Threshold", 0.0f);
    }

    #endregion
}
