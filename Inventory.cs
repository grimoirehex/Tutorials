using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Don't forget

public class Inventory : MonoBehaviour {

    public Button item; 

    // Use this for initialization
    void Start()
    {
        //This Tutorial Uses A Button To Add Items To The List

        //Create Button
        Button clone = Instantiate(item);

        //Make The "Content" Of The "Scroll View" The Parent
        clone.transform.SetParent(gameObject.transform, false);

        //Change The Text To Describe Its Function
        clone.GetComponentInChildren<Text>().text = "Add Items";
       
        //Add A Listener, "AddItem" Is The Method That Is Called When The Button Is Pressed
        clone.onClick.AddListener(AddItem);
    }

    //Returns A Random Item
    public string CreateItem()
    {
        //List Of Items
        string[] listOfItems = new string[] { "Oathkeeper", "Fenrir", "Kingdom Key", "Wishing Star", "Diamond Dust" , "Oblivion" , "Lady Luck" };

        //Return Random Item From Array
        return listOfItems[Random.Range(0, 7)];
    }

    //Items Are Created Just Like The Initial Button, With A Few Changes
    public void AddItem()
    {      
        //Creates Buttons To Represent Items
        Button clone = Instantiate(item);
        clone.transform.SetParent(gameObject.transform, false);

        //Set The Button Text To Be Equal To The Value Returned From The CreateItem Method
        clone.GetComponentInChildren<Text>().text = CreateItem();

        //Pass Along The Reference To The Button In The Action Listener
        clone.onClick.AddListener(() => UseItem(clone));
    }

    public void UseItem(Button button)
    {
        //Activate Any Effects Here, Then Remove The Item From The List

        GameObject.Destroy(button.gameObject);
    }
}