using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using Unity.VisualScripting;

public class PlayerManager : MonoBehaviour
{
    public Leaderboard leaderBoard;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetRoutine());
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
