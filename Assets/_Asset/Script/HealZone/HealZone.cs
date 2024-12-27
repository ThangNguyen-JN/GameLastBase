using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealZone : MonoBehaviour
{
    public float healInterval = 2f;
    private Coroutine healCorotine;

    public HealthPlayer characterManager;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SafeZone"))
        {
            if (healCorotine == null)
            {
                healCorotine = StartCoroutine(HealPlayer());
                
            }    
            Debug.Log("Trigger Safe Zone");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SafeZone"))
        {
            if (healCorotine != null)
            {
                StopCoroutine(healCorotine);
                healCorotine = null;
            }
        }
    }

    private IEnumerator HealPlayer()
    {

        while (characterManager.Health < characterManager.MaxHealth)
        {
            if (characterManager != null)
            {
                characterManager.Heal(2);
            }
                yield return new WaitForSeconds(healInterval);
        }

        healCorotine = null;

    }
}
