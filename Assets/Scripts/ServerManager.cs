using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class ServerManager : NetworkBehaviour
{
    public static ServerManager Ins;
    public TMP_InputField[] Inputs;
    private List<Result> _results;
    private List<string> _speeds;
    public TMP_Text[] Item;
    public TMP_Text[] Score;
    public TMP_Text[] Speed;

    private float _timer;

    private void Awake()
    {
        Ins = this;
    }

    public void CreateRank()
    {
        _results = new List<Result>();
        _speeds = new List<string>();
        _timer = 0;
        var contents = new List<string>();
        foreach (var input in Inputs)
        {
            contents.Add(input.text);
        }
        UpdateResultTab();
        InitRankClientRpc(contents[0], contents[1], contents[2], contents[3], contents[4], contents[5], contents[6], contents[7], contents[8]);
    }

    [ClientRpc]
    public void InitRankClientRpc(string result0, string result1, string result2, string result3, string result4, string result5, string result6, string result7, string result8)
    {
        if (ClientManager.Ins != null)
        {
            var content = new List<string>();
            if (!string.IsNullOrEmpty(result0))
                content.Add(result0);
            if (!string.IsNullOrEmpty(result1))
                content.Add(result1);
            if (!string.IsNullOrEmpty(result2))
                content.Add(result2);
            if (!string.IsNullOrEmpty(result3))
                content.Add(result3);
            if (!string.IsNullOrEmpty(result4))
                content.Add(result4);
            if (!string.IsNullOrEmpty(result5))
                content.Add(result5);
            if (!string.IsNullOrEmpty(result6))
                content.Add(result6);
            if (!string.IsNullOrEmpty(result7))
                content.Add(result7);
            if (!string.IsNullOrEmpty(result8))
                content.Add(result8);
            ClientManager.Ins.OpenRank(content);
        }
    }

    public void CreateVote()
    {
        _results = new List<Result>();
        _speeds = new List<string>();
        _timer = 0;
        var contents = new List<string>();
        foreach (var input in Inputs)
        {
            contents.Add(input.text);
        }
        UpdateResultTab();
        TestClientRpc();
        InitVoteClientRpc(contents[0], contents[1], contents[2], contents[3], contents[4], contents[5], contents[6], contents[7], contents[8]);
    }

    [ClientRpc]
    public void TestClientRpc()
    {
        Debug.Log("Success");
    }
    
    [ClientRpc]
    public void InitVoteClientRpc(string result0, string result1, string result2, string result3, string result4, string result5, string result6, string result7, string result8)
    {
        if (ClientManager.Ins != null)
        {
            var content = new List<string>();
            if (!string.IsNullOrEmpty(result0))
                content.Add(result0);
            if (!string.IsNullOrEmpty(result1))
                content.Add(result1);
            if (!string.IsNullOrEmpty(result2))
                content.Add(result2);
            if (!string.IsNullOrEmpty(result3))
                content.Add(result3);
            if (!string.IsNullOrEmpty(result4))
                content.Add(result4);
            if (!string.IsNullOrEmpty(result5))
                content.Add(result5);
            if (!string.IsNullOrEmpty(result6))
                content.Add(result6);
            if (!string.IsNullOrEmpty(result7))
                content.Add(result7);
            if (!string.IsNullOrEmpty(result8))
                content.Add(result8);
            ClientManager.Ins.OpenVote(content);
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;
    }

    public void HandleSubmit(string playerName, List<string> submit)
    {
        if (_speeds.Contains(playerName))
        {
            _speeds.Remove(playerName);
        }
        
        _speeds.Add(playerName);
        
        foreach (var result in _results)
        {
            if (result.PlayerName == playerName)
            {
                result.SubmitContent = submit;
                UpdateResultTab();
                return;
            }
        }

        _results.Add(new Result
        {
            PlayerName = playerName,
            SubmitContent = submit
        });

        UpdateResultTab();
    }

    private void UpdateResultTab()
    {
        var scores = new Dictionary<string, int>();
        
        foreach (var result in _results)
        {
            for (var i = 0; i < result.SubmitContent.Count; i++)
            {
                var item = result.SubmitContent[i];
                if (scores.ContainsKey(item))
                {
                    scores[item] += result.SubmitContent.Count - i;
                }
                else
                {
                    scores.Add(item, result.SubmitContent.Count - i);   
                }
            }
        }

        var j = 0;
        foreach (var score in scores)
        {
            Item[j].text = score.Key;
            Score[j].text = score.Value.ToString();
            j++;
        }

        for (; j < Item.Length; j++)
        {
            Item[j].text = "-";
            Score[j].text = "-";
        }

        for (j = 0; j < Speed.Length && j < _speeds.Count; j++)
        {
            Speed[j].text = _speeds[j];
        }

        for (; j < Speed.Length; j++)
        {
            Speed[j].text = "-";
        }
    }
}

public class Result
{
    public string PlayerName;
    public List<string> SubmitContent;
}