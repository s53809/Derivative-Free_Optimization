using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneticAlgorithm : MonoBehaviour, ISimulate
{
    [SerializeField] private int _numberOfChild;

    public string Name { get; private set; }
    public List<int[]> arr;

    private void InputData()
    {
        arr = new List<int[]>();
        for (int i = 0; i < _numberOfChild; i++)
        {
            int[] array = new int[NodeManager.Instance.nodeCount];
            for (int j = 0; j < NodeManager.Instance.nodeCount; j++) array[j] = j;

            System.Random random = new System.Random();
            array = array.OrderBy(x => random.Next()).ToArray();
            arr.Add(array);
        }

        for (int i = 0; i < arr.Count; i++)
        {
            string str = "";
            for (int j = 0; j < arr[i].Length; j++)
            {
                str += arr[i][j].ToString() + " ";
            }
        }
    }

    private void Solve()
    {

    }

    public void SolveProblem()
    {
        InputData();
        Solve();
    }

    private void Awake()
    {
        Name = transform.name;
    }
}
