using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SimulateManager : MonoBehaviour
{
    private Dictionary<string, ISimulate> _arr;
    [SerializeField] private string _name;
    void Start()
    {
        _arr = new Dictionary<string, ISimulate>();
        foreach (var item in gameObject.GetComponentsInChildren<ISimulate>())
            _arr.Add(item.Name, item);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!_arr.ContainsKey(_name)) Debug.Log($"{_name}은 없는 이름입니다.");
            else _arr[_name].SolveProblem();
        }
    }
}
