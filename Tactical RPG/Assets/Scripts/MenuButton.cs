using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// from https://pavcreations.com/scrollable-menu-in-unity-with-button-or-key-controller/#changing-position-on-list-with-keys
public class MenuButton : MonoBehaviour
{
    public MenuButtonController menuButtonController;
    public Animator animator;
    public int buttonIndex;
    [SerializeField] private GameObject menuPanelToOpen;

    private static bool isPressedAction = false;
    
    public BattleMenuOptions battleMenuOption;

    private void Start()
    {
        BattleStateSystem.instance.onBattleMenuSelectionCallback += TriggerAction;
    }

    void TriggerAction()
    {
        isPressedAction = true;
    }

    void Update()
    {
        if (menuButtonController.menuIndex == buttonIndex)
        {
            animator.SetBool("selected", true);
            if (isPressedAction)
            {
                animator.SetBool("pressed", true);
                //for opening a new panel
                if (menuPanelToOpen != null)
                {
                    menuButtonController.gameObject.SetActive(false);
                    menuPanelToOpen.SetActive(true);
                }

                isPressedAction = false;
            }
            else if (animator.GetBool("pressed"))
            {
                animator.SetBool("pressed", false);
            }
        }
        else
        {
           animator.SetBool("selected", false); 
        }
    } 
}
