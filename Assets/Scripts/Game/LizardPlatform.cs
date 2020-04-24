using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, 9, true);
    }    

    // Update is called once per frame
    void Update()
    {
        
    }
}
