using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameOver : MonoBehaviour
{
    PlayerData playData = new PlayerData();
    string path;
    string filename = "savedata";
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("게임오버 저장");
            string data = JsonUtility.ToJson(playData);
            File.WriteAllText(path + filename, data);
            SceneManager.LoadScene("StartGame");
        }
    }
}
