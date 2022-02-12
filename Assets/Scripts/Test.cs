using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public void ChangeColor()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
