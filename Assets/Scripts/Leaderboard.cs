using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    string leaderboardID = "PublicHighScore";
    int leaderboardIDNum = 17630;

    public TextMeshProUGUI playerNames;
    public TextMeshProUGUI playerScores;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderboardID, (response) =>
        {
            if(response.success)
            {
                Debug.Log("Successfully uploaded score");
                done = true;
            }
            else
            {
                Debug.Log("Faild " + response.errorData);
                done = true;
            }
        });
        yield return new WaitWhile(()=> done == false);

    }
    public IEnumerator FetchTopScores()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreList(leaderboardID, 5, 0, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Got scores, successfully");
                string tmpPlayerNames = "Names:" + '\n';
                string tmpPlayerScores = "Scores:" + '\n';
                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    tmpPlayerNames += members[i].rank + ". ";
                    if (members[i].player.name != "")
                    {
                        tmpPlayerNames += members[i].player.name;
                    }
                    else
                    {
                        tmpPlayerNames += members[i].player.id;
                    }
                    tmpPlayerScores += members[i].score + "\n";
                    tmpPlayerNames += "\n";
                }
                done = true;
                playerNames.text = tmpPlayerNames;
                playerScores.text = tmpPlayerScores;
            }
            else
            {
                Debug.Log("Faild " + response.errorData);
                done = true;
            }
        });
        yield return new WaitWhile( ()=> done == false);
    }
}
