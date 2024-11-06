using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitJuice : GameUnit
{
    public SpriteRenderer spriteRenderer;
    public void OnInit(SpriteRenderer spriteRenderer){
        this.spriteRenderer.sprite=spriteRenderer.sprite;
    }

    public void OnDespawn(){
        SimplePool.Despawn(this);
    }

}
