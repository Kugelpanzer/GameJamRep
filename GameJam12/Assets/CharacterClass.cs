using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClass:MonoBehaviour
{
    public int id;
    public string characterName;
    bool inScene;


    private Transform outScenePos;

    private Transform inScenePos;

    float moveDist = 6f;
    public void RemoveFromScene()
    {
        transform.position = outScenePos.position;
        inScene = false;
    }
    public void EnterScene(int pos, int max)
    {
        if (max % 2 == 0)
        {
            if (pos < max / 2)
            {
                transform.position = inScenePos.position - new Vector3(moveDist/2, 0);
            }
            else
            {
                transform.position = inScenePos.position + new Vector3(moveDist/2, 0);
            }
        }
        else
        {
            transform.position = inScenePos.position + new Vector3(moveDist * (pos - max / 2), 0);

        }
        inScene = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        inScenePos = GameObject.Find("InScene").transform;
        outScenePos = GameObject.Find("OutOfSceneObj").transform;
    }
    /*
    // Update is called once per frame
    void Update()
    {
        
    }*/
}
