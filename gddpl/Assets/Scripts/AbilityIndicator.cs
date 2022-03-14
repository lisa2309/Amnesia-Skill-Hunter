using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static StateController;

public class AbilityIndicator : MonoBehaviour
{
    private Image ability;
    private Image background;
    private Ability oldAbility;

    [Header("AbilitySprites")]
    [SerializeField]
    private Sprite bow;
    [SerializeField]
    private Sprite fireball;
    [SerializeField]
    private Sprite none;

    
    void Awake()
    {
        background = this.transform.Find("Ability_Background").GetComponent<Image>();
        ability = this.transform.Find("Ability").GetComponent<Image>();
    }

    private void Start()
    {
        oldAbility = StateController.currentAbility;
        ability.sprite = none;
        Color c;
        c = ability.color;
        c.a = 0f;
        ability.color = c;
    }

    void FixedUpdate()
    {
        if (oldAbility != StateController.currentAbility)
        {            
            UpdateSprite();
            StartCoroutine("IndicateChange");
            oldAbility = StateController.currentAbility;
        }

    }

    private void UpdateSprite()
    {
        if (StateController.currentAbility == StateController.Ability.Bow)
        {
            ability.sprite = bow;
            Color c;
            c = ability.color;
            c.a = 1f;
            ability.color = c;
        }
        else if (StateController.currentAbility == StateController.Ability.Fireball) 
        { 
            ability.sprite = fireball;
            Color c;
            c = ability.color;
            c.a = 1f;
            ability.color = c;
        }
        else
        {
            ability.sprite = none;
            Color c;
            c = ability.color;
            c.a = 0f;
            ability.color = c;
        }
    }

    private IEnumerator IndicateChange()
    {
        Color c;
        //fade in
        for (float f = 0f; f <= 0.5f;f += 0.04f)
        {
            c = background.color;
            c.a = f;
            background.color = c;
            yield return new WaitForSeconds(0.01f);
        }
        //fade out
        for (float f = 0.4f; f >= -0.05f; f -= 0.05f)
        {
            c = background.color;
            c.a = f;
            background.color = c;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
