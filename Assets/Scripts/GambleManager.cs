using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GambleManager : MonoSingleton<GambleManager>
{

    public void GoldGamble()//°ñµå °·ºí
    {
        int spendGold;
        spendGold = EnemySpawner.i.currentWave * 10;
        if (GameManager.i.currentGold >= spendGold)
        {
            int random = (Random.Range(-(EnemySpawner.i.currentWave* 10) - 20, (EnemySpawner.i.currentWave* 10) + 36));
            int rewardGold = EnemySpawner.i.currentWave * random; //º¸»ó°ñµå
            GameManager.i.currentGold -= spendGold; //¼Ò¸ð °ñµå¸¸Å­ »©°í
            GameManager.i.currentGold += rewardGold; //º¸»ó°ñµå¸¸Å­ ³Ö´Â´Ù
            if (GameManager.i.currentGold < 0)
            {
                GameManager.i.currentGold = 0;
            }
            GameManager.i.Gold.text = GameManager.i.currentGold.ToString();
        }
        else
        {
            UIViewGame.i.WarningTextSetter(AddresablesDataManager.i.GetString(1001));
        }
    }
    public void TokenGamble()//ÅäÅ« °·ºí
    {
        int spendGold = 5000;
        int rewardToken = Random.Range(0, 2); //º¸»ó°ñµå
        if (GameManager.i.currentGold >= spendGold)
        {
            GameManager.i.currentGold -= spendGold; //¼Ò¸ð °ñµå¸¸Å­ »©°í
            GameManager.i.currentToken += rewardToken;
            GameManager.i.Gold.text = GameManager.i.currentGold.ToString();
            GameManager.i.Token.text = GameManager.i.currentToken.ToString();
        }
        else
        {
            UIViewGame.i.WarningTextSetter(AddresablesDataManager.i.GetString(1001));
        }
    }
}
