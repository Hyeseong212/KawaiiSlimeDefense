using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerNumber { Player1, Player2, Player3, Player4 }
public class GameManager : MonoSingleton<GameManager>
{
    bool isfirst;
    public PlayerNumber playerNumber;
    public string PlayerID;
    [Header("웨이브")]
    public float testFirstWaveTime = 0;
    public float testSecondWaveTime = 0;

    float waveTime = 0;
    float uiWaveTime = 0;
    [SerializeField] Text uiWaveTimeTxt;
    [SerializeField] Text currentWave;
    public Text P1currentMonster;
    public Text P2currentMonster;
    public Text P3currentMonster;
    public Text P4currentMonster;

    [Header("골드,토큰,도박소 관련")]
    public int currentGold = 200;
    public int currentToken = 0;

    public Text Gold;
    public Text Token;
    private void Awake()
    {
        playerNumber = PlayerNumber.Player1;

        if (playerNumber == PlayerNumber.Player1)
        {
            Camera.main.transform.position = new Vector3(-40, GlobalOptions.i.options.cameraYvalue, 70);
        }
        else if(playerNumber == PlayerNumber.Player2)
        {

        }
        else if (playerNumber == PlayerNumber.Player3)
        {

        }
        else
        {

        }
        P1currentMonster.text = PlayerID + " 남은 몬스터 수 : " + EnemySpawner.i.P1enemyInThisWaveList.Count.ToString();
    }
    private void Start()
    {
        isfirst = true;
        StartCoroutine("WaveTimeLeft");
        Gold.text = currentGold.ToString();
        Token.text = currentToken.ToString();
    }
    private void Update()//UI상으로 남은시간 보여주는것
    {
        if (EnemySpawner.i.currentWave < 26)
        {
            uiWaveTime -= Time.deltaTime;
            uiWaveTimeTxt.text = ((int)uiWaveTime).ToString();
        }
        else
        {
            uiWaveTime = 0;
            uiWaveTimeTxt.text = ((int)uiWaveTime).ToString();
        }
    }
    IEnumerator WaveTimeLeft()//실제 게임에서 돌아가는 시간 (나눈이유 Update문에서 1번만처리되게 하는게 구조상 복잡해질것같아서)
    {
        while (true)
        {
            if (EnemySpawner.i.currentWave < 26)
            {
                if (isfirst)
                {
                    //처음시작할때
                    isfirst = false;
                    currentWave.text = "Round : " + EnemySpawner.i.currentWave.ToString();
                    uiWaveTime = testFirstWaveTime;
                    waveTime = testFirstWaveTime;
                }
                else //웨이브시작
                {
                    uiWaveTime = testSecondWaveTime;
                    waveTime = testSecondWaveTime;
                    EnemySpawner.i.NextWave();
                    currentWave.text = "Round : " + (EnemySpawner.i.currentWave).ToString();
                }
                uiWaveTime = waveTime;
                //UI상으로 보여주는시간 60으로 초기화
                yield return new WaitForSeconds(waveTime);
            }
            else
            {
                GameClear();
                yield break;
            }
        }
    }
    private void GameClear()
    {
        Debug.Log("게임클리어");
    }
}
