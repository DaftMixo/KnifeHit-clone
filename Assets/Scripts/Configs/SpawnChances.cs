using System;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "SpawnChances", menuName = "Configs/SpawnChances", order = 0)]
public class SpawnChances : ScriptableObject
{
    [SerializeField] private SpawnedObstacles[] _moneyChance;
    [SerializeField] private SpawnedObstacles[] _obstacles;

    public GameObject GetMoney()
    {
        GameObject returnObject = new GameObject();
        returnObject.name = "null";
        if (_moneyChance.Length == 0) return returnObject;

        float chance = Random.Range(0, 100);
        

        float itemChance = 0;
        for (int i = 0; i < _moneyChance.Length; i++)
        {
            if (_moneyChance[i].Chance + itemChance >= chance && itemChance < chance)
            {
                returnObject = _moneyChance[i].ObstaclePrefab;
                break;
            }
            else
            {
                itemChance += _moneyChance[i].Chance;
            }
        }

        return returnObject;
    }

    public GameObject GetObstacle()
    {
        GameObject returnObject = new GameObject();
        returnObject.name = "null";
        if (_obstacles.Length == 0) return new GameObject();
        
        float chance = Random.Range(0, 100);

        float itemChance = 0;
        for (int i = 0; i < _obstacles.Length; i++)
        {
            if (_obstacles[i].Chance + itemChance >= chance && itemChance < chance)
            {
                returnObject = _obstacles[i].ObstaclePrefab;
                break;
            }
            else
            {
                itemChance += _obstacles[i].Chance;
            }
        }

        return returnObject;
    }

    [Serializable]
    private class SpawnedObstacles
    {
        [Range(0, 100)]public float Chance;
        public GameObject ObstaclePrefab;
    }
}
