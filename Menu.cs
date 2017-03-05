using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    int noOfMatches = 10;
    int difficulty = 1;
    int firstTurn = 0;
    public Text noOfMatchesText;
    public Text difficultyText;
    public Text firstTurnText;
    public Text P1NameText;
    public InputField P1NameInput;
    public InputField P2NameInput;

    public string player1Name;
    public string player2Name;
   
    public Text matchesLeft;
    public Text turn;

    public Text numberList;
    public Text nameList;
    public Text difficultyList;
    public Text winStreakList;
    public Text scoreList;

    public Text resultText;
    public Text ScoreName;
    public Text Scores;

    public class Highscore
    {
        public string name;
        public string difficulty;
        public int winStreak;
        public int score;

        public Highscore(string na, string dif, int str, int sco)
        {
            name = na;
            difficulty = dif;
            winStreak = str;
            score = sco;
        }
    }

    public List<Highscore> HighscoreList = new List<Highscore>();


    void Start()
    {   
        //--- show Main Menu - hide everything else
        transform.FindChild("Main Menu").gameObject.SetActive(true);
        transform.FindChild("Game Menu").gameObject.SetActive(false);
        transform.FindChild("GameOverScreen").gameObject.SetActive(false);
        transform.FindChild("HUD").gameObject.SetActive(false);
        transform.FindChild("ScoreBoard").gameObject.SetActive(false);
        //---

        player1Name = "Player 1";                                                               // Set Player names to defaults
        player2Name = "Player 2";
    }



    //------------------------------------ HUD ------------------------------------------

    void Update()
    {
        matchesLeft.text = "Matches left: " + GameObject.Find("Main").GetComponent<Main>().matchCount.ToString();

        if (GameObject.Find("Main").GetComponent<Main>().playerNo == 0)
            turn.text = "Computer's turn";
        if (GameObject.Find("Main").GetComponent<Main>().playerNo == 1)
            turn.text = player1Name + "'s turn";
        if (GameObject.Find("Main").GetComponent<Main>().playerNo == 2)
            turn.text = player2Name + "'s turn";
    }



    //------------------------------------ Main Menu -----------------------------------

    public void singleplayer()
    {
        GameObject.Find("Main").GetComponent<Main>().multiplayer = false;

        //--- hide Buttons for Multiplayer Mode on Game Menu
        transform.FindChild("Game Menu").FindChild("P2NameText").gameObject.SetActive(false);
        transform.FindChild("Game Menu").FindChild("P2NameInputField").gameObject.SetActive(false);
        transform.FindChild("ScoreBoard").gameObject.SetActive(false);                                   // hide Scoreboard if open
        //---

        //--- show Buttons for Singleplayer Mode on Game Menu
        transform.FindChild("Game Menu").FindChild("DifficultyText").gameObject.SetActive(true);
        transform.FindChild("Game Menu").FindChild("DiffIncButton").gameObject.SetActive(true);
        transform.FindChild("Game Menu").FindChild("DiffDecButton").gameObject.SetActive(true);
        transform.FindChild("Game Menu").FindChild("Text 2").gameObject.SetActive(true);
        transform.FindChild("Game Menu").FindChild("FirstTurnText").gameObject.SetActive(true);
        transform.FindChild("Game Menu").FindChild("FTIncButton").gameObject.SetActive(true);
        transform.FindChild("Game Menu").FindChild("FTDecButton").gameObject.SetActive(true);
        transform.FindChild("Game Menu").FindChild("Text 3").gameObject.SetActive(true);
        //---

        P1NameText.text = "Player name:";                                           // Change Text Field from "Player 1 name" to "Player name"

        if (player1Name == "Player 1")                                              // Change Player's default name from "Player 1" to "Player"
            player1Name = "Player";

        
        gameMenu();
    }

    public void multiplayer()
    {
        GameObject.Find("Main").GetComponent<Main>().multiplayer = true;

        //--- hide Buttons for Singleplayer Mode on Game Menu
        transform.FindChild("Game Menu").FindChild("DifficultyText").gameObject.SetActive(false);
        transform.FindChild("Game Menu").FindChild("DiffIncButton").gameObject.SetActive(false);
        transform.FindChild("Game Menu").FindChild("DiffDecButton").gameObject.SetActive(false);
        transform.FindChild("Game Menu").FindChild("Text 2").gameObject.SetActive(false);
        transform.FindChild("Game Menu").FindChild("FirstTurnText").gameObject.SetActive(false);
        transform.FindChild("Game Menu").FindChild("FTIncButton").gameObject.SetActive(false);
        transform.FindChild("Game Menu").FindChild("FTDecButton").gameObject.SetActive(false);
        transform.FindChild("Game Menu").FindChild("Text 3").gameObject.SetActive(false);
        transform.FindChild("ScoreBoard").gameObject.SetActive(false);                          // hide Scoreboard if open
        //---

        //--- show Buttons for Multiplayer Mode on Game Menu
        transform.FindChild("Game Menu").FindChild("P2NameText").gameObject.SetActive(true);
        transform.FindChild("Game Menu").FindChild("P2NameInputField").gameObject.SetActive(true);
        //---

        P1NameText.text = "Player 1 name:";                                         // Change Text Field from "Player name" to "Player 1 name"
        if (player1Name == "Player")                                                // Change Player 1's default name from "Player" to "Player 1"
            player1Name = "Player 1";
        
        gameMenu();
    }


    public void gameMenu()
    {
        //--- show Game Menu - hide Main Menu
        transform.FindChild("Main Menu").gameObject.SetActive(false);
        transform.FindChild("Game Menu").gameObject.SetActive(true);
        //---

        gameObject.GetComponent<MenuSounds>().playButtonUp();
        GameObject.Find("Main").GetComponent<Main>().placeMatches();         // place a set of Matches
    }


    public void quit()
    {
        Application.Quit();
    }



    //------------------------------------ Show Score Board -----------------------------------

    public void scoreBoard()
    {
        gameObject.GetComponent<MenuSounds>().playButtonUp();

        //--- toggle Scoreboard window
        if (transform.FindChild("ScoreBoard").gameObject.activeSelf == false)
            transform.FindChild("ScoreBoard").gameObject.SetActive(true);
        else
            transform.FindChild("ScoreBoard").gameObject.SetActive(false);
        //---


        //--- clear Text Fields
        numberList.text = "";
        nameList.text = "";
        difficultyList.text = "";
        winStreakList.text = "";
        scoreList.text = "";
        //---

        HighscoreList.Sort((b, a) => (a.score.CompareTo(b.score)));         // sort List of Highscores by score

        foreach (Highscore score in HighscoreList)                          // output List of Highscores to Textfields
        {
            numberList.text = numberList.text + (HighscoreList.IndexOf(score) +1).ToString() + "\n";
            nameList.text = nameList.text + score.name + "\n";
            difficultyList.text = difficultyList.text + score.difficulty + "\n";
            winStreakList.text = winStreakList.text + score.winStreak + "\n";
            scoreList.text = scoreList.text + score.score.ToString() + "\n";
        }
    }


    //------------------------------------ Add new Highscore -----------------------------------

    public void addHighscore(string na, string dif, int str, int sco)
    {
        string name = na;
        string difficulty = dif;
        int winStreak = str;
        int score = sco;

        if (HighscoreList.Exists(x => x.name == name) == true)                         // if the name already exists in the Highscore List:
        {
            Highscore oldScore = HighscoreList.Find(x => x.name.Contains(name));
            if (oldScore.score < score)                                                // and the old score is less than the new score
            {
                HighscoreList.Remove(oldScore);                                        // delete old Highscore
                HighscoreList.Add(new Highscore(name, difficulty, winStreak, score));  // and add a new one
            }                
        }
        else                                                                           // if the name does not already exist:
            HighscoreList.Add(new Highscore(name, difficulty, winStreak, score));      // add a new Highscore

                                                                                    
    }



    //---------------------------- Clear List of Highscores ----------------------------

    public void clearScores()
    {
        HighscoreList.Clear();
        transform.FindChild("ScoreBoard").gameObject.SetActive(false);
        scoreBoard();
    }

    


    //------------------------------------ Game Menu -----------------------------------

    public void matchesInc()                                                                // action for Button to increase number of Matches
    {
        if (noOfMatches >= 10)                                                              // range between 10 and 15
            if (noOfMatches < 15)
            {
                gameObject.GetComponent<MenuSounds>().playButtonUp();
                noOfMatches++;
                noOfMatchesText.text = noOfMatches.ToString();
                GameObject.Find("Main").GetComponent<Main>().matchesAtStart++;              // increase number of Matches
                GameObject.Find("Main").GetComponent<Main>().placeMatches();                // place new set of Matches 
            }
    }

    //---

    public void matchesDec()                                                                // action for Button to decrease number of Matches
    {
        if (noOfMatches > 10)                                                               // range between 10 and 15
            if (noOfMatches <= 15)
            {
                gameObject.GetComponent<MenuSounds>().playButtonDown();
                noOfMatches--;
                noOfMatchesText.text = noOfMatches.ToString();
                GameObject.Find("Main").GetComponent<Main>().matchesAtStart--;              // decrease number of Matches
                GameObject.Find("Main").GetComponent<Main>().placeMatches();                // place new set of Matches
            }
    }

    //---

    public void difficultyInc()                                                             // action for Button to increase difficulty
    {
        if (difficulty == 1)
        {
            gameObject.GetComponent<MenuSounds>().playButtonUp();
            difficulty = 2;
            difficultyText.text = "Hard";
        }

    }

    //---

    public void difficultyDec()                                                             // action for Button to decrease difficulty
    {
        if (difficulty == 2)
        {
            gameObject.GetComponent<MenuSounds>().playButtonDown();
            difficulty = 1;
            difficultyText.text = "Easy";
        }

    }

    //---

    public void firstTurnInc()                                                              // action for Button to select who's turn is first (Computer first)
    {
        if (firstTurn == 0)
        {
            gameObject.GetComponent<MenuSounds>().playButtonUp();
            firstTurn = 1;
            firstTurnText.text = "Computer";
        }

    }

    //---

    public void firstTurnDec()                                                              // action for Button to select who's turn is first (Player first)
    {
        if (firstTurn == 1)
        {
            gameObject.GetComponent<MenuSounds>().playButtonDown();
            firstTurn = 0;
            firstTurnText.text = "Player";
        }

    }

    //---

    public void previousMenu()                                                              // "Back Button"
    {
        gameObject.GetComponent<MenuSounds>().playButtonDown();

        transform.FindChild("Game Menu").gameObject.SetActive(false);                       // hide this menu
        transform.FindChild("Main Menu").gameObject.SetActive(true);                        // show the previous one
    }

    //---

    public void startGame()                                                                 // start a new game
    {
        gameObject.GetComponent<MenuSounds>().playButtonUp();

        transform.FindChild("Game Menu").gameObject.SetActive(false);                       // hide game Menu
        transform.FindChild("HUD").gameObject.SetActive(true);                              // show HUD
        GameObject.Find("Main").GetComponent<Main>().playerAction = true;                   // allow Player input

        if (P1NameInput.text.ToString() != "")
        {
            if (P1NameInput.text.ToString() != player1Name)                                     // when entering a new name for player 1...
            {
                GameObject.Find("Main").GetComponent<Main>().winStreak = 0;                     // ...winStreak is set to 0
            }
            player1Name = P1NameInput.text.ToString();                                          // Player 1's name is read from Text field
        }
            
        if (P2NameInput.text.ToString() != "")
            player2Name = P2NameInput.text.ToString();                                          // Player 2's name is read from Text field

        GameObject.Find("Main").GetComponent<Main>().deltaTime = Time.time;                     // start Timing (stops in "Main.cs")
        GameObject.Find("Main").GetComponent<Main>().difficulty = difficulty;                   // give difficulty to "Main.cs"

        if (GameObject.Find("Camera Pivot").GetComponent<CameraMovement>().startPosition == false)      // reset camera to start position
            GameObject.Find("Camera Pivot").GetComponent<CameraMovement>().rotate180();

        if (GameObject.Find("Main").GetComponent<Main>().multiplayer == false && firstTurn == 1)        // if Computer has first turn...
            GameObject.Find("Main").GetComponent<Main>().nextPlayer();                                  // ...skip Player's turn
        else
            GameObject.Find("Main").GetComponent<Main>().playerNo = 1;                                  // if not: Player's turn
    }

    //-------------------------- Game Over Screen ----------------------------

    public void mainMenu() 
    {   
        //--- show Main Menu - hide everything else;
        transform.FindChild("Main Menu").gameObject.SetActive(true);
        transform.FindChild("Game Menu").gameObject.SetActive(false);
        transform.FindChild("GameOverScreen").gameObject.SetActive(false);
        transform.FindChild("HUD").gameObject.SetActive(false);
        transform.FindChild("ScoreBoard").gameObject.SetActive(false);
        //---
    }


    //------------------------- Restart Game ---------------------------------

    public void restartGame()
    {
        transform.FindChild("GameOverScreen").gameObject.SetActive(false);                      // hide Game Over Screen
        GameObject.Find("Main").GetComponent<Main>().placeMatches();                            // place new set of Matches
        startGame();                                                                            // start new game
    }
}
