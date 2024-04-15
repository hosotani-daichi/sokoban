using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    int[] map = { 0, 0, 1, 0, 0, };
    string debugTXT = "";

    // Start is called before the first frame update

    void PrintArray()
    {
        string debugText = "";
        for (int i = 0; i < map.Length; i++)
        {
            debugText += map[i].ToString() + ",";
        }
        Debug.Log(debugText);
    }

    int GetPlayerIndex()
    {
        for(int i = 0; i < map.Length; i++)
        {
            if (map[i] == 1)
            {
                return i;
            }
        }
        return -1;
    }

    void Start()
    {
        map = new int[] { 0, 2, 0, 1, 0, 2, 0, 2, 0 ,0,0};
        PrintArray();

        for (int i = 0; i < map.Length; i++) 
        {
            debugTXT += map[i].ToString() + ",";
        }
            Debug.Log(debugTXT);

       
    }

    bool MoveNumber(int number, int moveFrom, int moveTo)
    {
        if (moveTo < 0 || moveTo >= map.Length)
        {
            return false;
        }
        if (map[moveTo] == 2)
        {
            int velocity = moveTo - moveFrom;
            bool success = MoveNumber(2, moveTo, moveTo + velocity);
            if (success == false)
            {
                return false;
            }
        }
        map[moveTo] = number;
        map[moveFrom] = 0;
        return true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            int playerIndex = GetPlayerIndex();

            MoveNumber(1,playerIndex,playerIndex+1);
            PrintArray();

            string debugText = "";
            for(int i = 0; i < map.Length; i++)
            {
                debugText += map[i].ToString() + ",";
            }
            Debug.Log(debugText);
        }


        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int playerIndex = GetPlayerIndex();

            MoveNumber(1, playerIndex, playerIndex - 1);
            PrintArray();

            string debugText = "";
            for (int i = 0; i < map.Length; i++)
            {
                debugText += map[i].ToString() + ",";
            }
            Debug.Log(debugText);
        }

    }

 
   
}
