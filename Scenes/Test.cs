using System.Collections.Generic;
using UnityEngine;
using TalesPop.Objects;
using TalesPop.Objects.Items;
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

        //TEST_MAKE_BAG1();
        TEST_MAKE_BAG2();
        //TEST_INTERACT();
        TEST_COLLIDE();


        Debug.LogWarning("--- TEST END ---");
    }

    private void TEST_MAKE_BAG1()
    {
        Debug.LogWarning("TEST_MAKE_BAG1 begin");
        string armorJson1 = "{\"uid\": 0, \"name\": \"some named armor1\", \"nameId\": 0, \"itemType\": \"Armor\", \"spellUIDArray\": [231, 123], \"capacity\": 10}";
        string weaponJson = "{\"uid\": 10, \"name\": \"some named weapon\", \"nameId\": 4, \"itemType\": \"Weapon\", \"capacity\": 10}";
        string twoHandJson = "{\"uid\": 13, \"name\": \"some named twoHand\", \"nameId\": 5, \"itemType\": \"TwoHand\", \"capacity\": 10}";
        string potionJson1 = "{\"uid\": 2, \"name\": \"some named potion1\", \"nameId\": 2, \"itemType\": \"Potion\", \"capacity\": 10, \"amount\": 6}";
        string potionJson2 = "{\"uid\": 3, \"name\": \"some named potion2\", \"nameId\": 2, \"itemType\": \"Potion\", \"capacity\": 10, \"amount\": 3}";
        string potionJson3 = "{\"uid\": 11, \"name\": \"some named potion3\", \"nameId\": 2, \"itemType\": \"Potion\", \"capacity\": 10, \"amount\": 6}";
        string potionJson4 = "{\"uid\": 12, \"name\": \"some named potion4\", \"nameId\": 3, \"itemType\": \"Potion\", \"capacity\": 10, \"amount\": 6}";
        string armorJson2 = "{\"uid\": 4, \"name\": \"some named armor2\", \"nameId\": 0, \"itemType\": \"Armor\", \"capacity\": 10}";
        string bagJson1 = $"{{\"uid\": 1, \"name\": \"some named bag1\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Equip\", \"contents\": [{armorJson1}, {twoHandJson}, {weaponJson}]}}";
        string bagJson2 = $"{{\"uid\": 5, \"name\": \"some named bag2\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Any\", \"contents\": [{potionJson1}, {potionJson2}, {armorJson2}]}}";
        string bagJson3 = $"{{\"uid\": 6, \"name\": \"some named bag3\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Any\", \"contents\": [{potionJson3}, {potionJson4}]}}";
        string bagJson4 = $"{{\"uid\": 7, \"name\": \"some named bag4\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Equip\", \"contents\": []}}";
        string bagJson5 = $"{{\"uid\": 8, \"name\": \"some named bag5\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Any\", \"contents\": [{potionJson4}, {bagJson1}, {bagJson2}, {bagJson3}, {bagJson4}]}}";

        ItemManager itemManager = new ItemManager();

        Bag bag5 = itemManager.CreateBag(bagJson5);
        Debug.Log($">>>> bag5 type = {bag5?.inventoryType}, contentCNT = {bag5?.Occupied}");
        DisplayBagContents(bag5);
        Debug.LogWarning("TEST_MAKE_BAG1 end");
        Debug.Log("");
    }

    private void TEST_MAKE_BAG2()
    {
        Debug.LogWarning("TEST_MAKE_BAG2 begin");
        string armorJson1 = "{\"uid\": 0, \"name\": \"some named armor1\", \"nameId\": 0, \"itemType\": \"Armor\", \"spellUIDArray\": [231, 123], \"capacity\": 10}";
        string weaponJson = "{\"uid\": 10, \"name\": \"some named weapon\", \"nameId\": 4, \"itemType\": \"Weapon\", \"capacity\": 10}";
        string twoHandJson = "{\"uid\": 13, \"name\": \"some named twoHand\", \"nameId\": 5, \"itemType\": \"TwoHand\", \"capacity\": 10}";
        string potionJson1 = "{\"uid\": 2, \"name\": \"some named potion1\", \"nameId\": 2, \"itemType\": \"Potion\", \"capacity\": 10, \"amount\": 6}";
        string potionJson2 = "{\"uid\": 3, \"name\": \"some named potion2\", \"nameId\": 2, \"itemType\": \"Potion\", \"capacity\": 10, \"amount\": 3}";
        string potionJson3 = "{\"uid\": 11, \"name\": \"some named potion3\", \"nameId\": 2, \"itemType\": \"Potion\", \"capacity\": 10, \"amount\": 6}";
        string potionJson4 = "{\"uid\": 12, \"name\": \"some named potion4\", \"nameId\": 3, \"itemType\": \"Potion\", \"capacity\": 10, \"amount\": 6}";
        string armorJson2 = "{\"uid\": 4, \"name\": \"some named armor2\", \"nameId\": 0, \"itemType\": \"Armor\", \"capacity\": 10}";
        string bagJson1 = $"{{\"uid\": 1, \"name\": \"some named bag1\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Equip\", \"contents\": [{armorJson1}, {weaponJson}]}}";
        string bagJson2 = $"{{\"uid\": 5, \"name\": \"some named bag2\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Any\", \"contents\": [{potionJson1}, {potionJson2}, {armorJson2}]}}";
        string bagJson3 = $"{{\"uid\": 6, \"name\": \"some named bag3\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Any\", \"contents\": [{potionJson3}, {potionJson4}]}}";
        string bagJson4 = $"{{\"uid\": 7, \"name\": \"some named bag4\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Equip\", \"contents\": []}}";
        string bagJson5 = $"{{\"uid\": 8, \"name\": \"some named bag5\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Any\", \"contents\": [{bagJson1}, {bagJson2}, {bagJson3}, {bagJson4}]}}";

        ItemManager itemManager = new ItemManager();

        Bag bag5 = itemManager.CreateBag(bagJson5);
        Debug.Log($">>>> bag5 type = {bag5?.inventoryType}, contentCNT = {bag5?.Occupied}");
        DisplayBagContents(bag5);
        Debug.LogWarning("TEST_MAKE_BAG2 end");
        Debug.Log("");
    }


    private void TEST_INTERACT()
    {
        Debug.LogWarning("TEST_INTERACT begin");
        string armorJson1 = "{\"uid\": 0, \"name\": \"some named armor1\", \"nameId\": 0, \"itemType\": \"Armor\", \"spellUIDArray\": [231, 123], \"capacity\": 10}";
        string weaponJson = "{\"uid\": 10, \"name\": \"some named weapon\", \"nameId\": 4, \"itemType\": \"Weapon\", \"capacity\": 10}";
        string twoHandJson = "{\"uid\": 13, \"name\": \"some named twoHand\", \"nameId\": 5, \"itemType\": \"TwoHand\", \"capacity\": 10}";
        string potionJson1 = "{\"uid\": 2, \"name\": \"some named potion1\", \"nameId\": 2, \"itemType\": \"Potion\", \"capacity\": 10, \"amount\": 6}";
        string potionJson2 = "{\"uid\": 3, \"name\": \"some named potion2\", \"nameId\": 2, \"itemType\": \"Potion\", \"capacity\": 10, \"amount\": 3}";
        string potionJson3 = "{\"uid\": 11, \"name\": \"some named potion3\", \"nameId\": 2, \"itemType\": \"Potion\", \"capacity\": 10, \"amount\": 6}";
        string potionJson4 = "{\"uid\": 12, \"name\": \"some named potion4\", \"nameId\": 3, \"itemType\": \"Potion\", \"capacity\": 10, \"amount\": 6}";
        string armorJson2 = "{\"uid\": 4, \"name\": \"some named armor2\", \"nameId\": 0, \"itemType\": \"Armor\", \"capacity\": 10}";
        string bagJson1 = $"{{\"uid\": 1, \"name\": \"some named bag1\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Equip\", \"contents\": [{armorJson1}, {weaponJson}]}}";
        string bagJson2 = $"{{\"uid\": 5, \"name\": \"some named bag2\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Any\", \"contents\": [{potionJson1}, {potionJson2}, {armorJson2}]}}";
        string bagJson3 = $"{{\"uid\": 6, \"name\": \"some named bag3\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Any\", \"contents\": [{potionJson3}, {potionJson4}]}}";
        string bagJson4 = $"{{\"uid\": 7, \"name\": \"some named bag4\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Equip\", \"contents\": []}}";
        string bagJson5 = $"{{\"uid\": 8, \"name\": \"some named bag5\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Any\", \"contents\": [{bagJson1}, {bagJson2}, {bagJson3}, {bagJson4}]}}";

        ItemManager itemManager = new ItemManager();

        Debug.LogError($"container count = {itemManager.SIZE()}");

        Bag bag1 = itemManager.CreateBag(bagJson1);
        Bag bag2 = itemManager.CreateBag(bagJson2);
        Debug.Log($">>>> bag1 type = {bag1?.inventoryType}, contentCNT = {bag1?.Occupied}");
        Debug.Log($">>>> bag2 type = {bag2?.inventoryType}, contentCNT = {bag2?.Occupied}");

        Debug.LogError($"container count = {itemManager.SIZE()}");

        Item p1 = itemManager.SearchItem(2);
        Item p2 = itemManager.SearchItem(10);

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
        string armorJson1 = "{\"uid\": 0, \"name\": \"some named armor1\", \"nameId\": 0, \"itemType\": \"Armor\", \"spellUIDArray\": [231, 123], \"capacity\": 10}";
        string weaponJson = "{\"uid\": 10, \"name\": \"some named weapon\", \"nameId\": 4, \"itemType\": \"Weapon\", \"capacity\": 10}";
        string twoHandJson = "{\"uid\": 13, \"name\": \"some named twoHand\", \"nameId\": 5, \"itemType\": \"TwoHand\", \"capacity\": 10}";
        string potionJson1 = "{\"uid\": 2, \"name\": \"some named potion1\", \"nameId\": 2, \"itemType\": \"Potion\", \"capacity\": 10, \"amount\": 6}";
        string potionJson2 = "{\"uid\": 3, \"name\": \"some named potion2\", \"nameId\": 2, \"itemType\": \"Potion\", \"capacity\": 10, \"amount\": 3}";
        string potionJson3 = "{\"uid\": 11, \"name\": \"some named potion3\", \"nameId\": 2, \"itemType\": \"Potion\", \"capacity\": 10, \"amount\": 6}";
        string potionJson4 = "{\"uid\": 12, \"name\": \"some named potion4\", \"nameId\": 3, \"itemType\": \"Potion\", \"capacity\": 10, \"amount\": 6}";
        string armorJson2 = "{\"uid\": 4, \"name\": \"some named armor2\", \"nameId\": 0, \"itemType\": \"Armor\", \"capacity\": 10}";
        string bagJson1 = $"{{\"uid\": 1, \"name\": \"some named bag1\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Equip\", \"contents\": [{armorJson1}, {weaponJson}]}}";
        string bagJson2 = $"{{\"uid\": 5, \"name\": \"some named bag2\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Any\", \"contents\": [{potionJson1}, {potionJson2}, {armorJson2}]}}";
        string bagJson3 = $"{{\"uid\": 6, \"name\": \"some named bag3\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Any\", \"contents\": [{potionJson3}, {potionJson4}]}}";
        string bagJson4 = $"{{\"uid\": 7, \"name\": \"some named bag4\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Equip\", \"contents\": []}}";
        string bagJson5 = $"{{\"uid\": 8, \"name\": \"some named bag5\", \"nameId\": 1, \"itemType\": \"Bag\", \"capacity\": 10, \"inventoryType\": \"Any\", \"contents\": [{bagJson1}, {bagJson2}, {bagJson3}, {bagJson4}]}}";

        ItemManager itemManager = new ItemManager();

        Debug.LogWarning($"container count = {itemManager.SIZE()}");

        Bag bag1 = itemManager.CreateBag(bagJson1);
        Bag bag2 = itemManager.CreateBag(bagJson2);
        Debug.Log($">>>> bag1 type = {bag1?.inventoryType}, contentCNT = {bag1?.Occupied}");
        Debug.Log($">>>> bag2 type = {bag2?.inventoryType}, contentCNT = {bag2?.Occupied}");

        Debug.LogWarning($"container count = {itemManager.SIZE()}");

        Item p1 = itemManager.SearchItem(2);
        Item p2 = itemManager.SearchItem(3);
        Item p3 = itemManager.SearchItem(11);

        Item a1 = itemManager.SearchItem(0);
        Item a2 = itemManager.SearchItem(4);

        Debug.LogWarning($"p1?.name = {p1?.name}, p1?.uid = {p1?.uid}, p1?.Occupied = {p1?.Occupied}, p1?.groupId = {p1?.groupId}, p1?.slotId = {p1?.slotId}");
        Debug.LogWarning($"p2?.name = {p2?.name}, p2?.uid = {p2?.uid}, p2?.Occupied = {p2?.Occupied}, p2?.groupId = {p2?.groupId}, p2?.slotId = {p2?.slotId}");
        Debug.LogWarning($"p3?.name = {p3?.name}, p3?.uid = {p3?.uid}, p3?.Occupied = {p3?.Occupied}, p3?.groupId = {p3?.groupId}, p3?.slotId = {p3?.slotId}");

        p1?.Collide(p2);
        Debug.LogWarning($"p1?.name = {p1?.name}, p1?.uid = {p1?.uid}, p1?.Occupied = {p1?.Occupied}, p1?.groupId = {p1?.groupId}, p1?.slotId = {p1?.slotId}");
        Debug.LogWarning($"p2?.name = {p2?.name}, p2?.uid = {p2?.uid}, p2?.Occupied = {p2?.Occupied}, p2?.groupId = {p2?.groupId}, p2?.slotId = {p2?.slotId}");
        Debug.LogWarning($"p3?.name = {p3?.name}, p3?.uid = {p3?.uid}, p3?.Occupied = {p3?.Occupied}, p3?.groupId = {p3?.groupId}, p3?.slotId = {p3?.slotId}");

        Item remove = itemManager.SearchItem(3);
        Debug.LogWarning($"remove is Exist? [{remove}]");

        p1?.Collide(p3);
        Debug.LogWarning($"p1?.name = {p1?.name}, p1?.uid = {p1?.uid}, p1?.Occupied = {p1?.Occupied}, p1?.groupId = {p1?.groupId}, p1?.slotId = {p1?.slotId}");
        Debug.LogWarning($"p3?.name = {p3?.name}, p3?.uid = {p3?.uid}, p3?.Occupied = {p3?.Occupied}, p3?.groupId = {p3?.groupId}, p3?.slotId = {p3?.slotId}");

        Debug.Log("");

        Debug.LogWarning($"a1?.name = {a1?.name}, a1?.uid = {a1?.uid}, a1?.Occupied = {a1?.Occupied}, a1?.groupId = {a1?.groupId}, a1?.slotId = {a1?.slotId}");
        Debug.LogWarning($"p3?.name = {p3?.name}, p3?.uid = {p3?.uid}, p3?.Occupied = {p3?.Occupied}, p3?.groupId = {p3?.groupId}, p3?.slotId = {p3?.slotId}");

        itemManager.SHOW_BAG_CONTENTS(a1.SearchParentContainer().uid);
        itemManager.SHOW_BAG_CONTENTS(p3.SearchParentContainer().uid);

        a1.Collide(p3);
        Debug.LogWarning($"a1?.name = {a1?.name}, a1?.uid = {a1?.uid}, a1?.Occupied = {a1?.Occupied}, a1?.groupId = {a1?.groupId}, a1?.slotId = {a1?.slotId}");
        Debug.LogWarning($"p3?.name = {p3?.name}, p3?.uid = {p3?.uid}, p3?.Occupied = {p3?.Occupied}, p3?.groupId = {p3?.groupId}, p3?.slotId = {p3?.slotId}");

        itemManager.SHOW_BAG_CONTENTS(a1.SearchParentContainer().uid);
        itemManager.SHOW_BAG_CONTENTS(p3.SearchParentContainer().uid);

        Debug.Log("");
        Debug.Log("");

        itemManager.SHOW_BAG_CONTENTS(a1.SearchParentContainer().uid);
        itemManager.SHOW_BAG_CONTENTS(p3.SearchParentContainer().uid);

        Debug.LogWarning("a1' parent(Bag) <-> p3");
        a1.SearchParentContainer().Collide(p3);

        itemManager.SHOW_BAG_CONTENTS(a1.SearchParentContainer().uid);
        itemManager.SHOW_BAG_CONTENTS(p3.SearchParentContainer().uid);
        itemManager.SHOW_BAG_CONTENTS(1);

        Debug.LogWarning("TEST_COLLIDE end");
        Debug.Log("");
    }


    private void DisplayBagContents(Bag bag) 
    {
        if (bag == null)
            return;

        foreach (KeyValuePair<int, Item> element in bag.CONTAINER)
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
