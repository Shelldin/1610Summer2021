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

    public bool isPressedUp, isPressedDown, isPressedConfirm;

    private int verticalMovement;

    public float yOffset = 64;

    //private Vector2 rectTransOffsetVector2,
        //alternateRectTransOffsetVector2;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        isPressedUp = isPressedDown = false;
        //rectTransOffsetVector2 = new Vector2(0, yOffset);
        //alternateRectTransOffsetVector2 = new Vector2(0, (maxMenuIndex-2) * yOffset);
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

        if (Input.GetAxis("Vertical") !=0 || verticalMovement != 0)
        {
            if (!keyDown)
            {
                if (Input.GetAxis("Vertical") < 0 || verticalMovement < 0)
                {
                    if (menuIndex < maxMenuIndex)
                    {
                        menuIndex++;
                        if (menuIndex > 1 && menuIndex < maxMenuIndex)
                        {
                            rectTransform.offsetMax -= new Vector2(0, -yOffset);
                        }
                    }
                    else
                    {
                        menuIndex = 0;
                        rectTransform.offsetMax = Vector2.zero;
                    }
                }
                else if (Input.GetAxis("Vertical") > 0 || verticalMovement > 0)
                {
                    if (menuIndex > 0)
                    {
                        menuIndex--;
                        if (menuIndex < maxMenuIndex - 1 && menuIndex > 0)
                        {
                            rectTransform.offsetMax -= new Vector2(0, yOffset);
                        }
                    }
                    else
                    {
                        menuIndex = maxMenuIndex;
                        rectTransform.offsetMax = new Vector2(0, (maxMenuIndex - 2) * yOffset);
                    }
                }
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
