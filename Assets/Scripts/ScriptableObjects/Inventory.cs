using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int numberOfKeys;
    public int numberOfBasementKeys;
    public int numberOfSeaKeys;
    public int numberOfCastleKeys;
    public int numberOfDungeonKeys;
    public int coins;
    public float maxMagic = 100;
    public float currentMagic;

    public void OnEnable()
    {
        currentMagic = maxMagic;
    }

    public void ReduceMagic(float magicCost)
    {
        currentMagic -= magicCost;
    }

    public bool CheckForItem(Item item)
    {
        if(items.Contains(item))
        {
            return true;
        }
        return false;
    }

    public void AddItem(Item itemToAdd)
    {
        // is the item a key
        if(itemToAdd.isKey)
        {
            numberOfKeys++;
        }
        else if(itemToAdd.isBasementKey)
        {
            numberOfBasementKeys++;
        }

        else if (itemToAdd.isSeaKey)
        {
            numberOfSeaKeys++;
        }

        else if (itemToAdd.isCastleKey)
        {
            numberOfCastleKeys++;
        }
        else if (itemToAdd.isDungeonKey)
        {
            numberOfDungeonKeys++;
        }

        else
        {
            if(!items.Contains(itemToAdd))
            {
                items.Add(itemToAdd);
            }
        }
    }

}
