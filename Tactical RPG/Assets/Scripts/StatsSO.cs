using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class StatsSO : ScriptableObject
{

    public GameObject characterObj;
    public int lvl,
        hp,
        mp,
        exp,
        attack,
        defense,
        agility,
        move;

    public bool hasHadTurn = false;


}
