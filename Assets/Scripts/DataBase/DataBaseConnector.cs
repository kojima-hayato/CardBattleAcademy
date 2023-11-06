using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseConnector : MonoBehaviour
{

    //DBにアクセスする際の宛先設定(サーバを構築する際に変更の必要あり)
    string SERVER = "localhost";
    string DATABASE = "cardbattleacademy";
    string USERID = "root";
    string PORT = "3306";
    string PASSWORD = "";

    // Start is called before the first frame update
    void Start()
    {
        string connCmd =
       "server=" + SERVER + ";" +
       "database=" + DATABASE + ";" +
       "userid=" + USERID + ";" +
       "port=" + PORT + ";" +
       "password=" + PASSWORD;

        //接続するDBを指定する
        MySqlConnection conn = new(connCmd);

        try
        {
            Debug.Log("MySQLと接続中…");

            //DBと接続する
            conn.Open();

            //SQL文
            string sql = "SELECT * FROM test;";

            //SQL文をDBに渡す
            MySqlCommand cmd = new(sql, conn);

            //SQL文を実行した結果を受け取る
            MySqlDataReader rdr = cmd.ExecuteReader();

            //取得した結果に次の行があれば繰り返す
            while (rdr.Read())
            {
                //1列目：rdr[0]・2列目：rdr[1]...
                Debug.Log("text_id:" + rdr[0] + ", text:" + rdr[1]);
            }

            //結果の保持をやめる
            rdr.Close();

        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());

            //DBとの接続を切断する
            conn.Close();
            Debug.Log("接続を終了しました");
        }

        //DBとの接続を切断する
        conn.Close();
        Debug.Log("接続を終了しました");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
