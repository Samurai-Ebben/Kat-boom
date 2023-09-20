using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using Unity.VisualScripting;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public Leaderboard leaderBoard;
    public TMP_InputField playerNameIF;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetRoutine());
    }


    public void SetPlayerName()
    {
        LootLockerSDKManager.SetPlayerName(playerNameIF.text, (respons) =>
        {
            if (respons.success)
            {
                Debug.Log("Seccessfully set player name");
            }
            else
            {
                Debug.Log("Could not set player name" + respons.errorData);
            }
        });
    }
    IEnumerator SetRoutine()
    {
        yield return loginRoutine();
        yield return leaderBoard.FetchTopThree();
    }

    IEnumerator loginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Player log in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Couldn't start session");
                done = true;
            }

        });
        yield return new WaitWhile(() => done == false);
    }

}
