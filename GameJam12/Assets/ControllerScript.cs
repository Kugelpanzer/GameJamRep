using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerScript : MonoBehaviour
{


    public int crewMutany = 3;
    public List<GameObject> AllCharacters = new List<GameObject>();

    AudioSource audio;

    public List<GameObject> lastScene = new List<GameObject>();

    public List<int> choiceList=new List<int>();
    public GameObject firstScene;
    private GameObject currentScene;
    public GameObject mutanyScene;

    private List<string> splitList = new List<string>();
    //private List<GameObject> Character;
    /*   private string characterTalking;
       private string sentence;*/

    private bool endFlag;

    #region gui
    public GUISkin skin;

    public Text nameText;
    public Text sentenceText;

    GameObject displayQuestion;
    ControllerScript cs;
    #endregion

    #region ControllerMethods
    public void AddChoice(int choice)
    {
        choiceList.Add(choice);
    }
    public List<int> GetChoice()
    {
        return choiceList;
    }

    public void RunQuestion(GameObject Question)
    {
        displayQuestion = Question;
    }
    public void NextScene(GameObject Scene)
    {
        currentScene = Scene;
        currentScene.GetComponent<DialogClass>().InsertBackground();
    }

    public void LoadStep()
    {
        /* foreach(GameObject end in lastScene)
         {
             if(currentScene== end)
             {
                 endFlag = false;
             }
         }*/
        if (lastScene.Contains(currentScene)) endFlag = true;
        DialogClass dc = currentScene.GetComponent<DialogClass>();
        string[] str= dc.ReturnSentence().Split('/');
        foreach(string s in str)
        {
            splitList.Add(s);
        } 
        nameText.text = dc.ReturnName();
        //LOAD CHARACTERS
        if (dc.ReturnCharacters().Count != 0)
        {
            foreach (GameObject ch in AllCharacters)
            {
                ch.GetComponent<CharacterClass>().RemoveFromScene();
            }
            int i = 0;
            foreach (GameObject ch in AllCharacters)
            {
                if (dc.ReturnCharacters().Contains(ch.GetComponent<CharacterClass>().characterName))
                {
                    ch.GetComponent<CharacterClass>().EnterScene(i, dc.ReturnCharacters().Count);
                    i++;
                }
            }
        }

    }

    private void MicroStep()
    {
        string pop;
        pop = splitList[0];
        splitList.RemoveAt(0);
        sentenceText.text += pop;
    }
    public void Step()
    {
        /*if (displayQuestion == null)
        {*/
        if (splitList.Count == 0)
        {
            sentenceText.text = "";
            LoadStep();
            if (splitList.Count != 0)
            {
                MicroStep();
            }
             currentScene.GetComponent<DialogClass>().NextStep();
        }
        else
        {
            MicroStep();
        }
       /* }
        else
        {

        }*/
    }
#endregion
    #region GuiMethods
    public void QuestionShow()
    {
        if (displayQuestion != null)
        {
            int i = 0;
            //displayQuestion = Question;
            QuestionClass qc = displayQuestion.GetComponent<QuestionClass>();
            foreach (string option in qc.PosibleAnswers())
            {

                if (GUI.Button(new Rect(200, i * 50, 500, 50), option))
                {

                    AnswerQuestion(i);
                }
                i++;
            }
        }
    }
    public void AnswerQuestion(int ind)
    {
        NextScene(displayQuestion.GetComponent<QuestionClass>().Answer(ind));
        if (crewMutany <= 0)
        {
            NextScene(mutanyScene);
        }
        displayQuestion = null;

    }
    #endregion

    private void OnGUI()
    {


        GUI.skin = skin;
        QuestionShow();
    }
    // Start is called before the first frame update
    void Start()
    {


       // displayQuestion = null;
        cs = gameObject.GetComponent<ControllerScript>();

        currentScene = firstScene;
        currentScene.GetComponent<DialogClass>().InsertBackground();


        //LoadStep();
        sentenceText.text = "";
        nameText.text = "";
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && (!endFlag) &&(displayQuestion==null))
        {
            // if(currentScene!=null)

            // LoadStep();
            audio.Play();
            Step();

        }
    }
}
