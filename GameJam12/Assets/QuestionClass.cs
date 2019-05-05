using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionClass:MonoBehaviour
{


    [Tooltip("option text")]
    public List<string> options = new List<string>();  //option text


    [Tooltip("list of option to every character")]
    public List<int> allowedOption = new List<int>(); //list of option to every character

    [Tooltip("each option mutany level change")]
    public List<int> mutanyLevel = new List<int>();// each option mutany level change

    [Tooltip("each option chance of success")]
    public List<int> chance = new List<int>();//each option chance of success

    [Tooltip("if chance failes goes to this scene")]
    public List<GameObject> failedScenes = new List<GameObject>();//if chance failes goes to this scene

    [Tooltip("scenes that each option leads you")]
    public List<GameObject> sceneOptions = new List<GameObject>(); //scenes that each option leads you

    [Tooltip("adds modifier that changes options in some questions")]
    public List<int> AddModifier = new List<int>(); //adds modifier that changes options in some questions 



    private GameObject controller;




    public GameObject Answer(int index)
    {
        if (mutanyLevel.Count > 0)
            controller.GetComponent<ControllerScript>().crewMutany += mutanyLevel[index];
        if (AddModifier.Count > 0)
            controller.GetComponent<ControllerScript>().AddChoice(AddModifier[index]);

        if (chance.Count == 0)
        {
            return sceneOptions[index];
        }
        else
        {
            if (chance[index] < Random.Range(1, 99))
            {
                allowedOption[index] = -1;
                return failedScenes[index];
            }
            else
            {
                return sceneOptions[index];
            }

        }
    }
    public List<string> PosibleAnswers()
    {
        int i = 0;
        List<string> str = new List<string>();
        foreach (int num in allowedOption)
        {

            if (num == 0 || controller.GetComponent<ControllerScript>().GetChoice().Contains(num))
                str.Add(options[i]);
            i++;
        }
        return str;
    }




    private void Start()
    {
        controller = GameObject.Find("SceneController");
    }
}
