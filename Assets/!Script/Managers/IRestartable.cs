using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRestartable
{
    virtual void Subscribe()
    {
        if (GameManager.Instance != null)
            GameManager.OnGameRestart += Restart;
    }

    virtual void Unsubscribe() 
    { 
        if (GameManager.Instance != null)
            GameManager.OnGameRestart -= Restart;
    }

    abstract void Restart();
}
