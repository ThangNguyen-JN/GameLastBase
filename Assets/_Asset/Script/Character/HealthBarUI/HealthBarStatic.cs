using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarStatic : MonoBehaviour
{
    public Image delayedFill;
    public Image healthBarfill;
    
    public CharacterManager characterManager;

    private Coroutine delayedFillCoroutine;


    private void OnEnable()
    {
        characterManager.OnHealthChanged += UpdateHealthBar;

    }
    private void OnDisable()
    {
        characterManager.OnHealthChanged -= UpdateHealthBar;
    }
       

    private void UpdateHealthBar(int currentHealth, int maxHealth)
    {
        float healthPercent = (float) currentHealth / maxHealth;
        healthBarfill.fillAmount = healthPercent;

        if (delayedFillCoroutine != null)
        {
            StopCoroutine(delayedFillCoroutine);
        }
        delayedFillCoroutine = StartCoroutine(DelayedFillUpdate(healthPercent));

        if (healthBarfill.fillAmount > delayedFill.fillAmount)
        {
            delayedFill.fillAmount = healthBarfill.fillAmount;
        }

    }

    private IEnumerator DelayedFillUpdate(float targetFill)
    {
        while (delayedFill.fillAmount > targetFill)
        {
            delayedFill.fillAmount = Mathf.Lerp(delayedFill.fillAmount, targetFill, Time.deltaTime * 1f);
            if(Mathf.Abs(delayedFill.fillAmount - targetFill) <0.001f)
            {
                delayedFill.fillAmount = targetFill;
                break;
            }
            yield return null;
        }    
    }    

    private void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }

}
