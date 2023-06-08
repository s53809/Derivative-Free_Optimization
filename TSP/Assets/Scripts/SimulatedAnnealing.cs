using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimulatedAnnealing : MonoBehaviour, ISimulate
{
    private int[] _path;
    private int _pathSize;
    [SerializeField] private float criticalTemp = 0.2f;
    [SerializeField] private float reduceTemp = 0.995f;
    [SerializeField] private float K = 1; // 볼츠만 상수
    private float GetDistance(int[] path)
    {
        List<List<float>> list = NodeManager.Instance.node;

        float dist = 0;

        for (int i = 0; i < path.Length - 1; i++)
        {
            dist += list[path[i]][path[i + 1]];
        }
        dist += list[path[path.Length - 1]][path[0]];

        return dist;
    }
    private void InputData()
    {
        _path = new int[NodeManager.Instance.nodeCount];
        _pathSize = _path.Length;

        System.Random random = new System.Random();

        _path = _path.OrderBy(x => random.Next()).ToArray();
    }
    private void SA()
    {
        float T = 1; //온도
        float curE = 0; //현재 상태 에너지
        float newE = 0; //새로운 상태 에너지
        float p = 0; //확률
        while(T > criticalTemp)
        {
            curE = GetDistance(_path);
            System.Random random = new System.Random();
            int[] newPath = _path.OrderBy(x => random.Next()).ToArray();
            newE = GetDistance(newPath);
            p = (float)Math.Exp((newE - curE) / (T * K));
            Debug.Log($"{_path} : {curE}, {newPath} : {newE}, per = {p}");

            if (p > UnityEngine.Random.Range(0f, 1f))
            {
                Debug.Log("변화");
                _path = newPath;
            }

            T *= reduceTemp;
        }
    }
    public void SolveProblem()
    {
        InputData();
        SA();
    }
}