using System;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    private static NodeManager _instance;
    private LineRenderer _lineRenderer;
    private Transform[] _childTransforms;

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
        _childTransforms = gameObject.transform.GetComponentsInChildren<Transform>();
        _lineRenderer = gameObject.transform.GetComponent<LineRenderer>();

        foreach (var pos in _childTransforms)
        {
            if (pos.gameObject.name == "map") continue;
            List<float> temp = new List<float>();
            foreach (var dist in _childTransforms)
            {
                if (dist.gameObject.name == "map") continue;
                temp.Add(Vector3.Distance(pos.position, dist.position));
            }
            node.Add(temp);
            i++;
        }
        nodeCount = i;
    }

    public void DrawLine(int[] arr)
    {
        _lineRenderer.positionCount = arr.Length + 1;
        for(int i = 0; i < arr.Length; i++)
        {
            _lineRenderer.SetPosition(i, _childTransforms[arr[i] + 1].position);
        }
        _lineRenderer.SetPosition(arr.Length, _childTransforms[arr[0] + 1].position);
    }
}
