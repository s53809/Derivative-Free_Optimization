using System;
using System.Collections.Generic;
using UnityEngine;

public class WeightedRandom<T>
{
    private Dictionary<T, float> m_list;
    private float m_sumOfAllWeights;
    public WeightedRandom()
    {
        m_list = new Dictionary<T, float>();
        m_sumOfAllWeights = 0;
    }

    public void InputObject(T obj, float weight)
    {
        m_list.Add(obj, weight);
        m_sumOfAllWeights += weight;
    }

    public void DeleteObject(T obj)
    {
        if (m_list.ContainsKey(obj))
            m_list.Remove(obj);
        else
            throw new Exception($"no key({obj}) in WeightedRandom's List");
    }

    public T GetRandomOutput()
    {
        float index = UnityEngine.Random.Range(0, m_sumOfAllWeights);
        foreach(KeyValuePair<T, float> temp in m_list)
        {
            if (index <= temp.Value) return temp.Key;
            else index -= temp.Value;
        }
        throw new Exception("this can't be happen");
    }
}
