using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMPlayerTrigger : MonoBehaviour
{
    public MoreMountains.Feedbacks.MMF_Player mMF_Player;

    private void Start()
    {
        mMF_Player = gameObject.GetComponent<MoreMountains.Feedbacks.MMF_Player>();
    }

    public void Play()
    {
        mMF_Player.PlayFeedbacks();
    }
}
