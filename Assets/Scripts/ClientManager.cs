using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class ClientManager : MonoBehaviour
{
    public static ClientManager Ins;
    public Character.Avatar Avatar;
    [Header("Entry")] public GameObject Entry;
    public UnityTransport Transport;
    public TMP_InputField Name;
    public Button JoinButton;

    public GameObject Wait;
    public GameObject Rank;
    public GameObject Vote;

    [Header("Rank")] public Candidate[] Candidates;
    public VerticalLayoutGroup LayoutGroup;
    [Header("Vote")] public Vote[] Votes;

    private void Awake()
    {
        Ins = this;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            Name.text = PlayerPrefs.GetString("PlayerName");
        }
    }

    public void SetServerIPAddress(string ipAddress)
    {
        Transport.ConnectionData.Address = ipAddress;
    }

    public void SetPlayerName(string playerName)
    {
        PlayerPrefs.SetString("PlayerName", playerName);
        JoinButton.interactable = !string.IsNullOrEmpty(playerName);
    }

    public void JoinServer()
    {
        NetworkManager.Singleton.OnClientStarted -= OpenWait;
        NetworkManager.Singleton.OnClientStarted += OpenWait;
        NetworkManager.Singleton.StartClient();
    }

    public void OpenWait()
    {
        Entry.SetActive(false);
        Wait.SetActive(true);
    }

    public void OpenRank(List<string> candidateNames)
    {
        Wait.SetActive(false);
        Rank.SetActive(true);
        Vote.SetActive(false);

        LayoutGroup.enabled = true;

        var i = 0;
        for (; i < candidateNames.Count; i++)
        {
            Candidates[i].Init(i, candidateNames[i]);
            Candidates[i].gameObject.SetActive(true);
        }

        for (; i < Candidates.Length; i++)
        {
            Candidates[i].Init(-1, "-");
            Candidates[i].gameObject.SetActive(false);
        }


        StartCoroutine(UnlockLayout());
    }

    private IEnumerator UnlockLayout()
    {
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        LayoutGroup.enabled = false;
    }

    public void SubmitRank()
    {
        var result = new List<string>
        {
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
        };
        foreach (var candidate in Candidates)
        {
            if (candidate.Id < 0)
                continue;
            result[candidate.Id] = candidate.Text.text;
        }

        Avatar.SubmitServerRpc(PlayerPrefs.GetString("PlayerName"), result[0], result[1], result[2], result[3],
            result[4], result[5], result[6], result[7], result[8]);
    }

    public void OpenVote(List<string> answer)
    {
        Wait.SetActive(false);
        Rank.SetActive(false);
        Vote.SetActive(true);

        var i = 0;
        for (; i < answer.Count; i++)
        {
            Votes[i].Init(answer[i]);
            Votes[i].gameObject.SetActive(true);
        }

        for (; i < Votes.Length; i++)
        {
            Votes[i].Init("-");
            Votes[i].gameObject.SetActive(false);
        }
    }

    public void SubmitVote()
    {
        foreach (var vote in Votes)
        {
            if (vote.Tick.isOn)
            {
                Avatar.SubmitServerRpc(PlayerPrefs.GetString("PlayerName"), vote.Text.text, null, null, null, null,
                    null, null, null, null);
                return;
            }
        }
    }
}