using System.Collections.Generic;
using UnityEngine;
using TalesPop.Items;
using static Common;



public class Test : MonoBehaviour
{
    void Start()
    {
        Debug.LogWarning("--- TEST BEGIN ---");
        // Use the Assert class to test conditions


        //Assert.That(() => {
        //    var item = new Stackable(json);
        //});

        TEST_MAKE_BAG();
        TEST_INTERACT();
        TEST_COLLIDE();


        Debug.LogWarning("--- TEST END ---");
    }

    private void TEST_MAKE_BAG()
    {
        Debug.LogWarning("TEST_MAKE_BAG begin");
        string armorJson1 = "{\"uid\": 0, \"name\": \"some named armor1\", \"nameId\": 0, \"itemType\": \"Armor\", \"capacity\": 10}";
        string weaponJson = "{\"uid\": 10, \"name\": \"some named weapon\", \"nameId\": 4, \"itemType\": \"Weapon\", \"capacity\": 10}";
        string potionJson1 = "{\"uid\": 2, \"name\": \"some named potion1\", \"nameId\": 2, \"itemType\": \"Potion\", \"capacity\": 10, \"amount\": 6}";
        string potionJson2 = "{\"uid\": 3, \"name\": \"some named potion2\", \"nameId\": 2, \"itemType\": \"Potion\", \"capacity\": 10, \"amount\": 3}";
        string potionJson3 = "{\"uid\": 11, \"name\": \"some named potion3\", \"nameId\": 2, \"itemType\": \"Potion\", \"capacity\": 10, \"amount\": 6}";
        string potionJson4 = "{\"uid\": 12, \"name\": \"some named potion4\", \"nameId\": 3, \"itemType\": \"Potion\", \"capacity\": 10, \"amount\": 6}";
        string armorJson2 = "{\"uid\": 4, \"name\": \"some named armor2\", \"nameId\": 0, \"itemType\": \"Armor\", \"capacity\": 10}";
        string bagJson1 = $"{{\"uid\": 1, \"name\": \"some named bag1\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"UniqueEquip\", \"contents\": [{armorJson1}, {potionJson1}, {potionJson2}, {weaponJson}]}}";
        string bagJson2 = $"{{\"uid\": 5, \"name\": \"some named bag2\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Any\", \"contents\": [{armorJson1}, {potionJson1}, {potionJson2}, {armorJson2}, {potionJson3}, {potionJson4}]}}";
        string bagJson3 = $"{{\"uid\": 6, \"name\": \"some named bag3\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Any\", \"contents\": [{armorJson1}, {potionJson1}, {potionJson2}, {armorJson2}]}}";
        string bagJson4 = $"{{\"uid\": 7, \"name\": \"some named bag4\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"UniqueEquip\", \"contents\": []}}";
        string bagJson5 = $"{{\"uid\": 8, \"name\": \"some named bag5\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Any\", \"contents\": [{bagJson1}, {bagJson2}, {bagJson3}, {bagJson4}]}}";

        ItemManager itemManager = new ItemManager();

        Bag bag5 = itemManager.CreateBag(bagJson5);
        Debug.LogWarning($"bag5 type = {bag5?.inventoryType}, contentCNT = {bag5?.container.Count}");
        DisplayBagContents(bag5);
        Debug.LogWarning("TEST_MAKE_BAG end");
        Debug.Log("");
    }


    private void TEST_INTERACT()
    {
        Debug.LogWarning("TEST_INTERACT begin");
        string armorJson1 = "{\"uid\": 0, \"name\": \"some named armor1\", \"nameId\": 0, \"itemType\": \"Armor\", \"capacity\": 10}";
        string weaponJson = "{\"uid\": 10, \"name\": \"some named weapon\", \"nameId\": 4, \"itemType\": \"Weapon\", \"capacity\": 10}";
        string potionJson1 = "{\"uid\": 2, \"name\": \"some named potion1\", \"nameId\": 2, \"itemType\": \"Potion\", \"capacity\": 10, \"amount\": 6}";
        string potionJson2 = "{\"uid\": 3, \"name\": \"some named potion2\", \"nameId\": 2, \"itemType\": \"Potion\", \"capacity\": 10, \"amount\": 3}";
        string potionJson3 = "{\"uid\": 11, \"name\": \"some named potion3\", \"nameId\": 2, \"itemType\": \"Potion\", \"capacity\": 10, \"amount\": 6}";
        string potionJson4 = "{\"uid\": 12, \"name\": \"some named potion4\", \"nameId\": 3, \"itemType\": \"Potion\", \"capacity\": 10, \"amount\": 6}";
        string armorJson2 = "{\"uid\": 4, \"name\": \"some named armor2\", \"nameId\": 0, \"itemType\": \"Armor\", \"capacity\": 10}";
        string bagJson1 = $"{{\"uid\": 1, \"name\": \"some named bag1\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"UniqueEquip\", \"contents\": [{armorJson1}, {potionJson1}, {potionJson2}, {weaponJson}]}}";
        string bagJson2 = $"{{\"uid\": 5, \"name\": \"some named bag2\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Any\", \"contents\": [{armorJson1}, {potionJson1}, {potionJson2}, {armorJson2}, {potionJson3}, {potionJson4}]}}";
        string bagJson3 = $"{{\"uid\": 6, \"name\": \"some named bag3\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Any\", \"contents\": [{armorJson1}, {potionJson1}, {potionJson2}, {armorJson2}]}}";
        string bagJson4 = $"{{\"uid\": 7, \"name\": \"some named bag4\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"UniqueEquip\", \"contents\": []}}";
        string bagJson5 = $"{{\"uid\": 8, \"name\": \"some named bag5\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Any\", \"contents\": [{bagJson1}, {bagJson2}, {bagJson3}, {bagJson4}]}}";

        ItemManager itemManager = new ItemManager();

        Bag bag1 = itemManager.CreateBag(bagJson1);
        Bag bag2 = itemManager.CreateBag(bagJson2);

        Item p1 = itemManager.SearchByUID(2);
        Item p2 = itemManager.SearchByUID(10);

        bag1?.Interact();
        bag2?.Interact();

        p1?.Interact();
        p2?.Interact();
        Debug.LogWarning("TEST_INTERACT end");
        Debug.Log("");
    }


    private void TEST_COLLIDE()
    {
        Debug.LogWarning("TEST_COLLIDE begin");
        string armorJson1 = "{\"uid\": 0, \"name\": \"some named armor1\", \"nameId\": 0, \"itemType\": \"Armor\", \"capacity\": 10}";
        string weaponJson = "{\"uid\": 10, \"name\": \"some named weapon\", \"nameId\": 4, \"itemType\": \"Weapon\", \"capacity\": 10}";
        string potionJson1 = "{\"uid\": 2, \"name\": \"some named potion1\", \"nameId\": 2, \"itemType\": \"Potion\", \"capacity\": 10, \"amount\": 6}";
        string potionJson2 = "{\"uid\": 3, \"name\": \"some named potion2\", \"nameId\": 2, \"itemType\": \"Potion\", \"capacity\": 10, \"amount\": 3}";
        string potionJson3 = "{\"uid\": 11, \"name\": \"some named potion3\", \"nameId\": 2, \"itemType\": \"Potion\", \"capacity\": 10, \"amount\": 6}";
        string potionJson4 = "{\"uid\": 12, \"name\": \"some named potion4\", \"nameId\": 3, \"itemType\": \"Potion\", \"capacity\": 10, \"amount\": 6}";
        string armorJson2 = "{\"uid\": 4, \"name\": \"some named armor2\", \"nameId\": 0, \"itemType\": \"Armor\", \"capacity\": 10}";
        string bagJson1 = $"{{\"uid\": 1, \"name\": \"some named bag1\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"UniqueEquip\", \"contents\": [{armorJson1}, {potionJson1}, {potionJson2}, {weaponJson}]}}";
        string bagJson2 = $"{{\"uid\": 5, \"name\": \"some named bag2\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Any\", \"contents\": [{armorJson1}, {potionJson1}, {potionJson2}, {armorJson2}, {potionJson3}, {potionJson4}]}}";
        string bagJson3 = $"{{\"uid\": 6, \"name\": \"some named bag3\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Any\", \"contents\": [{armorJson1}, {potionJson1}, {potionJson2}, {armorJson2}]}}";
        string bagJson4 = $"{{\"uid\": 7, \"name\": \"some named bag4\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"UniqueEquip\", \"contents\": []}}";
        string bagJson5 = $"{{\"uid\": 8, \"name\": \"some named bag5\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Any\", \"contents\": [{bagJson1}, {bagJson2}, {bagJson3}, {bagJson4}]}}";

        ItemManager itemManager = new ItemManager();

        Bag bag1 = itemManager.CreateBag(bagJson1);
        Bag bag2 = itemManager.CreateBag(bagJson2);


        Item p1 = itemManager.SearchByUID(2);
        Item p2 = itemManager.SearchByUID(3);

        Item a = itemManager.SearchByUID(0);
        Item w = itemManager.SearchByUID(10);

        Debug.Log(p1?.name);
        Debug.Log(p2?.name);

        p1?.Collide(p2);
        //a.Collide(w);
        //w.Collide(p1);
        Debug.LogWarning("TEST_COLLIDE end");
        Debug.Log("");
    }


    private void DisplayBagContents(Bag bag) 
    {
        if (bag == null)
            return;

        foreach (KeyValuePair<int, Item> element in bag.container)
        {
            if (element.Value == null)
                continue;

            Debug.Log($"name = {element.Value.name}");

            if (element.Value.itemType.Equals(ItemType.Bag))
            {
                Debug.LogWarning($"Found BAG! = {element.Value.name}");
                DisplayBagContents((Bag)element.Value);
            }
            else
            {
                element.Value.Interact();
            }
        }
    }
}
