using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordSearch : MonoBehaviour
{       
    public Button button;//The Prefab Button Used To Create The Grid

    int edgeLength;//How Many Letters Are In Each Row And Column
    List<string> wordGrid;//Holds Letters To Be Used In Word Search
    List<string> wordBank;//Contains All Words That Can Be Found In Word Search
    List<Button> buttonsInGrid;//Holds A Reference To Each Button Being Used In The Word Search

    int currentNum;//The Index Representing The Currently Selected Letter
    string collectLetters;//Collects The Letters That The Player Chooses To See If They Match Any Words In The Word Bank

    //Holds The Available Choices That The Player Can Choose Surrounding The Currently Selected Letter 
    int westOption;
    int northOption;
    int eastOption;
    int southOption;
    int northWestOption;
    int northEastOption;
    int southEastOption;
    int southWestOption;        

    bool searchStarted;//Whether Or Not The First Letter Has Been Selected
    bool directionSelected;//Wheter Or Not At Least The 2nd Letter Has Been Selected To Establish A Direction
    string searchDirection;//The Direction The Player Must Continue To Follow In Order To Search For A Word       

    // Use this for initialization
    void Start()
    {             
        collectLetters = "";
        searchDirection = "";
        searchStarted = false;
        edgeLength = 5;
        
        //Word Grid Fills In With Index 0 ("C") In The Top Left, Ending With Index 24 ("G") In The Bottom Right
        wordGrid = new List<string>() { "C", "A", "T", "Q", "W", "E", "R", "T", "Y", "R", "I", "O", "A", "A", "I", "D", "F", "P", "H", "N", "K", "L", "E", "X", "G" };
        //Words That Can Be Found In The Grid
        wordBank = new List<string>() { "CAT", "RING", "TAPE" };
        //Access To All Buttons In The Grid Will Be Used To Change Their Color
        buttonsInGrid = new List<Button>();

        //Create A Grid Using The Length Of The Edges And The Letters That Should Be Used
        CreateGridOfButtons(edgeLength, wordGrid);        
    }

    void CreateGridOfButtons(int edgeLength, List<string> wordGrid)
    {
       
        //The Counters Keep Track Of When To Skip To The Next Row Or Column
        float xCounter = 0;
        float yCounter = 0;

        //Create A Button In The Grid For Each Letter
        for (int i = 0; i < edgeLength * edgeLength; i++)
        {
            Button clone = Instantiate(button);//Create Button Using Prefab
            buttonsInGrid.Add(clone);//Keep Track Of Each Button In The Grid
            clone.transform.SetParent(gameObject.transform, false);//Set The Canvas To Be The Parent
            clone.name = i.ToString();//Setting The Name To Be i Will Help Keep Track Of What Button Has Been Selected

            //Change The Text To The Correct Letter
            clone.GetComponentInChildren<Text>().text = wordGrid[i];

            //When The Button Is Clicked, Pass The Reference To The Verify Method 
            clone.onClick.AddListener(delegate { Verify(clone);});
            
            //The Width And Length Are Used To Determine The Spacing Between The Buttons
            float xValue = clone.gameObject.GetComponent<RectTransform>().sizeDelta.x;
            float yValue = clone.gameObject.GetComponent<RectTransform>().sizeDelta.y;

            //Place At The Correct X And Y Value
            clone.transform.localPosition = new Vector3(xValue * xCounter, yValue - yCounter, clone.transform.localPosition.z);

            //If The End Of The Row Has Been Reached, Move Down To The Next Row
            if (xCounter == edgeLength - 1)
            {
                xCounter = 0;
                yCounter += yValue;
            }
            else
            {
                //Keep Counting Forward (Left To Right), If It Still Isn't Time To Move Down To The Next Row
                xCounter++;
            }            
        }
    }

    void Verify( Button currentButton)
    {       
        //Change The Color To Show That The Button Has Been Selected
        currentButton.GetComponent<Image>().color = new Color(0, 255, 255);

        //Use The Name Of The Button To Determine What The Current Index Is
        currentNum = int.Parse(currentButton.name);

        //Add The New Letter To The Collected Letters To Determine If They Create A Word 
        collectLetters += currentButton.GetComponentInChildren<Text>().text;       

        //Start The Search 
        if (!searchStarted)
        {
            searchStarted = true;
            FindOptions(currentNum);
        }
        //Once The Search Has Started, Determine The Direction
        else if (searchStarted && !directionSelected)
        {
            directionSelected = true;

            if (currentNum == westOption) { searchDirection = "West"; FindOptions(currentNum); } else
            if (currentNum == northOption) { searchDirection = "North"; FindOptions(currentNum); } else
            if (currentNum == eastOption) { searchDirection = "East"; FindOptions(currentNum); } else
            if (currentNum == southOption) { searchDirection = "South"; FindOptions(currentNum); } else
            if (currentNum == northWestOption) { searchDirection = "NorthWest"; FindOptions(currentNum); } else
            if (currentNum == northEastOption) { searchDirection = "NorthEast"; FindOptions(currentNum); } else
            if (currentNum == southEastOption) { searchDirection = "SouthEast"; FindOptions(currentNum); } else
            if (currentNum == southWestOption) { searchDirection = "SouthWest"; FindOptions(currentNum); } else
            {
                //If An Incorrect Value Was Selected Reset The Booleans And Clear String Values
                InvalidOption();
            }           
        }
        //Make Sure That The Currently Selected Button Continues Down The Correct Path
        else if (searchStarted && directionSelected)
        {
            if (searchDirection == "West" && currentNum == westOption) { FindOptions(currentNum); }else
            if (searchDirection == "North" && currentNum == northOption){ FindOptions(currentNum); }else
            if (searchDirection == "East" && currentNum == eastOption){ FindOptions(currentNum); }else
            if (searchDirection == "South" && currentNum == southOption) { FindOptions(currentNum); }else
            if (searchDirection == "NorthWest" && currentNum == northWestOption) { FindOptions(currentNum); } else
            if (searchDirection == "NorthEast" && currentNum == northEastOption) { FindOptions(currentNum); }else
            if (searchDirection == "SouthEast" && currentNum == southEastOption) { FindOptions(currentNum); }else
            if (searchDirection == "SouthWest" && currentNum == southWestOption) { FindOptions(currentNum); }else
            {
                //If An Incorrect Value Was Selected Reset The Booleans And Clear String Values
                InvalidOption();
            }                
        }
    }
   
    //If A Button Selection Is Invalid Clear The Booleans And Strings
    void InvalidOption()
    {
        searchDirection = "";
        directionSelected = false;
        searchStarted = false;
        collectLetters = "";

        //Reset The Color Of Each Button
        for (int i = 0; i < buttonsInGrid.Count; i++)
        {
            buttonsInGrid[i].GetComponent<Image>().color = new Color(255,255,255);
        }
    }
    
    //Determine The Available Options For The Currently Selected Button  
    void FindOptions(int number)
    {
        westOption = number - 1;
        northOption = number - edgeLength;
        eastOption = number + 1;
        southOption = number + edgeLength;

        northWestOption = number - (edgeLength + 1);
        northEastOption = number - (edgeLength - 1);
        southEastOption = number + (edgeLength + 1);
        southWestOption = number + (edgeLength - 1);

        //Check The Word Bank To Determine If A Word Has Been Found
        for (int i = 0; i < wordBank.Count; i++)
        {            
            //If A Word Has Been Found The "InvalidOption" Method Is Used To Clear The Selection
            if(wordBank[i].Equals(collectLetters))
            {
                InvalidOption();
                break;
            }
            else
            {
               //If A Word Isn't Found, Do Something Here
            }
        }      
    }
}