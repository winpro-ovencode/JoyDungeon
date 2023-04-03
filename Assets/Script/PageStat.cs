using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageStat : MonoBehaviour
{
    public Player LocalPlayer;
    public Button ButtonStrength;
    public UIStat StatStrength;

    public Button ButtonSpeed;
    public UIStat StatSpeed;

    public Button ButtonWalk;
    public UIStat StatWalk;
    public TMPro.TMP_Text TextGold;
    // Start is called before the first frame update
    void Start()
    {
        ButtonStrength.onClick.AddListener(() => OnStrength());
        ButtonSpeed.onClick.AddListener(() => OnSpeed());
        ButtonWalk.onClick.AddListener(() => OnWalk());
        TextGold.text = "0";
        LocalPlayer.OnGoldUpdated += LocalPlayer_OnGoldUpdated;
        OnStrength();
        OnWalk();
        OnSpeed();
    }

   

    private void LocalPlayer_OnGoldUpdated(int newScore)
    {
        TextGold.text = $"{newScore}";
        if (LocalPlayer.Gold >= (int)Mathf.Pow(LocalPlayer.Strength, 1.35f))
        {
            StatStrength.TextGold.color = Color.white;
            StatStrength.TextGold.text = $"{(int)Mathf.Pow(LocalPlayer.Strength, 1.35f)}";
        }
        else
        {
            StatStrength.TextGold.color = Color.red;
            StatStrength.TextGold.text = $"{(int)Mathf.Pow(LocalPlayer.Strength, 1.35f)}";
        }

        if (LocalPlayer.Gold >= (int)Mathf.Pow(LocalPlayer.Walk, 1.35f))
        {
            StatWalk.TextGold.color = Color.white;
            StatWalk.TextGold.text = $"{(int)Mathf.Pow(LocalPlayer.Walk, 1.35f)}";
        }
        else
        {
            StatWalk.TextGold.color = Color.red;
            StatWalk.TextGold.text = $"{(int)Mathf.Pow(LocalPlayer.Walk, 1.35f)}";
        }

        if (LocalPlayer.Gold >= (int)Mathf.Pow(LocalPlayer.AttackSpeed, 1.35f))
        {
            StatSpeed.TextGold.color = Color.white;
            StatSpeed.TextGold.text = $"{(int)Mathf.Pow(LocalPlayer.AttackSpeed, 1.35f)}";
        }
        else
        {
            StatSpeed.TextGold.color = Color.red;
            StatSpeed.TextGold.text = $"{(int)Mathf.Pow(LocalPlayer.AttackSpeed, 1.35f)}";
        }
    }

    void OnStrength()
    {
        LocalPlayer.UpgradeStat(Player.Stat.Strength);
        StatStrength.Level = LocalPlayer.Strength;
        StatStrength.TextLevel.text = $"Lv {LocalPlayer.Strength}";
        StatStrength.TextValue.text = $"{LocalPlayer.Strength}";

        if(LocalPlayer.Gold >= (int)Mathf.Pow(LocalPlayer.Strength, 1.35f))
        {
            StatStrength.TextGold.color = Color.white;
            StatStrength.TextGold.text = $"{(int)Mathf.Pow(LocalPlayer.Strength, 1.35f)}";
        } else
        {
            StatStrength.TextGold.color = Color.red;
            StatStrength.TextGold.text = $"{(int)Mathf.Pow(LocalPlayer.Strength, 1.35f)}";
        }
    }
    private void OnWalk()
    {
        LocalPlayer.UpgradeStat(Player.Stat.Walk);
        StatWalk.Level = LocalPlayer.Walk;
        StatWalk.TextLevel.text = $"Lv {LocalPlayer.Walk}";
        StatWalk.TextValue.text = $"{LocalPlayer.Walk}";

        if (LocalPlayer.Gold >= (int)Mathf.Pow(LocalPlayer.Walk, 1.35f))
        {
            StatWalk.TextGold.color = Color.white;
            StatWalk.TextGold.text = $"{(int)Mathf.Pow(LocalPlayer.Walk, 1.35f)}";
        }
        else
        {
            StatWalk.TextGold.color = Color.red;
            StatWalk.TextGold.text = $"{(int)Mathf.Pow(LocalPlayer.Walk, 1.35f)}";
        }
    }

    private void OnSpeed()
    {
        LocalPlayer.UpgradeStat(Player.Stat.Speed);
        StatSpeed.Level = LocalPlayer.AttackSpeed;
        StatSpeed.TextLevel.text = $"Lv {LocalPlayer.AttackSpeed}";
        StatSpeed.TextValue.text = $"{LocalPlayer.AttackSpeed}";

        if (LocalPlayer.Gold >= (int)Mathf.Pow(LocalPlayer.AttackSpeed, 1.35f))
        {
            StatSpeed.TextGold.color = Color.white;
            StatSpeed.TextGold.text = $"{(int)Mathf.Pow(LocalPlayer.AttackSpeed, 1.35f)}";
        }
        else
        {
            StatSpeed.TextGold.color = Color.red;
            StatSpeed.TextGold.text = $"{(int)Mathf.Pow(LocalPlayer.AttackSpeed, 1.35f)}";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
