using System;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using Random = UnityEngine.Random;


[CreateAssetMenu(fileName = "SpawnChances", menuName = "Configs/SpawnChances", order = 0)]
public class SpawnChances : ScriptableObject
{
    [Tooltip("The probability is calculated from the sum of all set values\n\n" +
             "Шанс рассчитывается из суммы всех установленных значений")]
    [SerializeField] private SpawnedObstacles[] _moneyChance;
    
    [Tooltip("The probability is calculated from the sum of all set values\n\n" +
             "Шанс рассчитывается из суммы всех установленных значений")]
    [SerializeField] private SpawnedObstacles[] _obstacles;

    public GameObject GetMoney()
    {
        if (_moneyChance.Length == 0) return null;

        float maxChance = 0;
        foreach (var money in _moneyChance)
        {
            maxChance += money.Chance;
        }

        float chance = Random.Range(0, maxChance);     

        float itemChance = 0;
        for (int i = 0; i < _moneyChance.Length; i++)
        {
            if (_moneyChance[i].Chance + itemChance >= chance && itemChance < chance)
            {
                return _moneyChance[i].ObstaclePrefab;
            }
            else
            {
                itemChance += _moneyChance[i].Chance;
            }
        }

        return null;
    }

    public GameObject GetObstacle()
    {
        if (_obstacles.Length == 0) return null;

        float maxChance = 0;
        foreach (var obst in _obstacles)
        {
            maxChance += obst.Chance;
        }

        float chance = Random.Range(0, maxChance);

        float itemChance = 0;
        for (int i = 0; i < _obstacles.Length; i++)
        {
            if (_obstacles[i].Chance + itemChance >= chance && itemChance < chance)
            {
                return _obstacles[i].ObstaclePrefab;
            }
            else
            {
                itemChance += _obstacles[i].Chance;
            }
        }

        return null;
    }

    [Serializable]
    private class SpawnedObstacles
    {
        [Range(0, 100)]public float Chance;
        public GameObject ObstaclePrefab;
    }
}
