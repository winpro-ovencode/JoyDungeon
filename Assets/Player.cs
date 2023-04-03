using DG.Tweening;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Stat
    {
        Strength,
        Speed,
        Walk
    }
    // 델리게이트 선언
    public delegate void UpdateGoldDelegate(int newScore);
    // 이벤트 선언
    public event UpdateGoldDelegate OnGoldUpdated;

    public float Speed = 5f;
    public SkeletonAnimation Animation;
    public int Strength { get { return _strength; } }
    public int Gold { get { return _gold; } }

    public int Walk { get { return _walk; } }
    public int AttackSpeed { get { return _speed; } }

    GameObject _targetTree;
    int _treeHP;
    bool _alreadyHit;
    int _strength;
    int _gold;
    int _walk;
    int _speed;

    // Start is called before the first frame update
    void Start()
    {
        Animation.AnimationName = "run";
        Animation.AnimationState.Event += AnimationState_Event;
        Animation.AnimationState.Complete += AnimationState_Complete;
        _strength = 1;
    }

    internal void UpgradeStat(Stat stat)
    {
        int value = 0;
        switch(stat)
        {
            case Stat.Strength:
                value = _strength;
                break;
            case Stat.Speed:
                value = _speed;
                break;
            case Stat.Walk:
                value = _walk;
                break;
        }

        if (_gold < Mathf.Pow(value, 1.35f)) 
        {
            return;
        }

        _gold -= (int)Mathf.Pow(value, 1.35f);

        switch(stat)
        {
            case Stat.Strength:
                _strength++;
                break;
            case Stat.Speed:
                _speed++;
                break;
            case Stat.Walk:
                _walk++;
                break;
        }
        OnGoldUpdated?.Invoke(_gold);
        Animation.timeScale = 0.8f + (_speed * 0.05f);
    }

    private void AnimationState_Complete(Spine.TrackEntry trackEntry)
    {
        if(trackEntry.animation.name == "attack1")
        {
            _alreadyHit = false;
            Debug.Log("complete" + trackEntry.Animation.name);

            if (_targetTree.GetComponent<Tree>().IsDead())
            {
                Speed = 5 + (_speed * 0.5f);
                Animation.ClearState();
                Animation.AnimationState.AddAnimation(0, "run", true, 0);
                _targetTree = null;
                _gold += UnityEngine.Random.Range(2, 5);
                OnGoldUpdated?.Invoke(_gold);
                return;
            }

            Animation.AnimationState.AddAnimation(0, "attack1", false, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * Speed);
    }

    public void OnPlay(string name)
    {
        Animation.AnimationName = name;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_targetTree == collision.gameObject)
            return;

        _targetTree = collision.gameObject;
        
        Animation.ClearState();
        Animation.AnimationState.AddAnimation(0, "attack1", false, 0);
        Speed = 0;
    }

    private void AnimationState_Event(Spine.TrackEntry trackEntry, Spine.Event e)
    {
        if(e.Data.Name == "impact")
        {
            if (!_alreadyHit)
            {
                _alreadyHit = true;
                _targetTree.transform.DOShakePosition(0.5f, 0.2f, 2, 90, false);
                if(!_targetTree.GetComponent<Tree>().Damage(_strength))
                {
                    _targetTree.SetActive(false);
                }
            }
        }
    }
}
