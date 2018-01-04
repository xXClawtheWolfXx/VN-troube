﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    DialogueParser parser;
    // Use this for initialization
    void Start()
    {

        dialogue = "";
        characterName = "";
        emote = 0;
        playerTalking = false;
        parser = GameObject.Find("DialogueParser").GetComponent<DialogueParser>();
        lineNum = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("enter") || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown("space"))
        {
            if (playerTalking == false)
            {
                ShowDialogue();

                lineNum++;
            }
        }

        UpdateUI();
    }

    

    public string dialogue, characterName;
    public int lineNum;
    int emote;
    string[] options;
    public bool playerTalking;
    List<Button> buttons = new List<Button>();

    public Text dialogueBox;
    public Text nameBox;
    public GameObject choiceBox;

    public void ShowDialogue ()
    {
        ResetImages();
        ParseLine();
    }

    void ResetImages()
    {
        if(characterName != "")
        {
            GameObject character = GameObject.Find(characterName);
            SpriteRenderer currSprite = character.GetComponent<SpriteRenderer>();

            currSprite.sprite = null;
        }
    }

    void ParseLine()
    {
        if (parser.GetName (lineNum) != "Player")
        {
            playerTalking = false;
            characterName = parser.GetName(lineNum);
            dialogue = parser.GetContent(lineNum);
            emote = parser.GetEmote(lineNum);
            DisplayImages();
        }
        else
        {
            playerTalking = true;
            characterName = "";
            dialogue = "";
            options = parser.GetOptions(lineNum);
            CreateButtons();
        }
      
    }

    void DisplayImages()
    {
        if (characterName != "")
        {
            GameObject character = GameObject.Find(characterName);

            SpriteRenderer currsprite = character.GetComponent<SpriteRenderer>();
            currsprite.sprite = character.GetComponent<Character>().characterEmote[emote];
        }
    }
    
   void CreateButtons ()
    {
        for (int i =0; i< options.Length; i++)
        {
            GameObject button = (GameObject)Instantiate(choiceBox);
            Button b = button.GetComponent<Button>();
            ChoiceButton cb = button.GetComponent<ChoiceButton>();
            cb.SetText(options[i].Split(':')[0]);
            cb.option = options[i].Split(':')[1];
            cb.box = this;
            b.transform.SetParent(this.transform);
            b.transform.localPosition = new Vector3(0, -25 + (i * 50));
            b.transform.localScale = new Vector3(1, 1, 1);
            buttons.Add(b);

        }
    }

    void UpdateUI()
    {
        if (!playerTalking)
        {
            ClearButtons();        
        }

        dialogueBox.text = dialogue;
        nameBox.text = characterName;

    }

     void ClearButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            print("Clearing buttons");
            Button b = buttons[i];
            buttons.Remove(b);
            Destroy(b.gameObject);
        }
    }

}
