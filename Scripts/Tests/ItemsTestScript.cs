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
        // Use the Assert class to test conditions

        string armorJson = "{" +
            "\"uid\": 0," +
            "\"name\": \"some name\", " +
            "\"nameId\": 1, " +
            "\"category\": \"Armor\", " +
            "\"capacity\": 10" +
            "}";

        string bagJson = "{" +
            "\"uid\": 1," +
            "\"name\": \"some name\", " +
            "\"nameId\": 1, " +
            "\"category\": \"Bag\", " +
            "\"capacity\": 10" +
            // "\"contentArray\": [0, 2]" +
            "}";

        string potionJson1 = "{" +
            "\"uid\": 2," +
            "\"name\": \"some name\", " +
            "\"nameId\": 1, " +
            "\"category\": \"Potion\", " +
            "\"capacity\": 10" +
            // "\"amount\": 6" +
            "}";

        string potionJson2 = "{" +
            "\"uid\": 3," +
            "\"name\": \"some name\", " +
            "\"nameId\": 1, " +
            "\"category\": \"Potion\", " +
            "\"capacity\": 10" +
            // "\"amount\": 6" +
            "}";

        //Assert.That(() => {
        //    var item = new Stackable(json);
        //});



        ItemManager itemManager = new ItemManager();

        Item armor = itemManager.CreateItem(armorJson);
        Debug.Log(armor.uid);
        Debug.Log(armor.name);
        Debug.Log(armor.category);
        Debug.Log((int)armor.category);
        armor.Perform();

        Item bag = itemManager.CreateItem(bagJson);
        Debug.Log(bag.uid);
        Debug.Log(bag.name);
        Debug.Log(bag.category);
        Debug.Log((int)bag.category);
        bag.Perform();

        Item potion1 = itemManager.CreateItem(potionJson1);
        Debug.Log(potion1.uid);
        Debug.Log(potion1.name);
        Debug.Log(potion1.category);
        Debug.Log((int)potion1.category);
        potion1.Perform();

        Item potion2 = itemManager.CreateItem(potionJson2);
        Debug.Log(potion2.uid);
        Debug.Log(potion2.name);
        Debug.Log(potion2.category);
        Debug.Log((int)potion2.category);
        potion2.Perform();

        Debug.LogWarning("--- TEST END ---");

        // Debug.Log($"json = {json}");
        // string json = "{" +
        //     "\"uid\": 0," +
        //     "\"name\": \"some name\", " +
        //     "\"nameId\": 1, " +
        //     "\"category\": \"Armor\", " +
        //     "\"capacity\": 10" +
        //     // "\"maxCapacity\": 100" +
        //     "}";


        // ItemManager itemManager = new ItemManager();

        // Debug.LogWarning("---------------------");
        // Item m = itemManager.CreateItem(json);
        // Debug.Log(m.uid);
        // Debug.Log(m.name);
        // Debug.Log(m.category);
        // Debug.Log((int)m.category);


        // ItemManager itemManager = new ItemManager();

        Debug.LogWarning("---------------------");
        // Item m = itemManager.CreateItem(json);
        // Debug.Log(m.uid);
        // Debug.Log(m.name);
        // Debug.Log(m.category);
        // Debug.Log((int)m.category);
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
