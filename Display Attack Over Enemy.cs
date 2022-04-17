using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Be Sure To Add This

public class BattleText : MonoBehaviour {

	public Text attack;   
	public GameObject[] attackEffects = { }; //May not be needed for your script, but will help show the effect 

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (Attacker ()); //Will Switch Between Attacks
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Places Text Slightly Above Enemy
		attack.transform.position = new Vector3( gameObject.transform.position.x, gameObject.transform.position.y +2, gameObject.transform.position.z);
	}

	IEnumerator Attacker()
	{
		GameObject clone;//Needed If Instantiating An Object

		attack.text = "Attack1";//Name First Attack
		clone = Instantiate(attackEffects[0], new Vector3(transform.position.x, transform.position.y + 1, transform.position.z) + transform.forward * 1.2f, transform.rotation) as GameObject;

		yield return new WaitForSeconds (2f);//Wait A Little Before Next Attack

		attack.text = "Attack2";//Name Second Attack
		clone = Instantiate(attackEffects[1], new Vector3(transform.position.x, transform.position.y + 1, transform.position.z) + transform.forward * 1.2f, transform.rotation) as GameObject;

		yield return new WaitForSeconds (2f);//Wait Before Restarting Coroutine

		StartCoroutine (Attacker ());//Restart Coroutine
	}
}