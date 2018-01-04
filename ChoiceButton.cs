using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChoiceButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public string option;
    public DialogueManager box;

    public void SetOption(string newOption)
    {
        this.option = newOption;


    }

    public void SetText (string newText)
    {
        this.GetComponentInChildren<Text>().text = newText;
    }

    public void ParseOption()
    {
        string command = option.Split(',')[0];
        string commandModifier = option.Split(',')[1];
        box.playerTalking = false;
        if (command == "line")
        {
            box.lineNum = int.Parse(commandModifier);
            box.ShowDialogue();

        }
        else if (command == "scene")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Scene" + commandModifier);
        }
    }


}
