using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {

    GameObject[] matches;

    public int matchesAtStart = 10;
    public int matchCount;
    public int matchNo = 1;
    public int selectCount = 0;
    public int playerNo = 1;
    string winnerName = "";

    int currentScore = 0;
    int overallScore = 20;
    int pointsForWin = 100;
    int timeBonus = 200;
    public float deltaTime = 0;
    int winStreakBonus;
    public int winStreak = 0;

    public bool playerAction = false;
    public bool multiplayer = false;

    public int difficulty = 1;
    int allowedToTake = 3;

    public List<GameObject> selectedMatches;
    GameObject Match;
 

    void Start () {

        matches = new GameObject[matchesAtStart + 1];                        // 1-10 rather than 0-9
        selectedMatches = new List<GameObject>();
        Match = Resources.Load<GameObject>("Match");
        currentScore = 0;
    }



	void Update () {

        if (matchCount == 3)
            allowedToTake = 2;

        if (matchCount == 2)
            allowedToTake = 1;

        if (Input.GetKeyDown("space") && playerAction == true && selectCount > 0)           // if space is pressed while input is allowed and at least 1 Match is selected
        {
            GameObject.Find("Menu").GetComponent<MenuSounds>().playButtonDown();

            takeMatch();                                                                    // Player takes Matches
            matchCount = matchCount - selectCount;                                          // reduce MatchCount by number of taken Matches
            selectCount = 0;                                                                // reset counter for selected Matches
            nextPlayer();                                                                   // end Player turn
        }

    }



    //--------------------------- Place Matches ----------------------------

    public void placeMatches()
    {
        for (int i = 1; i < matches.Length; i++)                                    // delete all placed Matches
        {
            Destroy(matches[i]);
        }
        matches = new GameObject[matchesAtStart + 1];

        int evenNumber = matchCount % 2;
        float x;                                                                    // x-position of the placed Match
        float distance = 4f / matchesAtStart ;                                          // distance between matches

        for (int i = 1; i <= matchesAtStart; i++)
        {
            if (evenNumber == 0)
            {
                if (i <= matchesAtStart / 2)
                    x = (-i + 0.5f) * distance;                                         // place half of the matches left of x = 0
                else
                    x = (i - matchesAtStart / 2 - 0.5f) * distance;                     // other half right of x = 0
            }
            else
            {
                if (i <= matchesAtStart / 2)
                    x = (i) * distance;                                                 // place half of the matches left of x = 0
                else
                    x = (i - matchesAtStart) * distance;                                // other half right of x = 0
            }


            Vector3 v3 = new Vector3(x, 1.09f, 0.7f);                               // vector for x,y,z position of the Match
            float randomRotation = Random.Range(-3, 3);                             // random rotation for the matches to look like placed by hand

            matches[i] = Instantiate(Match, v3, Quaternion.identity);
            matches[i].name = "Match " + i;
            matches[i].transform.Rotate(Vector3.right * -89.2f);                    // Rotation around X-Axis
            matches[i].transform.Rotate(Vector3.up * 0);                            // Rotation around Y-Axis
            matches[i].transform.Rotate(Vector3.forward * randomRotation);          // Rotation around Z-Axis
        }
        matchCount = matchesAtStart;
        print("Game started, " + matchesAtStart + " matches placed");
    }



    //--------------------------- Next Player ------------------------------

    public void nextPlayer() {

        if (matchCount > 1)
        {
            if (multiplayer == false)
            {
                playerNo = 0;
                StartCoroutine(waitForComputer());                                    // lets the Computer wait a specific amount of time and then take matches
            }
            if (multiplayer == true)
            {
                if (playerNo == 1)                                                     // switches between Player 1 and Player 2
                    playerNo = 2;
                else
                    playerNo = 1;                
            }

            GameObject.Find("Camera Pivot").GetComponent<CameraMovement>().rotate180();
        }
        else
            gameOver();
    }



    //---------------------- Player takes Matches --------------------------

    void takeMatch()                                                                    
    {
         for (int j = 1; j <= matchesAtStart; j++)                                 // go through array of Matches
         {
             if (selectedMatches.Contains(matches[j]) == true)                     // if one of the selected Matches is found
             {
                 Destroy(matches[j]);                                              // take it
                 print("You took " + matches[j].name);
             }
         }
        selectedMatches.Clear();                                                   // reset List of selected Matches
        print("You took " + selectCount + " matches");
    }



    //------------------------- Computer's Turn ----------------------------

    public void computerTurn () {

        int noOfMatchesToTake = 3;                                              // Number of Matches that the Computer will take



        //----------------- Hard Mode ------------------
        if (difficulty == 2)
        {
            noOfMatchesToTake = Random.Range(1, allowedToTake + 1);             // Random returns Numbers from min [inclusive] to max [exclusive] (hence "+ 1")

            if (matchCount == 8 || matchCount == 4)
                noOfMatchesToTake = 3;

            if (matchCount == 7 || matchCount == 3)
                noOfMatchesToTake = 2;

            if (matchCount == 6)
                noOfMatchesToTake = 1;
        }
        //----------------------------------------------



        //----------------- Easy Mode ------------------
        if (difficulty == 1)
        {
            noOfMatchesToTake = Random.Range(1, allowedToTake + 1);            // Computer takes random number of Matches from 1 to 3
        }
        //----------------------------------------------



        if (matchCount != 1)
        {
            for (int i = 1; i <= noOfMatchesToTake; i++)
            {
                int randomMatchNo = Random.Range(1, matchesAtStart);           // Computer picks random Match out of Array

                if (matches[randomMatchNo] != null)                            // in case match has already been taken...
                {
                    print("Computer takes Match " + randomMatchNo);
                    Destroy(matches[randomMatchNo]);                           // remove Asset
                    matches[randomMatchNo] = null;                             // empty Array at randomMatchNo
                    matchCount--;
                }
                else
                    i--;                                                       // ...try again
            }
            print("Computer took " + noOfMatchesToTake + " matches");

            GameObject.Find("Menu").GetComponent<MenuSounds>().playButtonDown();
            GameObject.Find("Camera Pivot").GetComponent<CameraMovement>().rotate180();
        }
    }



    //---------------------------- Game Over -------------------------------

    void gameOver()
    {
        deltaTime = Time.time - deltaTime;                                                                                  // stop Timing (starts at "Menu.cs")

        GameObject.Find("Menu").GetComponent<Menu>().transform.FindChild("GameOverScreen").gameObject.SetActive(true);
        GameObject.Find("Menu").GetComponent<Menu>().transform.FindChild("HUD").gameObject.SetActive(false);

        playerAction = false;                                                                                               // no inputs allowed

        GameObject.Find("Menu").GetComponent<Menu>().ScoreName.text = "";                                                   // delete previous score from Game Over Screen
        GameObject.Find("Menu").GetComponent<Menu>().Scores.text = "";

        Text result = GameObject.Find("Menu").GetComponent<Menu>().resultText;                                              // Textfield for "You Win/Loose"



        for (int j = 1; j <= matchesAtStart; j++)                                                                           // search last remaining Match
        {
            if (matches[j] != null)
            matches[j].GetComponentInChildren<ParticleSystem>().Play();                                                     // ignite
        }
        GetComponent<AudioSource>().Play();




        if (multiplayer == false)                                                                                           // write Text for "You Win/Loose"
        {
            if (playerNo == 0)
            {
                result.text = "You loose!";
                print("You loose");
                winStreak = 0;                                                                                              // Player loses, winStreak resets
            }

            if (playerNo == 1)
            {
                result.text = "You win!";
                print("You win");
                printScore();                                                                                               // Player  wins, show scores on Game Over Screen
                winStreak++;
            }
        }



        if (multiplayer == true)                                                                                            // write Text for "Player 1 / Player 2 Wins"
        {
            if (playerNo == 1)
                winnerName = GameObject.Find("Menu").GetComponent<Menu>().player1Name;

            if (playerNo == 2)
                winnerName = GameObject.Find("Menu").GetComponent<Menu>().player2Name;

            result.text = winnerName + " wins!";
        }
    }



    //--------------------- Show Scores on Game Over -----------------------

    void printScore()
    {
        Text ScoreName = GameObject.Find("Menu").GetComponent<Menu>().ScoreName;
        Text Scores = GameObject.Find("Menu").GetComponent<Menu>().Scores;

        timeBonus = timeBonus - (2 * (int) deltaTime);                                        // calculate Time Bonus: starts at 200, decreases every second by 2
        currentScore = (pointsForWin + timeBonus) * difficulty;                               // double points for Hard Mode
        winStreakBonus = (currentScore * winStreak) / 2;                                      // for every consecutive victory add half of the current score  
        currentScore = currentScore + winStreakBonus;

        ScoreName.text = "Score:\n";                                                          // write Textfields for type of points (Time Bonus, WinStreak, etc.)
        ScoreName.text = ScoreName.text + "\n" + "Victory";
        if (timeBonus != 0)
            ScoreName.text = ScoreName.text + "\n" + "Time Bonus";
        if (winStreak != 0)
            ScoreName.text = ScoreName.text + "\n" + "Winning Streak";
        ScoreName.text = ScoreName.text + "\n" + "Total Score:";

        Scores.text = "\n\n" + pointsForWin;                                                  // write points
        if (timeBonus != 0)
            Scores.text = Scores.text + "\n" + timeBonus.ToString();
        if (winStreak != 0)
            Scores.text = Scores.text + "\n" + winStreakBonus.ToString();
        Scores.text = Scores.text + "\n" + currentScore.ToString();

        string difficultyString = "Easy";                                                     // generate String out of value for Difficulty
        if (difficulty == 2)
            difficultyString = "Hard";

        timeBonus = 200;                                                                      // reset Time Bonus

        GameObject.Find("Menu").GetComponent<Menu>().addHighscore(GameObject.Find("Menu").GetComponent<Menu>().player1Name, difficultyString, winStreak, currentScore);     // add Highscore
       
    }


    //------------------------ Wait for Computer ---------------------------

    IEnumerator waitForComputer()
    {
        print("Wait for Computer's Turn...");
        playerAction = false;                                           // no Player input allowed
        yield return new WaitForSeconds(2);                             // Computer waits for 2 seconds before
        computerTurn();                                                 // it makes it's turn

        if (matchCount == 1)                                            // if Computer gets the last Match
            gameOver();                                                 // Game is over, Computer lost
        else                                                            // if more than 1 Match left
        {
            playerNo = 1;                                               // Player's turn
            playerAction = true;                                        // allow Player input
        }
        
    }
}
