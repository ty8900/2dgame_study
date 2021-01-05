using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public HealthBar healthBarPrefab;
    HealthBar healthbar;

    public Inventory inventoryPrefab;
    Inventory inventory;

    public void Start()
    {
        hitPoints.value = startingHitPoints;

        healthbar = Instantiate(healthBarPrefab);
        inventory = Instantiate(inventoryPrefab);
        healthbar.character = this;
    }

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
}
