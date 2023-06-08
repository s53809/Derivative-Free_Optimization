using System;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    private static NodeManager _instance;

    public static NodeManager Instance
    {
        get
        {
            try
            {
                return _instance;
            }
            catch (NullReferenceException ex)
            {
                Debug.LogError($"NodeManager.cs : {ex.Message}");
                return null;
            }
        }
    }

    public List<List<float>> node { get; private set; }
    public int nodeCount = 0;
    private void Awake()
    {
        _instance = this;
        
        int i = 0;
        node = new List<List<float>>();
        foreach(var pos in gameObject.transform.GetComponentsInChildren<Transform>())
        {
            if (pos.gameObject.name == "map") continue;
            Debug.Log($"{i} : {pos.gameObject.name}");
            List<float> temp = new List<float>();
            foreach (var dist in gameObject.transform.GetComponentsInChildren<Transform>())
            {
                if (dist.gameObject.name == "map") continue;
                temp.Add(Vector3.Distance(pos.position, dist.position));
            }
            node.Add(temp);
            i++;
        }
        nodeCount = i;
    }
}
