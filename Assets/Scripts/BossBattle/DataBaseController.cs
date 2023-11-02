using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class DataBaseController : MonoBehaviour
{
    string SERVER = "localhost";
    string DATABASE = "cardbattleacademy";
    string USERID = "root";
    string PORT = "3306";
    string PASSWORD = "1234";

    // Start is called before the first frame update
    void Start()
    {
        string connCmd =
       "server=" + SERVER + ";" +
       "database=" + DATABASE + ";" +
       "userid=" + USERID + ";" +
       "port=" + PORT + ";" +
       "password=" + PASSWORD;

        MySqlConnection conn = new(connCmd);

        try
        {
            Debug.Log("MySQLÇ∆ê⁄ë±íÜÅc");
            conn.Open();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }
        conn.Close();
        Debug.Log("ê⁄ë±ÇèIóπÇµÇ‹ÇµÇΩ");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
