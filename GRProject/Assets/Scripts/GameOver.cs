using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameOver : MonoBehaviour       // ���ӿ��� �� �� �ѱ� + ȹ����ȭ ����
{
    PlayerData playData = new PlayerData();
    string path;
    string filename = "savedata";

    [SerializeField] private GameObject press;
    void Update()
    {
    }
    public void VillageBtn()
    {
        string data = JsonUtility.ToJson(playData);
        File.WriteAllText(path + filename, data);
        //SceneManager.LoadScene("StartGame");
        press.SetActive(true);
    }
}
