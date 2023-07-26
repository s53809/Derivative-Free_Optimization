using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneticAlgorithm : MonoBehaviour, ISimulate
{
    [SerializeField] private int _numberOfChild;
    [SerializeField] private int _numberOfIterations;
    [SerializeField] private int _mutationChance;

    public string Name { get; private set; }
    public List<int[]> arr;

    private void InputData()
    {
        UIManager.Instance.SetTitle("GeneticAlgorithm");
        arr = new List<int[]>();
        for (int i = 0; i < _numberOfChild; i++)
        {
            int[] array = new int[NodeManager.Instance.nodeCount];
            for (int j = 0; j < NodeManager.Instance.nodeCount; j++) array[j] = j;

            System.Random random = new System.Random();
            array = array.OrderBy(x => random.Next()).ToArray();
            arr.Add(array);
        }
    }

    private void ReFreshUIInfo(int[] path)
    {
        string str = "Best Direction: ";
        for(int i = 0; i < path.Length; i++)
        {
            str += $"{path[i]} ";
        }
        str += "\n";

        UIManager.Instance.SetInfo(str);

        str = "childInfo\n";

        for(int i = 0; i < arr.Count; i++)
        {
            for(int j = 0; j < arr[i].Length; j++)
            {
                str += $"{arr[i][j]} ";
            }
            str += "\n";
        }
        UIManager.Instance.SetDebug(str);
    }

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

    private void PrintDebugLogArr(int[] ary, string st = "")
    {
        string str = st + " ";
        for(int i = 0; i < ary.Length; i++)
        {
            str += $"{ary[i]} ";
        }
        Debug.Log(str);
    }

    private float GetWeight(int[] path)
    {
        return Mathf.Pow(1000 / GetDistance(path), 2);
    }

    private void Solve()
    {
        WeightedRandom<int[]> wr = new WeightedRandom<int[]>();
        List<int[]> newArr = new List<int[]>();
        for(int i = 0; i < arr.Count; i++)
            wr.InputObject(arr[i], GetWeight(arr[i]));

        for (int i = 0; i < _numberOfChild; i++)
        {
            int[] visited = new int[NodeManager.Instance.nodeCount + 1];
            int[] firstPath = wr.GetRandomOutput();
            int[] secondPath = wr.GetRandomOutput();

            int[] finalPath = new int[NodeManager.Instance.nodeCount];
            int randomIndex = Random.Range(1, NodeManager.Instance.nodeCount);

            for(int j = 0; j < randomIndex; j++)
            {
                finalPath[j] = firstPath[j];
                visited[firstPath[j]] = 1;
            }

            for (int j = 0, k = randomIndex; j < NodeManager.Instance.nodeCount; j++)
            {
                if (visited[secondPath[j]] == 1) continue;
                finalPath[k++] = secondPath[j];
            }

            if (Random.Range(0, 100) < _mutationChance)
            {
                int firstIndex = Random.Range(0, NodeManager.Instance.nodeCount);
                int secondIndex = Random.Range(0, NodeManager.Instance.nodeCount);

                int temp = finalPath[firstIndex];
                finalPath[firstIndex] = finalPath[secondIndex];
                finalPath[secondIndex] = temp;
            }

            newArr.Add(finalPath);
        }

        arr = newArr.ConvertAll(s => s);
    }

    private void Show()
    {
        int index = 0;
        float dist = GetDistance(arr[0]);
        for(int i = 1; i < arr.Count; i++)
        {
            float dir = GetDistance(arr[i]);
            if (dist > dir)
            {
                dist = dir;
                index = i;
            }
        }

        NodeManager.Instance.DrawLine(arr[index]);
        ReFreshUIInfo(arr[index]);
    }

    public void SolveProblem()
    {
        InputData();
        for(int i = 0; i < _numberOfIterations; i++)
        {
            Solve();
        }
        Show();
    }

    private void Awake()
    {
        Name = transform.name;
    }
}
