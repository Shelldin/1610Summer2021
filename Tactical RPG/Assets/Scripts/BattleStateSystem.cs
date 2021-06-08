using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleStateSystem : MonoBehaviour
{
    public BattleState state;

    public GameObject player;
    public TileMovement playerMovement;

    public float seconds = 2f;

    private WaitForSeconds wfs;

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
