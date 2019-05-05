using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionClass:MonoBehaviour
{
    public List<int> allowedOption = new List<int>(); //list of option to every character
    public List<string> options = new List<string>();  //option text
    public List<int> mutanyLevel = new List<int>();

    public List<GameObject> sceneOptions = new List<GameObject>(); //scenes that each option leads you



    private GameObject controller;




    public GameObject Answer(int index)
    {
        if(mutanyLevel.Count>0)
            controller.GetComponent<ControllerScript>().crewMutany += mutanyLevel[index];
        return sceneOptions[index];
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
