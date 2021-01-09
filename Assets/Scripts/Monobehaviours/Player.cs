using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public HitPoints hitPoints;

    public HealthBar healthBarPrefab;
    HealthBar healthbar;

    public Inventory inventoryPrefab;
    Inventory inventory;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;

            if (hitObject != null)
            {
                bool shouldDisappear = false;
                switch (hitObject.itemType)
                {
                    case Item.ItemType.COIN:
                        shouldDisappear = inventory.AddItem(hitObject);
                        break;
                    case Item.ItemType.HEALTH:
                        shouldDisappear = AdjustHitPoints(hitObject.quantity);
                        break;
                    default:
                        break;
                }

                if (shouldDisappear)
                collision.gameObject.SetActive(false);
            }
        }
    }

    public bool AdjustHitPoints(int amount)
    {
        if (hitPoints.value < maxHitPoints)
        {
            hitPoints.value = hitPoints.value + amount;

            print("Adjusted hitPoints by: " + amount + ". New Value: " + hitPoints.value);

            return true;
        }

        return false;
    }

    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while(true)
        {
            StartCoroutine(FlickerCharacter());
            hitPoints.value = hitPoints.value - damage;

            if (hitPoints.value <= float.Epsilon)
            {
                KillCharacter();
                break;
            }

            if (interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }
            else
            {
                break;
            }
        }
    }

    public override void KillCharacter()
    {
        base.KillCharacter();

        Destroy(healthbar.gameObject);
        Destroy(inventory.gameObject);
    }

    public override void ResetCharacter()
    {
        healthbar = Instantiate(healthBarPrefab);
        inventory = Instantiate(inventoryPrefab);
        healthbar.character = this;

        hitPoints.value = startingHitPoints;
    }

    private void OnEnable()
    {
        ResetCharacter();
    }
    
}
