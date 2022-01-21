using System;
using System.Collections.Generic;
using UnityEngine;
using TalesPop.Objects;
using TalesPop.Objects.Items;
using TalesPop.Datas;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Common;


public class D1
{
    public Dictionary<int, string> d1 = new Dictionary<int, string>
    {
        { 0, "zero" },
        { 1, "one" },
        { 2, "two" }
    };
}

public class D2
{
    public readonly Dictionary<int, string> d2;

    public D2(ref Dictionary<int, string> r)
    {
        d2 = r;
    }
}

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
        "\"name\": \"bag1 (Pouch overflow)\", " +
        "\"nameId\": 100, " +
        "\"itemType\": \"Pouch\", " +
        "\"capacity\": 5, " +
        "\"inventoryType\": \"Pouch\", " +
        $"\"contents\": [{armor1}, {twoHand1}, {weapon1}]" +
        "}";
    static string bag2 = "{" +
        "\"uid\": 101, " +
        "\"name\": \"bag2 (Pouch)\", " +
        "\"nameId\": 100, " +
        "\"itemType\": \"Pouch\", " +
        "\"capacity\": 5, " +
        "\"inventoryType\": \"Pouch\", " +
        $"\"contents\": [{armor1}, {weapon1}]" +
        "}";
    static string bag3 = "{" +
        "\"uid\": 102, " +
        "\"name\": \"bag3 (ExtraPouch overflow)\", " +
        "\"nameId\": 101, " +
        "\"itemType\": \"ExtraPouch\", " +
        "\"capacity\": 2, " +
        "\"inventoryType\": \"ExtraPouch\", " +
        $"\"contents\": [{potion1}, {potion2}, {armor2}]" +
        "}";
    static string bag4 = "{" +
        "\"uid\": 103, " +
        "\"name\": \"bag4 (ExtraPouch)\", " +
        "\"nameId\": 101, " +
        "\"itemType\": \"ExtraPouch\", " +
        "\"capacity\": 2, " +
        "\"inventoryType\": \"ExtraPouch\", " +
        $"\"contents\": [{potion3}, {potion4}]" +
        "}";
    static string bag5 = "{" +
        "\"uid\": 104, " +
        "\"name\": \"bag5 (Pouch)\", " +
        "\"nameId\": 100, " +
        "\"itemType\": \"Pouch\", " +
        "\"capacity\": 5, " +
        "\"inventoryType\": \"Pouch\", " +
        "\"contents\": []" +
        "}";
    static string bag6 = "{" +
        "\"uid\": 105, " +
        "\"name\": \"bag6 (ExtraPouch overflow)\", " +
        "\"nameId\": 101, " +
        "\"itemType\": \"ExtraPouch\", " +
        "\"capacity\": 2, " +
        "\"inventoryType\": \"ExtraPouch\", " +
        $"\"contents\": [{bag1}, {bag2}, {bag3}, {bag4}]" +
        "}";
    static string bag7 = "{" +
        "\"uid\": 106, " +
        "\"name\": \"bag7 (ExtraPouch)\", " +
        "\"nameId\": 101, " +
        "\"itemType\": \"ExtraPouch\", " +
        "\"capacity\": 2, " +
        "\"inventoryType\": \"ExtraPouch\", " +
        $"\"contents\": [{bag2}, {bag4}]" +
        "}";
    static string bag8 = "{" +
        "\"uid\": 107, " +
        "\"name\": \"bag8 (ExtraPouch)\", " +
        "\"nameId\": 101, " +
        "\"itemType\": \"ExtraPouch\", " +
        "\"capacity\": 2, " +
        "\"inventoryType\": \"ExtraPouch\", " +
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


        //TEST_MAKE_BAG1();

        //TEST_MAKE_BAG2();

        //TEST_INTERACT();
        //TEST_COLLIDE();

        //TEST_CONTAINER();

        D1 d1 = new D1();
        D2 d2 = new D2(ref d1.d1);

        Debug.Log($"d1.d1.Count = {d1.d1.Count}, d2.d2.Count = {d2.d2.Count}");
        d1.d1.Remove(0);
        Debug.Log($"d1.d1.Count = {d1.d1.Count}, d2.d2.Count = {d2.d2.Count}");
        d2.d2.Remove(1);
        Debug.Log($"d1.d1.Count = {d1.d1.Count}, d2.d2.Count = {d2.d2.Count}");
        d1.d1 = null;
        Debug.Log($"d1.d1.Count = {d1.d1?.Count}, d2.d2.Count = {d2.d2?.Count}");

        Debug.LogWarning("--- TEST END ---");
    }

    //private void TEST_CONTAINER()
    //{
    //    TalesPopContainer<int, Item> test_container = new TalesPopContainer<int, Item>();
    //    int uid1 = 101; // bag
    //    int uid2 = 106; // bag
    //    int uid3 = 0; // item
    //    int uid4 = 1; // item

    //    TEST_MAKE_BAG2();
    //    //itemManager.SHOW_BAG_CONTENTS(106);
    //    //test_container.AppendContainerType(typeof(Amulet));


    //    //test_container.AppendContainerType(typeof(Pouch));
    //    //test_container.AppendContainerType(typeof(ExtraPouch));
    //    test_container.AppendContainerType(typeof(Inventory));

    //    Item search1 = itemManager.SearchItem(uid1);
    //    Item search2 = itemManager.SearchItem(uid2);
    //    Item search3 = itemManager.SearchItem(uid3);
    //    Item search4 = itemManager.SearchItem(uid4);


    //    Debug.Log($"search1 = {search1}");
    //    Debug.Log($"search2 = {search2}");
    //    Debug.Log($"search3 = {search3}");
    //    Debug.Log($"search4 = {search4}");


    //    test_container.Add(search1.uid, search1);
    //    test_container.Add(search2.uid, search2);

    //    Item item = test_container.SearchBy(101, typeof(Inventory));
    //    //Debug.Log($"[result by my container] name = {item.name}, uid = {item.uid}, parent name = {item.SearchParentContainer().name}, parent uid = {item.SearchParentContainer().uid}");
    //    Debug.Log($"[result by my container] name = {item.name}, uid = {item.uid}");
    //    itemManager.SHOW_BAG_CONTENTS(item.uid);


    //    //itemManager.SHOW_BAG_CONTENTS(search1.uid);
    //    //itemManager.SHOW_BAG_CONTENTS(search2.uid);

    //    test_container.SHOW_ALL_CONTENTS();
    //    //Item search1 = itemManager.SearchItem(uid1);
    //    //Debug.Log($"search1 = {search1}");
    //    //Debug.Log($"search1 type = {search1.GetType()}");
    //    //Debug.Log($"search1 type type = {search1.GetType().GetType()}");

    //    //Item search2 = itemManager.SearchItem(uid2);
    //    //Debug.Log($"search2 = {search2}");
    //    //Debug.Log($"search2 type = {search2.GetType()}");
    //    //Debug.Log($"search2 type type = {search2.GetType().GetType()}");

    //    //Item search3 = itemManager.SearchItem(uid3);
    //    //Debug.Log($"search3 = {search3}");

    //    //Item search4 = itemManager.SearchItem(uid4);
    //    //Debug.Log($"search4 = {search4}");

    //}

    private void TEST_MAKE_BAG1()
    {
        Debug.LogWarning("TEST_MAKE_BAG1 begin");

        Inventory pouch = itemManager.CreateInventory(bag6);
        Debug.Log($">>>> bag6 type = {pouch?.inventoryType}, contentCNT = {pouch?.Occupied}");
        DisplayBagContents(pouch);
        Debug.LogWarning("TEST_MAKE_BAG1 end");
        Debug.Log("");
    }

    private void TEST_MAKE_BAG2()
    {
        Debug.LogWarning("TEST_MAKE_BAG2 begin");

        Inventory pouch = itemManager.CreateInventory(bag8);
        Debug.Log($">>>> bag8 type = {pouch?.inventoryType}, contentCNT = {pouch?.Occupied}");
        DisplayBagContents(pouch);
        Debug.LogWarning("TEST_MAKE_BAG2 end");
        Debug.Log("");
    }


    private void TEST_INTERACT()
    {
        Debug.LogWarning("TEST_INTERACT begin");

        Debug.LogWarning($"container count = {itemManager.SIZE()}");

        Inventory equip1 = itemManager.CreateInventory(bag2);
        Inventory equip2 = itemManager.CreateInventory(bag5);
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

        Inventory pouch1 = itemManager.CreateInventory(bag4);
        Inventory pouch2 = itemManager.CreateInventory(bag8);
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

        itemManager.SHOW_BAG_CONTENTS(a1?.SearchParentContainer().uid ?? 0);
        itemManager.SHOW_BAG_CONTENTS(p3?.SearchParentContainer().uid ?? 0);

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


    private void DisplayBagContents(Inventory bag) 
    {
        if (bag == null)
            return;

        foreach (KeyValuePair<int, Item> element in bag.CONTAINER)
        {
            if (element.Value == null)
                continue;

            Debug.Log($"name = {element.Value.name}");

            if (element.Value.itemType.Equals(ItemType.ExtraPouch)
                || element.Value.itemType.Equals(ItemType.Pouch))
            {
                Debug.LogWarning($"Found BAG! = {element.Value.name}");
                DisplayBagContents((Inventory)element.Value);
            }
            else
            {
                element.Value.Interact();
            }
        }
    }
}
