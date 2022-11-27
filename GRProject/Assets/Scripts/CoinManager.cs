using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int earnedCoins;
    public static int earnedCoinsInstance;
    [SerializeField]
    public Text earnedCoinsCount;
    private static CoinManager instance;
    public static CoinManager count_instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CoinManager>();
            }
            return instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        earnedCoinsCount.text = earnedCoins.ToString();
        SaveManager.getCoinInstance = earnedCoins;
        earnedCoinsInstance = earnedCoins;
    }
}
