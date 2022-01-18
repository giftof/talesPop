using System.Collections.Generic;
using UnityEngine;
using TalesPop.Objects;
using TalesPop.Objects.Items;
using static Common;



public class Test : MonoBehaviour
{
    static string armor1 = "{" +
        "\"uid\": 0, " +
        "\"name\": \"armor1\", " +
        "\"nameId\": 0, " +
        "\"iconId\": 0, " +
        "\"itemType\": \"Armor\", " +
        "\"spellID\": 0, " +
        "\"capacity\": 10" +
        "}";
    static string weapon1 = "{" +
        "\"uid\": 1, " +
        "\"name\": \"weapon1\", " +
        "\"nameId\": 1, " +
        "\"iconId\": 1, " +
        "\"itemType\": \"Weapon\"" +
        "}";
    static string twoHand1 = "{" +
        "\"uid\": 2, " +
        "\"name\": \"twoHand1\", " +
        "\"nameId\": 2, " +
        "\"iconId\": 2, " +
        "\"itemType\": \"TwoHand\", " +
        "\"spellID\": 1, " +
        "\"capacity\": 10" +
        "}";
    static string armor2 = "{" +
        "\"uid\": 3, " +
        "\"name\": \"armor2\", " +
        "\"nameId\": 0, " +
        "\"iconId\": 3, " +
        "\"itemType\": \"Armor\"" +
        "}";
    static string potion1 = "{" +
        "\"uid\": 4, " +
        "\"name\": \"potion1(3:6)\", " +
        "\"nameId\": 3, " +
        "\"itemType\": \"Potion\", " +
        "\"capacity\": 10, " +
        "\"amount\": 6" +
        "}";
    static string potion2 = "{" +
        "\"uid\": 5, " +
        "\"name\": \"potion2(3:3)\", " +
        "\"nameId\": 3, " +
        "\"itemType\": \"Potion\", " +
        "\"capacity\": 10, " +
        "\"amount\": 3" +
        "}";
    static string potion3 = "{" +
        "\"uid\": 6, " +
        "\"name\": \"potion3(3:6)\", " +
        "\"nameId\": 3, " +
        "\"itemType\": \"Potion\", " +
        "\"capacity\": 10, " +
        "\"amount\": 6" +
        "}";
    static string potion4 = "{" +
        "\"uid\": 7, " +
        "\"name\": \"potion4(4:6)\", " +
        "\"nameId\": 4, " +
        "\"itemType\": \"Potion\", " +
        "\"capacity\": 10, " +
        "\"amount\": 6" +
        "}";

    static string bag1 = "{" +
        "\"uid\": 100, " +
        "\"name\": \"bag1 (equip slot duplicated)\", " +
        "\"nameId\": 100, " +
        "\"itemType\": \"Bag\", " +
        "\"capacity\": 5, " +
        "\"inventoryType\": \"Equip\", " +
        $"\"contents\": [{armor1}, {twoHand1}, {weapon1}]" +
        "}";
    static string bag2 = "{" +
        "\"uid\": 101, " +
        "\"name\": \"bag2 (equip)\", " +
        "\"nameId\": 100, " +
        "\"itemType\": \"Bag\", " +
        "\"capacity\": 5, " +
        "\"inventoryType\": \"Equip\", " +
        $"\"contents\": [{armor1}, {weapon1}]" +
        "}";
    static string bag3 = "{" +
        "\"uid\": 102, " +
        "\"name\": \"bag3 (pouch overflow)\", " +
        "\"nameId\": 101, " +
        "\"itemType\": \"Bag\", " +
        "\"capacity\": 2, " +
        "\"inventoryType\": \"Pouch\", " +
        $"\"contents\": [{potion1}, {potion2}, {armor2}]" +
        "}";
    static string bag4 = "{" +
        "\"uid\": 103, " +
        "\"name\": \"bag4 (pouch)\", " +
        "\"nameId\": 101, " +
        "\"itemType\": \"Bag\", " +
        "\"capacity\": 2, " +
        "\"inventoryType\": \"Pouch\", " +
        $"\"contents\": [{potion3}, {potion4}]" +
        "}";
    static string bag5 = "{" +
        "\"uid\": 104, " +
        "\"name\": \"bag5 (equip)\", " +
        "\"nameId\": 100, " +
        "\"itemType\": \"Bag\", " +
        "\"capacity\": 5, " +
        "\"inventoryType\": \"Equip\", " +
        "\"contents\": []" +
        "}";
    static string bag6 = "{" +
        "\"uid\": 105, " +
        "\"name\": \"bag6 (pouch overflow)\", " +
        "\"nameId\": 101, " +
        "\"itemType\": \"Bag\", " +
        "\"capacity\": 2, " +
        "\"inventoryType\": \"Pouch\", " +
        $"\"contents\": [{bag1}, {bag2}, {bag3}, {bag4}]" +
        "}";
    static string bag7 = "{" +
        "\"uid\": 106, " +
        "\"name\": \"bag7 (extra)\", " +
        "\"nameId\": 101, " +
        "\"itemType\": \"Bag\", " +
        "\"capacity\": 2, " +
        "\"inventoryType\": \"Extra\", " +
        $"\"contents\": [{bag2}, {bag4}]" +
        "}";
    static string bag8 = "{" +
        "\"uid\": 107, " +
        "\"name\": \"bag8 (pouch)\", " +
        "\"nameId\": 101, " +
        "\"itemType\": \"Bag\", " +
        "\"capacity\": 2, " +
        "\"inventoryType\": \"Pouch\", " +
        $"\"contents\": [{bag7}, ]" +
        "}";

    ItemManager itemManager = new ItemManager();

    void Start()
    {
        Debug.LogWarning("--- TEST BEGIN ---");
        // Use the Assert class to test conditions


        //Assert.That(() => {
        //    var item = new Stackable(json);
        //});

        TEST_MAKE_BAG1();
        TEST_MAKE_BAG2();
        TEST_INTERACT();
        TEST_COLLIDE();


        Debug.LogWarning("--- TEST END ---");
    }

    private void TEST_MAKE_BAG1()
    {
        Debug.LogWarning("TEST_MAKE_BAG1 begin");
        Bag pouch = itemManager.CreateBag(bag6);
        Debug.Log($">>>> bag6 type = {pouch?.inventoryType}, contentCNT = {pouch?.Occupied}");
        DisplayBagContents(pouch);
        Debug.LogWarning("TEST_MAKE_BAG1 end");
        Debug.Log("");
    }

    private void TEST_MAKE_BAG2()
    {
        Debug.LogWarning("TEST_MAKE_BAG2 begin");

        Bag pouch = itemManager.CreateBag(bag8);
        Debug.Log($">>>> bag8 type = {pouch?.inventoryType}, contentCNT = {pouch?.Occupied}");
        DisplayBagContents(pouch);
        Debug.LogWarning("TEST_MAKE_BAG2 end");
        Debug.Log("");
    }


    private void TEST_INTERACT()
    {
        Debug.LogWarning("TEST_INTERACT begin");

        Debug.LogWarning($"container count = {itemManager.SIZE()}");

        Bag equip1 = itemManager.CreateBag(bag2);
        Bag equip2 = itemManager.CreateBag(bag5);
        Debug.Log($">>>> bag2 type = {equip1?.inventoryType}, contentCNT = {equip1?.Occupied}");
        Debug.Log($">>>> bag5 type = {equip2?.inventoryType}, contentCNT = {equip2?.Occupied}");

        Debug.LogWarning($"container count = {itemManager.SIZE()}");

        Item p1 = itemManager.SearchItem(4);
        Item p2 = itemManager.SearchItem(5);

        equip1?.Interact();
        equip2?.Interact();

        p1?.Interact();
        p2?.Interact();
        Debug.LogWarning("TEST_INTERACT end");
        Debug.Log("");
    }


    private void TEST_COLLIDE()
    {
        Debug.LogWarning("TEST_COLLIDE begin");

        Debug.LogWarning($"container count = {itemManager.SIZE()}");

        Bag pouch1 = itemManager.CreateBag(bag4);
        Bag pouch2 = itemManager.CreateBag(bag8);
        Debug.Log($">>>> bag4 type = {pouch1?.inventoryType}, contentCNT = {pouch1?.Occupied}");
        Debug.Log($">>>> bag8 type = {pouch2?.inventoryType}, contentCNT = {pouch2?.Occupied}");

        Debug.LogWarning($"container count = {itemManager.SIZE()}");

        Item p1 = itemManager.SearchItem(4);
        Item p2 = itemManager.SearchItem(5);
        Item p3 = itemManager.SearchItem(6);

        Item a1 = itemManager.SearchItem(0);
        Item a2 = itemManager.SearchItem(3);

        Debug.LogWarning($"p1?.name = {p1?.name}, p1?.uid = {p1?.uid}, p1?.Occupied = {p1?.Occupied}, p1?.groupId = {p1?.groupId}, p1?.slotId = {p1?.slotId}");
        Debug.LogWarning($"p2?.name = {p2?.name}, p2?.uid = {p2?.uid}, p2?.Occupied = {p2?.Occupied}, p2?.groupId = {p2?.groupId}, p2?.slotId = {p2?.slotId}");
        Debug.LogWarning($"p3?.name = {p3?.name}, p3?.uid = {p3?.uid}, p3?.Occupied = {p3?.Occupied}, p3?.groupId = {p3?.groupId}, p3?.slotId = {p3?.slotId}");

        p1?.Collide(p2);
        Debug.LogWarning($"p1?.name = {p1?.name}, p1?.uid = {p1?.uid}, p1?.Occupied = {p1?.Occupied}, p1?.groupId = {p1?.groupId}, p1?.slotId = {p1?.slotId}");
        Debug.LogWarning($"p2?.name = {p2?.name}, p2?.uid = {p2?.uid}, p2?.Occupied = {p2?.Occupied}, p2?.groupId = {p2?.groupId}, p2?.slotId = {p2?.slotId}");
        Debug.LogWarning($"p3?.name = {p3?.name}, p3?.uid = {p3?.uid}, p3?.Occupied = {p3?.Occupied}, p3?.groupId = {p3?.groupId}, p3?.slotId = {p3?.slotId}");

        Item remove = itemManager.SearchItem(5);
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
