using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeMatch : MonoBehaviour {

    bool playerAction;
    private bool selected = false;
    private int allowedToTake = 3;

    Color32 hoverColor = new Color32(255, 25, 10, 1);
    Color32 clickColor = new Color32(100, 100, 100, 1);


    void Update() {

        playerAction = GameObject.Find("Main").GetComponent<Main>().playerAction;           // checking if Player input is allowed

        if (GameObject.Find("Main").GetComponent<Main>().matchCount <= 3)                   // if less than 3 matches left, ensure that one match remains after the players turn
            allowedToTake = GameObject.Find("Main").GetComponent<Main>().matchCount - 1;
    }


    //-------------------------------- Mouseover color change effect -----------------------------------

    void OnMouseOver() {

        if (playerAction == true)
        {
            gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", hoverColor);
            gameObject.transform.FindChild("Tip").GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            gameObject.transform.FindChild("Tip").GetComponent<Renderer>().material.SetColor("_EmissionColor", hoverColor);
        }
    }


    void OnMouseExit()
    {
        if (selected == false)
        {
            gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
            gameObject.transform.FindChild("Tip").GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            gameObject.transform.FindChild("Tip").GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
        }
    }


    //-------------------------------- When Match is clicked -----------------------------------

    void OnMouseDown(){

        if (playerAction == true)
        {
            GameObject.Find("Menu").GetComponent<MenuSounds>().playButtonUp();

            if (selected == false)                                                                              // if clicked Match is not already selected
                {
                    if (GameObject.Find("Main").GetComponent<Main>().selectCount < allowedToTake)               //prevent player from selecting more matches than allowed
                        
                    {
                        selected = true;                                                                        // clicked Match is now selected
                        GameObject.Find("Main").GetComponent<Main>().selectedMatches.Add(gameObject);           // add Match to List of selected Matches
                        GameObject.Find("Main").GetComponent<Main>().selectCount++;                             // increase number of selected Matches by one
                    }
                }

                else
                {
                    selected = false;                                                                           // clicked Match is now deselected
                    GameObject.Find("Main").GetComponent<Main>().selectedMatches.Remove(gameObject);                // remove Match from List of selected Matches
                    GameObject.Find("Main").GetComponent<Main>().selectCount--;                                 // decrease number of selected Matches by one
                }  
        }
    }


}