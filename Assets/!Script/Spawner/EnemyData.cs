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