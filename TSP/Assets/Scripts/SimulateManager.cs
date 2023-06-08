using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SimulateManager : MonoBehaviour
{
    private List<ISimulate> _arr;
    void Awake()
    {
        _arr = new List<ISimulate>();
        foreach (var item in gameObject.GetComponentsInChildren<ISimulate>())
        {
            _arr.Add(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            for(int i = 0; i < _arr.Count; i++)
            {
                _arr[i].SolveProblem();
            }
        }
    }
}
