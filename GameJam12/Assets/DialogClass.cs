using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DialogClass:MonoBehaviour
{
    public GameObject background;

    public Transform outScenePos;

    public Transform inScenePos;

    [TextArea]
    public List<string> sentence = new List<string>();

    public List<string> charactersInScene = new List<string>();//All characters displayed in scene
    public List<string> currCharacterList = new List<string>();// Current character talking


    public GameObject question;

    public GameObject nextDialog;


    GameObject controller;
    int currentSentence = 0;
    int currentCharacter = 0;
    int charactersDisplayedInd = 0;
    private void Start()
    {
        controller = GameObject.Find("SceneController");
    }
    public void NextStep()
    {
        if (currentSentence < sentence.Count - 1)
        {
        //    Debug.Log(currentSentence);
            currentSentence++;
            currentCharacter++;
            if(charactersInScene.Count!=0)
                charactersDisplayedInd = currentSentence % charactersInScene.Count;

        }
        else
        {
            EndScene();
        }
            
    }
    public string ReturnSentence()
    {
        return sentence[currentSentence];
    }
    public string ReturnName()
    {
        if (currCharacterList.Count == 0)
            return "";
        else
            return currCharacterList[currentCharacter];
    }
    public List<string> ReturnCharacters()
    {
        
        List<string> str=new List<string>();
        if (charactersInScene.Count != 0)
        {
            string[] strr = charactersInScene[charactersDisplayedInd].Split(',');
            foreach (string st in strr)
                str.Add(st);
        }
        return str;
    }

    public void EndScene()
    {

        if (question != null)
        {
            controller.GetComponent<ControllerScript>().RunQuestion(question);
        }
        else
        {
            RemoveBackground();
            controller.GetComponent<ControllerScript>().NextScene(nextDialog);
        }
       
       // ResetScene();
    }
    public void ResetScene()
    {
        currentSentence = 0;
        currentCharacter = 0;
        charactersDisplayedInd = 0;
    }
    public void RemoveBackground()
    {
        background.transform.position = outScenePos.position;
    }
    public void InsertBackground()
    {
        background.transform.position = inScenePos.position;
    }


}
