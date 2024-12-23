using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpText : MonoBehaviour
{
    [SerializeField] private Text hpText;
    public CharacterManager characterManager;
    // Start is called before the first frame update
    void Start()
    {
        characterManager.OnHealthChanged += UpdateHealthText;
        UpdateHealthText(characterManager.Health, characterManager.MaxHealth);
        Debug.Log($"{characterManager.Health}");
    }

    private void UpdateHealthText(int currentHp, int maxHp)
    {
        hpText.text = characterManager.Health.ToString();
    }

    private void OnDestroy()
    {
        //Kiem tra khi bi loi neu nhu nhan vat die. Khi script bi huy se de tiem an loi. 
        characterManager.OnHealthChanged -= UpdateHealthText;
    }
}
