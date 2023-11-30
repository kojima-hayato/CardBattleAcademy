using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class DataBaseConnector
{
    //DB�ɃA�N�Z�X����ۂ̈���ݒ�(�T�[�o���\�z����ۂɕύX�̕K�v����)
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

        //�ڑ�����DB���w�肷��
        conn = new(connCmd); 
    }

    public DataTable ExecuteSQL(string sql)
    {
        try
        {

            //DB�Ɛڑ�����
            Debug.Log("DB�ڑ���");
            conn.Open();

            //SQL����DB�ɓn��
            Debug.Log("sql��:" + sql);
            cmd = new(sql, conn);

            //SQL�������s�������ʂ��󂯎��
            rdr = cmd.ExecuteReader();

            //���s���ʂ𑼂ł��g����^�Ɉڂ�
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
            Debug.Log("DB�ڑ��I��");
            //���ʕێ��̏I��
            rdr.Close();

            //DB�ڑ��̏I��
            conn.Close();
        }
    }
}
