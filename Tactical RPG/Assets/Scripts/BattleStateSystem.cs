using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

public enum BattleState {START, PLAYERTURN,TURNTRANSITION, ENEMYTURN, WON, LOST}

public class BattleStateSystem : MonoBehaviour
{

    public BattleState state;

    private Object[] statsSOArray;
    public StatsSO[] characterStatsArray;

    public TileMovement[] characterMovement;

    public GameObject activeUnit;
    //public TileMovement activeMovement;

    public float seconds = 2f;

    private WaitForSeconds wfs;

    
    
    
    
    


    private void Awake()
    {
        
        //agiComparer = new AgilityComparer();

        //From Unity Forums https://forum.unity.com/threads/how-to-properly-create-an-array-of-all-scriptableobjects-in-a-folder.794109/
        statsSOArray = Resources.LoadAll("ScriptableObject", typeof(StatsSO));
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

        state = BattleState.TURNTRANSITION;
        
        TurnTransition();
    }

    private void TurnTransition()
    {
        for (int i = 0; i < characterStatsArray.Length; i++)
        {
            if (characterStatsArray[i].hasHadTurn == false) 
            {
                activeUnit = GameObject.Find(characterStatsArray[i].characterObj.name+"(Clone)");
                break;
            }
        }
        
        if (characterStatsArray[characterStatsArray.Length-1].hasHadTurn)
        {
            for (int i = 0; i < characterStatsArray.Length; i++)
            {
                characterStatsArray[i].hasHadTurn = false;
            }
            TurnTransition();
        }

        if (activeUnit.CompareTag("Player"))
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
        
        else if (activeUnit.CompareTag("Enemy"))
        {
            state = BattleState.ENEMYTURN;
        }
    }

    private void PlayerTurn()
    {
        activeUnit.GetComponent<TileMovement>().enabled = true;
    }

    public void OnEndTurnButton()
    {
        if (state != BattleState.PLAYERTURN) 
            return;

        StartCoroutine(EndPlayerTurn());
    }

     IEnumerator EndPlayerTurn()
    {
        activeUnit.GetComponent<TileMovement>().enabled = false;
        state = BattleState.TURNTRANSITION;
        yield return wfs;

        for (int i = 0; i < characterStatsArray.Length; i++)
        {
            if (characterStatsArray[i].hasHadTurn == false)
            {
                characterStatsArray[i].hasHadTurn = true;
                break;
            }  
        }

        TurnTransition();
    }
     
     //from https://docs.microsoft.com/en-us/troubleshoot/dotnet/csharp/use-icomparable-icomparer
     private class AgilityComparer : IComparer
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

}
