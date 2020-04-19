using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDeadBaby : MonoBehaviour
{
    public GameObject BabyDead;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(BabyDead,5.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
