using UnityEngine;

using MagmaLabs;
using MagmaLabs.Economy;

[CreateAssetMenu(fileName = "Item", menuName = "Economy/Item", order = 1)]
public class Item : SavableBase
{
    public SerializableDictionary<Sprite> sprites = new SerializableDictionary<Sprite>();

    public Sprite DefaultSprite
    {
        get
        {
            if (sprites == null || sprites.Count == 0)
            {
                return null;
            }

            if (sprites.ContainsKey("default")) return sprites["default"];
            if (sprites.ContainsKey("main")) return sprites["main"];
            if (sprites.ContainsKey("icon")) return sprites["icon"];

            return sprites.Items[0].value;
        }
        set
        {
            if (sprites == null)
            {
                sprites = new SerializableDictionary<Sprite>();
            }

            sprites.Set("default", value);
        }
    }

    public bool HasSprites => sprites != null && sprites.Count > 0;

    protected override void OnEnable()
    {
        base.OnEnable();
        EnsureItemInitialized();
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        EnsureItemInitialized();
    }

    public Item Copy()
    {
        Item newItem = CreateInstance<Item>();
        newItem.id = id;
        newItem.name = name;

        CopyDictionary(sprites, newItem.sprites);
        CopyDictionary(tags, newItem.tags);

        return newItem;
    }

    public Item Copy(string newId)
    {
        Item newItem = Copy();
        newItem.id = newId;
        return newItem;
    }

    private void EnsureItemInitialized()
    {
        if (sprites == null)
        {
            sprites = new SerializableDictionary<Sprite>();
        }
    }

    private static void CopyDictionary<T>(SerializableDictionary<T> source, SerializableDictionary<T> destination)
    {
        if (destination == null)
        {
            return;
        }

        destination.Clear();
        if (source == null)
        {
            return;
        }

        foreach (var kvp in source.Items)
        {
            destination.Set(kvp.key, kvp.value);
        }
    }
}
