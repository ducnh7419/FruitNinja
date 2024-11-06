using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FruitsSO", menuName = "ScriptableObjects/FruitsSO", order = 0)]
public class FruitsSO : ScriptableObject
{
  public List<Fruit> Fruits = new List<Fruit>();
  public Bomb bomb;
  public Fruit FrostFruit;
  public Fruit RainbowFruit;

  public Fruit pomeGranate;

  public Fruit GetRandomFruits()
  {
    return Fruits[Random.Range(0, Fruits.Count)];
  }

  public Bomb GetBomb()
  {
    return bomb;
  }

  public Fruit GetFrostFruit()
  {
    return FrostFruit;
  }

  public Fruit GetRainbowFruit()
  {
    return RainbowFruit;
  }

  public Fruit GetPomeGranate()
  {
    return pomeGranate;
  }

}
