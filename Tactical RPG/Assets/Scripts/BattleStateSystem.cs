using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleStateSystem : MonoBehaviour
{

    public BattleState state;

    private Object[] statsSOArray;
    public StatsSO[] characterStatsArray;

    public TileMovement[] characterMovement;

    //public GameObject player;
    //public TileMovement playerMovement;

    public float seconds = 2f;

    private WaitForSeconds wfs;

    //private AgilityComparer agiComparer;
    
    
    //from https://docs.microsoft.com/en-us/troubleshoot/dotnet/csharp/use-icomparable-icomparer
    public class AgilityComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            StatsSO s1 = (StatsSO) x;
            StatsSO s2 = (StatsSO) y;

            if (s1.agility < s2.agility)
                return 1;

            if (s1.agility > s2.agility)
                return -1;

            else
                return 0;
        }
    }
    


    private void Awake()
    {
        
        //agiComparer = new AgilityComparer();

        //From Unity Forums https://forum.unity.com/threads/how-to-properly-create-an-array-of-all-scriptableobjects-in-a-folder.794109/
        statsSOArray = Resources.FindObjectsOfTypeAll(typeof(StatsSO));
        characterStatsArray = new StatsSO[statsSOArray.Length];
        statsSOArray.CopyTo(characterStatsArray, 0);
        
        
        for (int i = 0; i < characterStatsArray.Length; i++)
        {
            Instantiate(characterStatsArray[i].characterObj);
        }
        
        characterMovement = new TileMovement[characterStatsArray.Length];
        
    }

    void Start()
    {
        
        wfs = new WaitForSeconds(seconds);
        //playerMovement = player.GetComponent(typeof(TileMovement)) as TileMovement;

        for (int i = 0; i < characterStatsArray.Length; i++)
        {
          characterMovement[i] = characterStatsArray[i].characterObj.GetComponent(typeof(TileMovement)) as TileMovement;
        }
        
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        Array.Sort(characterStatsArray, new AgilityComparer());
        
        yield return wfs;

        state = BattleState.PLAYERTURN;
    }

    private void PlayerTurn()
    {
        characterMovement[0].enabled = true;
    }

    public void OnEndTurnButton()
    {
        if (state != BattleState.PLAYERTURN) 
            return;

        StartCoroutine(EndPlayerTurn());
    }

     IEnumerator EndPlayerTurn()
    {
        characterMovement[0].enabled = false;
        state = BattleState.ENEMYTURN;
        yield return wfs;
        Debug.Log("Player's turn has ended");
    }

}
