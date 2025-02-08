using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITurretUpgradeManager : MonoBehaviour
{
    public TurretUpgradeManager turretUpgradeManager;
    public Text currentLevelText; // Hiển thị cấp độ hiện tại
    public Transform resourceListContainer; // Nơi chứa danh sách tài nguyên
    public GameObject resourceItemPrefab; // Prefab cho mỗi tài nguyên
    //public Button upgradeButton; // Nút nâng cấp

    private void Awake()
    {
        UpdateUI();
    }
   

    public void UpdateUI()
    {
        // Lấy cấp độ hiện tại và thông tin cấp tiếp theo
        int currentLevel = turretUpgradeManager.GetCurrentLevel();
        TurretUpgradeLevel nextLevel = turretUpgradeManager.GetNextLevel();

        // Cập nhật cấp độ hiện tại
        if (nextLevel == null)
        {
            currentLevelText.text = "Max Level!";
            //upgradeButton.interactable = false; // Vô hiệu nút nâng cấp
            return;
        }

        currentLevelText.text = $"TURRET: {currentLevel}";

        // Xóa tài nguyên cũ
        foreach (Transform child in resourceListContainer)
        {
            Destroy(child.gameObject);
        }

        // Tạo UI cho tài nguyên yêu cầu
        foreach (var resource in nextLevel.requiredResources)
        {
            GameObject resourceItem = Instantiate(resourceItemPrefab, resourceListContainer);
            resourceItem.transform.Find("ResourceImage").GetComponent<Image>().sprite = resource.imageResource;
            resourceItem.transform.Find("ResourceText").GetComponent<Text>().text = $"{resource.quantilyResource}";
        }
    }

    

}
