using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : GameUnit
{
    [SerializeField] private Rigidbody rb;

    public void AddForce(Vector3 force){
        rb.AddForce(force,ForceMode.Impulse);
    }

    public void OnDespawn(){
        SimplePool.Despawn(this);
    }

    public void Slice(){
        OnDespawn();
    }
}
