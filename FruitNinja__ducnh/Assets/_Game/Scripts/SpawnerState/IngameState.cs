using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameState : IState<Spawner>
{
    public void OnEnter(Spawner t)
    {
        
    }

    public void OnExecute(Spawner spawner)
    {
        spawner.InGameSpawn();
    }

    public void OnExit(Spawner t)
    {
       
    }
}
