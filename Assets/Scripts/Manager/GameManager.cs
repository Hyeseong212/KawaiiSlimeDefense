using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerNumber { Player1, Player2, Player3, Player4 }
public class GameManager : MonoSingleton<GameManager>
{
    public PlayerNumber playerNumber;
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
}
