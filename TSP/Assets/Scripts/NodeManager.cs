using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public List<List<float>> node { get; private set; }
    public int nodeCount = 0;

    private void Awake()
    {
        int i = 0;
        node = new List<List<float>>();
        foreach(var pos in this.gameObject.transform.GetComponentsInChildren<Transform>())
        {
            if (pos.gameObject.name == "map") continue;
            Debug.Log($"{i} : {pos.gameObject.name}");
            List<float> temp = new List<float>();
            foreach (var dist in this.gameObject.transform.GetComponentsInChildren<Transform>())
            {
                if (dist.gameObject.name == "map") continue;
                temp.Add(Vector3.Distance(pos.position, dist.position));
            }
            node.Add(temp);
            i++;
        }
        nodeCount = i;

        for (int k = 0; k < nodeCount; k++)
        {
            for (int j = 0; j < nodeCount; j++)
            {
                Debug.Log($"{k} -> {j} : {node[k][j]}");
            }
        }
    }
}
