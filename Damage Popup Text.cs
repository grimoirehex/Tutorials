using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;//Don't Forget

public class PopUpText : MonoBehaviour {

    //The Main Prefab Used To Create The Popups
    public GameObject popUpObject;  

    //A List That Holds All Currently Instantiated PopUp Objects
    List<GameObject> popUpList;
    
    void Start ()
    {       
        popUpList = new List<GameObject>();         
    }	

    void ProduceText()
    {
        //Create Instance Of Popup
        GameObject clone;
        clone = Instantiate(popUpObject, new Vector3(transform.position.x , transform.position.y + 2, transform.position.z), Quaternion.identity);
       
        //Set Text And Color Of Popup
        clone.transform.GetComponent<TextMeshPro>().text = "-" + Random.Range(1, 21);
        clone.transform.GetComponent<TextMeshPro>().color = Color.red;
        clone.transform.GetComponent<TextMeshPro>().fontSize = 5;

        //Randomly Choose If Text Appears Center, Left, Or Right Of Character
        int rand = Random.Range(0, 3);
        if (rand == 0)
        {
            clone.transform.GetComponent<TextMeshPro>().alignment = TMPro.TextAlignmentOptions.Center;
        }
        else if (rand == 1)
        {
            clone.transform.GetComponent<TextMeshPro>().alignment = TMPro.TextAlignmentOptions.Left;
        }
        else if(rand == 2)
        {
            clone.transform.GetComponent<TextMeshPro>().alignment = TMPro.TextAlignmentOptions.Right;
        }         

        //Add Clone To List And Start The Wait Coroutine
        popUpList.Add(clone);
        StartCoroutine(Wait(clone));        
    }

    //Wait 1/2 Of A Second Before Removing The Clone From The List, Then Destory It
    IEnumerator Wait(GameObject clone)
    {
        yield return new WaitForSeconds(.5f);
        popUpList.Remove(clone);
        Destroy(clone);
    }
   
    void Update()
    {    
        foreach (GameObject pop in popUpList)
        {
            //Update Popup To Remain Over Character Even While Moving
            pop.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);

            #region Shrink Over Time
            pop.transform.GetComponent<TextMeshPro>().fontSize = pop.transform.GetComponent<TextMeshPro>().fontSize - Time.deltaTime * 5;
            #endregion

            #region Face Camera
            //Use The LookRotation Function To Make Sure That The Popup Always Faces The Main Camera
            pop.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
            #endregion
        }

        //Create A PopUp Using The Z Button As A Trigger
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ProduceText();
        }
    }
}