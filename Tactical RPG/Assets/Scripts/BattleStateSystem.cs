﻿using System;
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
    

    public GameObject player;
    public TileMovement playerMovement;

    public float seconds = 2f;

    private WaitForSeconds wfs;

    


    private void Awake()
    {
        //From Unity Forums https://forum.unity.com/threads/how-to-properly-create-an-array-of-all-scriptableobjects-in-a-folder.794109/
        statsSOArray = Resources.FindObjectsOfTypeAll(typeof(StatsSO));
        characterStatsArray = new StatsSO[statsSOArray.Length];
        statsSOArray.CopyTo(characterStatsArray, 0);
        
        
        for (int i = 0; i < characterStatsArray.Length; i++)
        {
            Instantiate(characterStatsArray[i].characterObj);
        }
    }

    void Start()
    {
        
        wfs = new WaitForSeconds(seconds);
        playerMovement = player.GetComponent(typeof(TileMovement)) as TileMovement;
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        yield return wfs;

        state = BattleState.PLAYERTURN;
    }

    private void PlayerTurn()
    {
        playerMovement.enabled = true;
    }

    public void OnEndTurnButton()
    {
        if (state != BattleState.PLAYERTURN) 
            return;

        StartCoroutine(EndPlayerTurn());
    }

     IEnumerator EndPlayerTurn()
    {
        playerMovement.enabled = false;
        state = BattleState.ENEMYTURN;
        yield return wfs;
        Debug.Log("Player's turn has ended");
    }

}
