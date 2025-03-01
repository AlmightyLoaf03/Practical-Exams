using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, IItem
{
    public static event Action<int> OnGemCollect;
    public int worth = 5;

    public void Collect()
    {
        if (OnGemCollect != null) // Check if there are subscribers
        {
            OnGemCollect.Invoke(worth);
            SoundEffectManager.Play("Gem");
        }
        Destroy(gameObject);
    }
}
