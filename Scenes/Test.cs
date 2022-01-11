using UnityEngine;
using TalesPop.Items;



public class Test : MonoBehaviour
{
    void Start()
    {
        string json = "{" +
            "\"uid\": 0," +
            "\"name\": \"some name\", " +
            "\"nameId\": 1, " +
            "\"category\": \"Armor\", " +
            "\"capacity\": 10, " +
            "\"maxCapacity\": 100" +
            "}";
        Debug.Log($"json = {json}");
        Item item = new Stackable(json);

        Debug.Log(item.uid);
        Debug.Log(item.name);
        Debug.Log(item.category);
        Debug.Log((int)item.category);

        ItemManager itemManager = new ItemManager();

        Debug.LogWarning("---------------------");
        Item m = itemManager.CreateItem(json);
        Debug.Log(m.uid);
        Debug.Log(m.name);
        Debug.Log(m.category);
        Debug.Log((int)m.category);
    }
}
