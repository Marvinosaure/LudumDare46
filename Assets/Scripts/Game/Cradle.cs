using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cradle : MonoBehaviour
{
    [SerializeField] GameObject withBaby;

    public void SetBaby(bool activate)
    {
        withBaby.SetActive(activate);
    }
}
