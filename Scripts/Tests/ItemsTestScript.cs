using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TalesPop.Items;




public class ItemsTestScript
{
    // A Test behaves as an ordinary method
    [Test]
    public void NewTestScriptSimplePasses()
    {
        Debug.LogWarning("--- TEST BEGIN ---");
        // Use the Assert class to test conditions

        string armorJson = "{" +
            "\"uid\": 0," +
            "\"name\": \"some named armor\", " +
            "\"nameId\": 0, " +
            "\"itemType\": \"Armor\", " +
            "\"capacity\": 10" +
            "}";

        string potionJson1 = "{" +
            "\"uid\": 2," +
            "\"name\": \"some named potion1\", " +
            "\"nameId\": 2, " +
            "\"itemType\": \"Potion\", " +
            "\"capacity\": 10, " +
            "\"amount\": 6" +
            "}";

        string potionJson2 = "{" +
            "\"uid\": 3," +
            "\"name\": \"some named potion2\", " +
            "\"nameId\": 3, " +
            "\"itemType\": \"Potion\", " +
            "\"capacity\": 10, " +
            "\"amount\": 6" +
            "}";

        string bagJson1 = "{" +
            "\"uid\": 1," +
            "\"name\": \"some named bag1\", " +
            "\"nameId\": 1, " +
            "\"itemType\": \"Bag\", " +
            "\"capacity\": 10," +
            "\"inventoryType\": \"UniqueEquip\", " +
            $"\"contents\": [{armorJson}, {potionJson1}, {potionJson2}]" +
            "}";

        string armorJson2 = "{" +
            "\"uid\": 4," +
            "\"name\": \"some named armor2\", " +
            "\"nameId\": 0, " +
            "\"itemType\": \"Armor\", " +
            "\"capacity\": 10" +
            "}";

        string bagJson2 = "{" +
            "\"uid\": 5," +
            "\"name\": \"some named bag2\", " +
            "\"nameId\": 1, " +
            "\"itemType\": \"Bag\", " +
            "\"capacity\": 10," +
            "\"inventoryType\": \"Any\", " +
            $"\"contents\": [{armorJson}, {potionJson1}, {potionJson2}, {armorJson2}]" +
            "}";

        string bagJson3 = "{" +
            "\"uid\": 6," +
            "\"name\": \"some named bag3\", " +
            "\"nameId\": 1, " +
            "\"itemType\": \"Bag\", " +
            "\"capacity\": 10," +
            "\"inventoryType\": \"Any\", " +
            $"\"contents\": [{armorJson}, {potionJson1}, {potionJson2}, {armorJson2}]" +
            "}";
        
        string bagJson4 = "{" +
            "\"uid\": 7," +
            "\"name\": \"some named bag4\", " +
            "\"nameId\": 1, " +
            "\"itemType\": \"Bag\", " +
            "\"capacity\": 10," +
            "\"inventoryType\": \"UniqueEquip\", " +
            $"\"contents\": []" +
            "}";

        string bagJson5 = "{" +
            "\"uid\": 8," +
            "\"name\": \"some named bag5\", " +
            "\"nameId\": 1, " +
            "\"itemType\": \"Bag\", " +
            "\"capacity\": 10," +
            "\"inventoryType\": \"Any\", " +
            $"\"contents\": [{bagJson1}, {bagJson2}, {bagJson3}, {bagJson4}]" +
            "}";

        //Assert.That(() => {
        //    var item = new Stackable(json);
        //});



        ItemManager itemManager = new ItemManager();
        // Bag bag1 = itemManager.CreateBag(bagJson1);
        // Bag bag2 = itemManager.CreateBag(bagJson2);
        // Bag bag3 = itemManager.CreateBag(bagJson3);
        // Bag bag4 = itemManager.CreateBag(bagJson4);

        // Debug.LogWarning($"bag1 type = {bag1?.inventoryType}");
        // Debug.LogWarning($"bag2 type = {bag2?.inventoryType}");
        // Debug.LogWarning($"bag3 type = {bag3?.inventoryType}");
        // Debug.LogWarning($"bag4 type = {bag4?.inventoryType}");

        Bag bag5 = itemManager.CreateBag(bagJson5);
        Debug.LogWarning($"bag5 type = {bag5?.inventoryType}, contentCNT = {bag5?.container.Count}");

        DisplayBagContents(bag5);

        Bag bag1 = itemManager.CreateBag(bagJson1);
        Bag bag2 = itemManager.CreateBag(bagJson2);
        
        Item p1 = itemManager.Search(2);
        Item p2 = itemManager.Search(3);

        Debug.Log(p1?.name);
        Debug.Log(p2?.name);

        p1.Collide(p2);

        Debug.LogWarning("--- TEST END ---");    
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
                element.Value.Perform();
            }
        }
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    // [UnityTest]
    // public IEnumerator NewTestScriptWithEnumeratorPasses()
    // {
    //     // Use the Assert class to test conditions.
    //     // Use yield to skip a frame.
    //     yield return null;
    // }
}
