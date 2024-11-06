using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreEndGameState : IState<Spawner>
{
    public void OnEnter(Spawner spawner)
    {
        spawner.PreEndGameSpawn();
    }

    public void OnExecute(Spawner spawner)
    {
        
    }

    public void OnExit(Spawner t)
    {
        
    }
}
