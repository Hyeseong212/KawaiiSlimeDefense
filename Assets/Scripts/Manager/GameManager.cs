using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerNumber { Player1, Player2, Player3, Player4 }
public class GameManager : MonoSingleton<GameManager>
{
    public PlayerNumber playerNumber;

    public float testFirstWaveTime = 0;
    public float testSecondWaveTime = 0;

    float waveTime = 0;
    float uiWaveTime = 0;
    [SerializeField] Text uiWaveTimeTxt;

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
    }
    private void Start()
    {
        StartCoroutine("WaveTimeLeft");
    }
    private void Update()//UI상으로 남은시간 보여주는것
    {
        if (EnemySpawner.i.currentWave < 27)
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
                if (EnemySpawner.i.currentWave == 0)
                {
                    //처음시작할때
                    uiWaveTime = testFirstWaveTime;
                    waveTime = testFirstWaveTime;
                    EnemySpawner.i.currentWave++;
                }
                else //웨이브시작
                {
                    uiWaveTime = testSecondWaveTime;
                    waveTime = testSecondWaveTime;
                    EnemySpawner.i.NextWave();
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
        Debug.Log("게임클릭어");
    }
}
