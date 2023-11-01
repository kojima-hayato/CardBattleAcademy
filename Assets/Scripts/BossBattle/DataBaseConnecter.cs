using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseConnecter : MonoBehaviour
{
    private MySqlConnection connection;
    private string server;
    private string database;
    private string user;
    private string password;

    public void Database_Initialize()
    {
        server = "127.0.0.1";
        database = "localhost";
        user = "root";
        //password = "PASSWORD";    パスワード(設定してなければいらない？)

        //Uid = USERNAME; Pwd = PASSWORD;   必要ならconnectionStringの末尾に追加
        string connectionString = "Server=IP-ADDRESS;Port=PORT;Database=DB_NAME;";
        MySqlConnection connection = new MySqlConnection(connectionString);

        connection.Open();

        string query = "INSERT INTO user (UserName) VALUES ('User1')";
        MySqlCommand command = new(query, connection);

        command.CommandTimeout = 60;

        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            // Access data using reader["columnname"]
        }
        reader.Close();

        connection.Close();
    }
// Start is called before the first frame update
void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
