using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Script quan ly tai nguyen thu thap va tu dong di chuyen
public class CharacterCollectItemSystem : MonoBehaviour
{
    public Transform player;
    public ChangeResource changeResource;
    public float moveDuration;

    public void Start()
    {
        changeResource = new ChangeResource();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ItemSkull"))
        {
            CollectItem(other.gameObject, "ItemSkull");
        }

        if (other.CompareTag("ItemWood"))
        {
            CollectItem(other.gameObject, "ItemWood");
        }
        
        if (other.CompareTag("ItemStone"))
        {
            CollectItem(other.gameObject, "ItemStone");
        }

        if (other.CompareTag("ItemIron"))
        {
            CollectItem(other.gameObject, "ItemIron");
        }

        if (other.CompareTag("ItemDetail"))
        {
            CollectItem(other.gameObject, "ItemDetail");
        }

        if (other.CompareTag("ItemFuel"))
        {
            CollectItem(other.gameObject, "ItemFuel");
        }

        if (other.CompareTag("ItemRope"))
        {
            CollectItem(other.gameObject, "ItemRope");
        }

        if (other.CompareTag("ItemBattery"))
        {
            CollectItem(other.gameObject, "ItemBattery");
        }
        
    }


    private void CollectItem(GameObject item, string resourceName)
    {
        Resource resource = ResourceDatabase.Instance.GetResource(resourceName);
        if (resource == null)
        {
            return;
        }
        if (resource.amount >= resource.maxAmount)
        { 
            return;
        }

        Sequence moveSequence = DOTween.Sequence();

        moveSequence.AppendCallback(() =>
        {
            item.transform.DOMove(player.position, moveDuration).SetEase(Ease.Linear);
        });

        moveSequence.AppendInterval(moveDuration).OnComplete(() =>
        {
            changeResource.AddResource(resourceName, 1);
            //ResourceDatabase.Instance.SaveResource();
            if (QuestManager.Instance != null)
            {
                QuestManager.Instance.UpdateQuestProgress(resourceName, 1);
            }
            if (GoogleAchievements.Instance != null && GoogleLogin.Instance.IsLoggedIn())
            {
                string achievementID = GetAchievementID(resourceName);
                if (!string.IsNullOrEmpty(achievementID))
                {
                    GoogleAchievements.Instance.IncrementAchievement(achievementID, 1);
                }
            }
            Destroy(item);
        });
        //item.transform.DORotate(new Vector3(0, 0, 0), moveDuration, RotateMode.FastBeyond360);
        item.transform.DOScale(Vector3.zero, moveDuration);
    }

    private string GetAchievementID(string resourceName)
    {
        switch (resourceName)
        {
            case "ItemSkull": return "CgkIyqnG1LcDEAIQBQ";
            case "ItemWood": return "CgkIyqnG1LcDEAIQAw";
            case "ItemStone": return "CgkIyqnG1LcDEAIQBA";
            default: return "";
        }
    }


}
