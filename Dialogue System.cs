using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour {

    public InputField displayText;
    public bool letterByLetter;
    public bool wordByWord;

    string dialogue1, dialogue2, dialogue3, dialogue4;
    string[] sentences;
    int counter;

	// Use this for initialization
	void Start ()
    {         
        counter = 0;

        dialogue1 = "Hello I am Goku";
        dialogue2 = "Food Wars is great";
        dialogue3 = "Sora, Kairi, and Riku are friends";
        dialogue4 = "Some League of Legends players are toxic";

        sentences = new string[] {dialogue1 , dialogue2 , dialogue3 , dialogue4};                
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (letterByLetter)
            {
                displayText.text = "";//Clear previous message
                StartCoroutine(OneLetterAtATime());
            }
            else if (wordByWord)
            {
                displayText.text = "";//Clear previous message
                StartCoroutine(OneWordAtATime());
            }
            else
            {
                UpdateText();
            }
        }                 
	}

    void UpdateText()
    {
        displayText.text = sentences[counter];
        CheckCounter();
    }

    IEnumerator OneLetterAtATime()
    {
        for(int i = 0; i < sentences[counter].Length; i++)//Focus on the message located at the index specified by "counter"
        {
            displayText.text += sentences[counter][i];//"i" Moves to each letter located at the "counter" index
            yield return new WaitForEndOfFrame();//Each frame one more letter gets added
        }

        CheckCounter();//Increase the counter, to move to the next message
        yield return new WaitForEndOfFrame();//End the coroutine
    }

    IEnumerator OneWordAtATime()
    {
        string[] collect = sentences[counter].Split(' ');//Removes any spaces and creates an array containing only words

        for(int i = 0; i < collect.Length; i++)//Loop through the newly created array
        {
            displayText.text += collect[i] + " ";
            yield return new WaitForSeconds(0.2f);//Any time you want
        }

        CheckCounter();//Increase the counter, to move to the next message
        yield return new WaitForEndOfFrame();//End the coroutine
    }

    void CheckCounter()
    {
        counter++;

        if (counter == sentences.Length)
        {
            counter = 0;
        }
        
    }
}