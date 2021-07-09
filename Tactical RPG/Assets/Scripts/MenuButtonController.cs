using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(RectTransform))]
//from PavCreations Tutorial https://pavcreations.com/scrollable-menu-in-unity-with-button-or-key-controller/#script-controlling-button-behaviour
public class MenuButtonController : MonoBehaviour
{
    public int menuIndex,
        maxMenuIndex;

    [SerializeField] private bool keyDown;
    [SerializeField] private RectTransform rectTransform;

    private bool isPressedUp, isPressedDown, isPressedConfirm;

    private int verticalMovement;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        isPressedUp = isPressedDown = false;
    }

    private void Update()
    {
        if (isPressedUp)
        {
            verticalMovement = 1;
        }

        if (isPressedDown)
        {
            verticalMovement = -1;
        }

        if (!isPressedUp && !isPressedDown)
        {
            verticalMovement = 0;
        }

        if (verticalMovement != 0)
        {
            if (!keyDown)
            {
                keyDown = true;
            }
        }
        else
        {
            keyDown = false;
        }
    }

    public void OnPressedUp()
    {
        isPressedUp = true;
    }

    public void OnReleaseUp()
    {
        isPressedUp = false;
    }

    public void OnPressedDown()
    {
        isPressedDown = true;
    }

    public void OnReleaseDown()
    {
        isPressedDown = false;
    }

    public void OnPressedConfirm()
    {
        isPressedConfirm = true;
    }

    public void OnReleaseConfirm()
    {
        isPressedConfirm = false;
    }

}
