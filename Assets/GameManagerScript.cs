using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class GameManagerScript : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject boxPrefab;
    public GameObject goalPrefab;

    //�ǋL
    public GameObject clearText;

    int[,] map;
    GameObject[,] field;


    void Start()
    {

        Screen.SetResolution(1280, 270, false);

        //map�̐���
        map = new int[,]{
        { 0, 0, 2, 0, 0 },
        { 0, 3, 1, 3, 0 },
        { 0, 0, 2, 0, 0 },
        { 0, 2, 3, 2, 0 },
        { 0, 0, 0, 0, 0 },
    };
        //�t�B�[���h�T�C�Y����
        field = new GameObject
            [
            map.GetLength(0),
            map.GetLength(1)
            ];

        //�}�b�v�ɉ����ĕ`��
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 1)
                {
                    field[y, x] = Instantiate(
                  playerPrefab,
                  new Vector3(x, map.GetLength(0) - y, 0),
                  Quaternion.identity
                  );
                }
                if (map[y, x] == 2)
                {
                    field[y, x] = Instantiate(
                  boxPrefab,
                  new Vector3(x, map.GetLength(0) - y, 0),
                  Quaternion.identity
                  );
                }

            }
        }


        string debugTXT = "";

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                debugTXT += map[y, x].ToString() + ",";
            }
            debugTXT += "\n";
        }
        Debug.Log(debugTXT);

    }

    Vector2Int GetPlayerIndex()
    {
        for (int y = 0; y < field.GetLength(0); y++)
        {
            for (int x = 0; x < field.GetLength(1); x++)
            {
                if (field[y, x] == null) { continue; }
                if (field[y, x].tag == "Player") { return new Vector2Int(x, y); }
            }

        }
        return new Vector2Int(-1, -1);
    }

    bool IsCleard()
    {
        //Vector2Int�^�̉ϒ��z��̍쐬
        List<Vector2Int> goals = new List<Vector2Int>();

        for(int y = 0; y < map.GetLength(0); y++)
        {
            for(int x = 0;x < map.GetLength(1); x++)
            {
                //�i�[�ꏊ���ۂ��𔻒f
                if (map[y, x] == 3)
                {
                    //�i�[�ꏊ�ɃC���f�b�N�X���T���Ă���
                    goals.Add(new Vector2Int(x, y));
                }
            }
        }

        for(int i=0;i<goals.Count;i++) 
        {
            GameObject f = field[goals[i].y, goals[i].x];
            if (f == null || f.tag != "Box")
            {
                //1�ł������Ȃ�������������B��
                return false;
            }
        }
        return true;
    }


    bool MoveNumber(Vector2Int moveFrom, Vector2Int moveTo)
    {


        if (moveTo.x < 0 || moveTo.x >= field.GetLength(1))
        {
            return false;
        }
        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0))
        {
            return false;
        }

        if (field[moveTo.y, moveTo.x] != null && field[moveTo.y, moveTo.x].tag == "Box")
        {
            Vector2Int velocity = moveTo - moveFrom;
            bool success = MoveNumber(moveTo, moveTo + velocity);
            if (!success)
            {
                return false;
            }
        }
       
        Vector3 moveToPosition = new Vector3(moveTo.x, map.GetLength(0) - moveTo.y, 0);
        field[moveFrom.y, moveFrom.x].GetComponent<Move>().MoveTo(moveToPosition);
        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];
        field[moveFrom.y, moveFrom.x] = null;
        return true;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            Vector2Int playerIndex = GetPlayerIndex();

            MoveNumber(playerIndex, playerIndex + new Vector2Int(1, 0));
            //PrintArray();

            //�����N���A���Ă�����
            if (IsCleard())
            {
               //�Q�[���I�u�W�F�N�g��SetActive�̃��\�b�h���g���L����
               clearText.SetActive(true);
            }

        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            Vector2Int playerIndex = GetPlayerIndex();

            MoveNumber(playerIndex, playerIndex + new Vector2Int(-1, 0));
            //PrintArray();

            //�����N���A���Ă�����
            if (IsCleard())
            {
                //�Q�[���I�u�W�F�N�g��SetActive�̃��\�b�h���g���L����
                clearText.SetActive(true);
            }

        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            Vector2Int playerIndex = GetPlayerIndex();

            MoveNumber(playerIndex, playerIndex + new Vector2Int(0, -1));
            //PrintArray();

            //�����N���A���Ă�����
            if (IsCleard())
            {
                //�Q�[���I�u�W�F�N�g��SetActive�̃��\�b�h���g���L����
                clearText.SetActive(true);
            }

        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            Vector2Int playerIndex = GetPlayerIndex();

            MoveNumber(playerIndex, playerIndex + new Vector2Int(0, 1));
            //PrintArray();

            //�����N���A���Ă�����
            if (IsCleard())
            {
                //�Q�[���I�u�W�F�N�g��SetActive�̃��\�b�h���g���L����
                clearText.SetActive(true);
            }

        }



        //string debugText = "";
        //for(int i = 0; i < map.Length; i++)
        //{
        //    debugText += map[i].ToString() + ",";
        //}
        //Debug.Log(debugText);
        //}


        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{

        //    PrintArray();

        //    string debugText = "";
        //    for (int i = 0; i < map.Length; i++)
        //    {
        //        debugText += map[i].ToString() + ",";
        //    }
        //    Debug.Log(debugText);
        //}

        //}



    }
}