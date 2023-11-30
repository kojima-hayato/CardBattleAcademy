using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class DataBaseConnector
{
    //DBにアクセスする際の宛先設定(サーバを構築する際に変更の必要あり)
    string SERVER = "localhost";
    string DATABASE = "cardbattleacademy";
    string USERID = "root";
    string PORT = "3306";
    string PASSWORD = "";

    MySqlConnection conn = new();
    MySqlCommand cmd = new();
    MySqlDataReader rdr;
    DataTable dt = new();

    // Start is called before the first frame update
    public void SetCommand()
    {
        string connCmd =
       "server=" + SERVER + ";" +
       "database=" + DATABASE + ";" +
       "userid=" + USERID + ";" +
       "port=" + PORT + ";" +
       "password=" + PASSWORD;

        //接続するDBを指定する
        conn = new(connCmd); 
    }

    public DataTable ExecuteSQL(string sql)
    {
        try
        {

            //DBと接続する
            Debug.Log("DB接続中");
            conn.Open();

            //SQL文をDBに渡す
            Debug.Log("sql文:" + sql);
            cmd = new(sql, conn);

            //SQL文を実行した結果を受け取る
            rdr = cmd.ExecuteReader();

            //実行結果を他でも使える型に移す
            dt.Load(rdr);
            return dt;
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            return dt;
        }
        finally
        {
            Debug.Log("DB接続終了");
            //結果保持の終了
            rdr.Close();

            //DB接続の終了
            conn.Close();
        }
    }
}
