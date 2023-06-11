using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISimulate
{
    public string Name { get; }
    public void SolveProblem();
}
