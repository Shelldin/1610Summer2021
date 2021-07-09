using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using Object = UnityEngine.Object;

public enum BattleState {START, PLAYERTURN,TURNTRANSITION, ENEMYTURN, WON, LOST}
public enum BattleMenuOptions {PANEL, ATTACK, MAGIC, ITEM, STAY} //from https://pavcreations.com/selecting-battle-targets-in-a-grid-based-game/


//CONTINUE FROM STEP 3 FOR TARGETING SELECTION TUTORIAL

public class BattleStateSystem : MonoBehaviour
{

    public BattleState battleState;
    public BattleMenuOptions lastBattleMenuOption;

    public bool hasClicked = false,
        isSelectionMode = false,
        onSelectionModeEnabled = false,
        keyDown = false;

    private Object[] statsSOArray;
    public StatsSO[] characterStatsArray;

    public TileMovement[] characterMovement;

    public List<GameObject> enemyList;

    public GameObject activeUnit;

    public GameObject enemy,
        lastEnemyChoice;
    //public TileMovement activeMovement;
    
    private ContactFilter2D contactFilter = new ContactFilter2D();
    private List<RaycastHit2D> results = new List<RaycastHit2D>();

    public LayerMask enemyLayer;

    public delegate void OnBattleMenuSelectionCallback();
    public OnBattleMenuSelectionCallback onBattleMenuSelectionCallback;

    public delegate void OnBattleSelectionModeConfirmCallback();
    public OnBattleSelectionModeConfirmCallback onBattleSelectionModeConfirmCallback;

    public float seconds = 2f;

    private WaitForSeconds wfs;

    
    
    
    
    


    private void Awake()
    {
        
        //agiComparer = new AgilityComparer();

        //From Unity Forums https://forum.unity.com/threads/how-to-properly-create-an-array-of-all-scriptableobjects-in-a-folder.794109/
        //finds all the scriptable objects for units that will be in a battle and places them into an array
        statsSOArray = Resources.LoadAll("ScriptableObject", typeof(StatsSO));
        characterStatsArray = new StatsSO[statsSOArray.Length];
        statsSOArray.CopyTo(characterStatsArray, 0);
        
        //instantiate the sprite and gameobject for a unit according to the unit prefab associated with the scriptableObjects
        for (int i = 0; i < characterStatsArray.Length; i++)
        {
            Instantiate(characterStatsArray[i].characterObj);
        }
        
        characterMovement = new TileMovement[characterStatsArray.Length];
        
    }

    void Start()
    {
        enemyList = new List<GameObject>();
        contactFilter.SetLayerMask(enemyLayer);
        contactFilter.useLayerMask = true;

        onBattleSelectionModeConfirmCallback += ConfirmSelectionModeChoice;


        wfs = new WaitForSeconds(seconds);
        //playerMovement = player.GetComponent(typeof(TileMovement)) as TileMovement;

        for (int i = 0; i < characterStatsArray.Length; i++)
        {
          characterMovement[i] = characterStatsArray[i].characterObj.GetComponent(typeof(TileMovement)) as TileMovement;
        }
        
        battleState = BattleState.START;
        StartCoroutine(SetupBattle());
    }
    
    //prepares the battle by sorting the units in battle according to their agility stat
    IEnumerator SetupBattle()
    {
        Array.Sort(characterStatsArray, new AgilityComparer());
        
        yield return wfs;

        battleState = BattleState.TURNTRANSITION;
        
        TurnTransition();
    }
    
    //changes state and determine which unit is next in battle based on array order and if they've already had a turn.
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
            battleState = BattleState.PLAYERTURN;
            PlayerTurn();
        }
        
        else if (activeUnit.CompareTag("Enemy"))
        {
            battleState = BattleState.ENEMYTURN;
        }
    }
    
    //enables the player to take actions with the active unit when the active unit is a player character.
    private void PlayerTurn()
    {
        activeUnit.GetComponent<TileMovement>().enabled = true;
    }
    
    //ends turn of current active unit
    public void OnEndTurnButton()
    {
        if (battleState != BattleState.PLAYERTURN) 
            return;

        StartCoroutine(EndPlayerTurn());
    }
    
    //when the player ends its turn marks the active unit as having had its turn and causes a turn transition to cycle to the next unit.
     IEnumerator EndPlayerTurn()
    {
        activeUnit.GetComponent<TileMovement>().enabled = false;
        battleState = BattleState.TURNTRANSITION;
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

     private void ConfirmSelectionModeChoice()
     {
         if (isSelectionMode)
         {
             onSelectionModeEnabled = false;
             isSelectionMode = false;
             hasClicked = true;

             if (lastBattleMenuOption == BattleMenuOptions.ATTACK)
             {
                 Attack();
             }
         }
     }

     private void Attack()
     {
         Debug.Log("Such a devastating attack!");
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
