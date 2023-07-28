using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimulatedAnnealing : MonoBehaviour, ISimulate
{
    public string Name { get; private set; }

    private int[] _path;
    private int _pathSize;
    [SerializeField] private float criticalTemp = 0.2f;
    [SerializeField] private float reduceTemp = 0.995f;
    [SerializeField] private float K = 1; // 볼츠만 상수
    [SerializeField] private bool isSimulate = false;
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

    private void Awake()
    {
        Name = transform.name;
    }
    private void InputData()
    {
        _path = new int[NodeManager.Instance.nodeCount];
        _pathSize = _path.Length;

        System.Random random = new System.Random();

        for (int i = 0; i < _pathSize; i++) _path[i] = i;
        _path = _path.OrderBy(x => random.Next()).ToArray();

        UIManager.Instance.SetTitle("SimulatedAnnealing");
        RefreshUIInfo();
    }

    private void RefreshUIInfo()
    {
        string str = "";
        for (int i = 0; i < _path.Length; i++)
        {
            str += _path[i].ToString() + " ";
        }
        UIManager.Instance.SetInfo(str);
    }

    private void RefreshUIInfo(float T)
    {
        string str = $"Temperature : {T}\n";
        for (int i = 0; i < _path.Length; i++)
        {
            str += _path[i].ToString() + " ";
        }
        UIManager.Instance.SetInfo(str);
    }

    private void RefreshUIInfo(float T, float result)
    {
        string str = $"Distance : {result}\nTemperature : {T}\n";
        for (int i = 0; i < _path.Length; i++)
        {
            str += _path[i].ToString() + " ";
        }
        UIManager.Instance.SetInfo(str);
    }
    private void SA()
    {
        float T = 1; //온도
        float curE = 0; //현재 상태 에너지
        float newE = 0; //새로운 상태 에너지
        float p = 0; //확률
        int countGetsu = 0;
        while (T > criticalTemp)
        {
            countGetsu++;
            curE = GetDistance(_path);
            int[] newPath = new int[_pathSize];
            Array.Copy(_path, newPath, _pathSize); // Deep Copy
            Swap(newPath, UnityEngine.Random.Range(0, _pathSize),
                UnityEngine.Random.Range(0, _pathSize));
            newE = GetDistance(newPath);
            p = (float)Math.Clamp(Math.Exp((curE - newE) / (T * K)), 0f, 1f);

            if (p > UnityEngine.Random.Range(0f, 1f))
                _path = newPath;

            T *= reduceTemp;
        }
        Debug.Log(countGetsu);
        NodeManager.Instance.DrawLine(_path);
        RefreshUIInfo(T, GetDistance(_path));
    }
    public void SolveProblem()
    {
        InputData();
        if (isSimulate) StartCoroutine(SA_Simulate());
        else SA();
    }
    private void Swap(int[] arr, int a, int b)
    {
        int temp = arr[a];
        arr[a] = arr[b];
        arr[b] = temp;
    }

    IEnumerator SA_Simulate()
    {
        float T = 1; //온도
        float curE = 0; //현재 상태 에너지
        float newE = 0; //새로운 상태 에너지
        float p = 0; //확률
        while (T > criticalTemp)
        {
            curE = GetDistance(_path);
            int[] newPath = new int[_pathSize];
            Array.Copy(_path, newPath, _pathSize);
            Swap(newPath, UnityEngine.Random.Range(0, _pathSize), UnityEngine.Random.Range(0, _pathSize));
            newE = GetDistance(newPath);

            p = (float)Math.Clamp(Math.Exp((curE - newE) / (T * K)), 0f, 1f);

            if (p > UnityEngine.Random.Range(0f, 1f))
                _path = newPath;

            NodeManager.Instance.DrawLine(_path);
            RefreshUIInfo(T, GetDistance(_path));

            T *= reduceTemp;
            yield return new WaitForSeconds(0.005f);
        }
        yield return null;
    }
}