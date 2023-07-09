using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushGhost : MonoBehaviour
{
    [SerializeField] private float targetX;
    [SerializeField] private float timeRequiredToFinish;

    // Update is called once per frame
    void Start()
    {
        LeanTween.moveLocalX(gameObject, targetX, timeRequiredToFinish).setEaseInOutCubic().setLoopPingPong();
    }
}
