using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class DynamicProgramming : MonoBehaviour, ISimulate
{
    private class pair
    {
        public float dist;
        public int nextPos, nextState;

        public pair(float dist)
        {
            this.dist = dist;
            nextPos = -1;
            nextState = -1;
        }
    }

    public string Name => name;
    private List<List<pair>> _dp;
    private List<List<float>> _node;
    private int[] _path;
    private int _ansState;
    private int N;

    private void Awake()
    {
        name = transform.name;
    }

    private float DFS(int pos, int state, int index)
    {
        if (_node[pos][0] != 0 && state == _ansState) { return _node[pos][0]; }
        else if (_node[pos][0] == 0 && state == _ansState) return 1e9f;
        if (_dp[pos][state].dist != 0) return _dp[pos][state].dist;

        _dp[pos][state].dist = (float)1e9;
        for (int i = 0; i < N; i++)
        {
            if (_node[pos][i] != 0 && (state & (1 << i)) == 0)
            {
                float result = DFS(i, state | (1 << i), index + 1);
                if (_dp[pos][state].dist > _node[pos][i] + result)
                {
                    _dp[pos][state].dist = _node[pos][i] + result;
                    _dp[pos][state].nextPos = i;
                    _dp[pos][state].nextState = (state | (1 << i));
                    _path[index] = i;
                }
            }
        }
        return _dp[pos][state].dist;
    }

    private void Input()
    {
        N = NodeManager.Instance.node.Count;
        _dp = new List<List<pair>>(N);
        _node = NodeManager.Instance.node;
        _path = new int[N];
        for(int i = 0; i < N; i++)
        {
            _dp.Add(new List<pair>());
            for (int j = 0; j < (1 << N); j++)
            {
                _dp[i].Add(new pair(0));
            }
        }

        _ansState = (1 << N) - 1;

        UIManager.Instance.SetTitle("DynamicProgramming");
    }

    private void RefreshUIInfo(float ans, int[] path)
    {
        string str = $"shortest path = {ans}\n";
        for(int i = 0; i < path.Length; i++)
        {
            str += $"{path[i]} ";
        }
        UIManager.Instance.SetInfo(str);
    }

    private void Solve()
    {
        float ans = DFS(0, 1, 0);

        int[] path = new int[N];
        pair cur = _dp[0][1];
        path[0] = 0;
        for (int i = 1; cur.nextPos != -1; i++) 
        {
            path[i] = cur.nextPos;
            cur = _dp[cur.nextPos][cur.nextState];
        }

        RefreshUIInfo(ans, path);
        NodeManager.Instance.DrawLine(path);
    }
    public void SolveProblem()
    {
        Input();
        Solve();
    }
}
