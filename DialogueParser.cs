using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class DialogueParser : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {

        string file = "Assets/Scripts/Prologue";
        string sceneNum = SceneManager.GetActiveScene().name;
        sceneNum = Regex.Replace(sceneNum, "[^0-9]", "");
        file += sceneNum;
        file += ".txt";

    

        lines = new List<DialogueLine>();

        LoadDialogue(file);


	}
	
	// Update is called once per frame
	void Update () {
		
	}

    struct DialogueLine
    {
        public string name;
        public string content;
        public int emote;
        public string[] options;

        public DialogueLine (string Name, string Content, int Emote)
        {
            name = Name;
            content = Content;
            emote = Emote;
            options = new string[0];

        }
    }


    List<DialogueLine> lines;

     void LoadDialogue(string filename)
    {
        string line;
        StreamReader R = new StreamReader(filename);

        using (R)
        {
            do
            {
                line = R.ReadLine();
                if (line != null)
                {
                    string[] lineData = line.Split(';');
                    if (lineData[0] == "Player")
                    {
                        DialogueLine lineEntry = new DialogueLine(lineData[0], "", 0);
                        lineEntry.options = new string[lineData.Length - 1];

                        for (int i = 1; i < lineData.Length; i++)
                        {
                            lineEntry.options[i - 1] = lineData[i];
                        }
                        lines.Add(lineEntry);

                    }
                }
            }
            while (line != null);
            R.Close();
        }
    }

    public string GetName (int lineNumber)
    {
        if (lineNumber < lines.Count)
        {
            return lines[lineNumber].name;
        }
        return "";
    }

    public string GetContent (int lineNumber)
    {
        if (lineNumber < lines.Count)
        {
            return lines[lineNumber].content;
        }
        return "";
    }

    public int GetEmote (int lineNumber)
    {
        if (lineNumber <lines.Count)
        {
            return lines[lineNumber].emote;
        }
        return 0;
    }

    public string[] GetOptions (int lineNumber)
    {
        if (lineNumber < lines.Count)
        {
            return lines[lineNumber].options;
        }
        return new string[0];
    }










}
