using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public Sprite itemSprite;
    public string itemDescription;
    public bool isKey;
    public bool isBasementKey;
    public bool isSeaKey;
    public bool isCastleKey;
    public bool isDungeonKey;
}
