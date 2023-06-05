using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    private void Awake()
    {
        foreach (var pos in this.gameObject.GetComponentsInParent<Vector3>())
        {
            
        }
    }
}
