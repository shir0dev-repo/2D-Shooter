using System.Diagnostics; //Namespace containing the Stopwatch object.
using Debug = UnityEngine.Debug; //This line ensures we are using the correct Debug.Log method.
using UnityEngine;

[System.Serializable]
public struct EnemyData
{
    public EnemyType EnemyType;
    public GameObject EnemyPrefab;

    /*
        public EnemyData() Constructing a new copy of this struct. All fields inside struct MUST be initialized before construction is finished.
        {

        }
    */
}

public enum EnemyType
{
    Basic,
    Summoner
}