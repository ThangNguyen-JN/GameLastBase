using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretGameObjectManager : MonoBehaviour
{
    public Dictionary<string, GameObject> turretMapping = new Dictionary<string, GameObject>();

    private void Awake()
    {
        // Ánh xạ key đến các GameObject có sẵn trong cảnh
        turretMapping.Add("Level1", GameObject.Find("Turret_01"));
        turretMapping.Add("Level2", GameObject.Find("Turret_02"));

        // Đảm bảo tất cả tháp pháo ban đầu đều bị vô hiệu hóa
        foreach (var turret in turretMapping.Values)
        {
            if (turret != null)
            {
                turret.SetActive(false);
            }
        }
    }
    public GameObject GetTurretByLevel(string leverTurret)
    {
        if (turretMapping.TryGetValue(leverTurret, out GameObject turret))
        {
            return turret;
        }
        Debug.LogWarning($"No turret found for level: {leverTurret}");
        return null;
    }
}
