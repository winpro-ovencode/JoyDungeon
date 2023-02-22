using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public SkeletonAnimation Animation;
    // Start is called before the first frame update
    void Start()
    {
        Animation.AnimationName = "run";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlay(string name)
    {
        Animation.AnimationName = name;
    }
}
