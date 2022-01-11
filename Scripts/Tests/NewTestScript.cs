using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TalesPop.Items;



public class NewTestScript
{
    // A Test behaves as an ordinary method
    [Test]
    public void NewTestScriptSimplePasses()
    {
        Debug.LogWarning("--- TEST BEGIN ---");
        // Use the Assert class to test conditions
        // Use the Assert class to test conditions
        string json = "{" +
            "\"uid\": 0," +
            "\"name\": \"some name\", " +
            "\"nameId\": 1, " +
            "\"category\": \"Armor\", " +
            "\"capacity\": 10, " +
            "\"maxCapacity\": 100" +
            "}";

        //Assert.That(() => {
        //    var item = new Stackable(json);
        //});


        Item item = new Stackable(json);

        Debug.Log(item.uid);
        Debug.Log(item.name);
        Debug.Log(item.category);
        Debug.Log((int)item.category);


        ItemManager itemManager = new ItemManager();
        Item m = itemManager.CreateItem(json);
        Debug.Log(m.uid);
        Debug.Log(m.name);
        Debug.Log(m.category);
        Debug.Log((int)m.category);

        Debug.LogWarning("--- TEST END ---");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
