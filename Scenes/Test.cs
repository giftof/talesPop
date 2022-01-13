using UnityEngine;
using TalesPop.Items;



public class Test : MonoBehaviour
{
    void Start()
    {
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
            "\"capacity\": 10" +
            // "\"amount\": 6" +
            "}";

        string potionJson2 = "{" +
            "\"uid\": 3," +
            "\"name\": \"some named potion2\", " +
            "\"nameId\": 3, " +
            "\"itemType\": \"Potion\", " +
            "\"capacity\": 10" +
            // "\"amount\": 6" +
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
            "\"uid\": 0," +
            "\"name\": \"some named armor2\", " +
            "\"nameId\": 0, " +
            "\"itemType\": \"Armor\", " +
            "\"capacity\": 10" +
            "}";

        string bagJson2 = "{" +
            "\"uid\": 1," +
            "\"name\": \"some named bag2\", " +
            "\"nameId\": 1, " +
            "\"itemType\": \"Bag\", " +
            "\"capacity\": 10," +
            "\"inventoryType\": \"UniqueEquip\", " +
            $"\"contents\": [{armorJson}, {potionJson1}, {potionJson2}, {armorJson2}]" +
            "}";

        //Assert.That(() => {
        //    var item = new Stackable(json);
        //});



        ItemManager itemManager = new ItemManager();
        Bag bag1 = itemManager.CreateBag(bagJson1);
        Bag bag2 = itemManager.CreateBag(bagJson2);

        Debug.LogWarning($"bag1 type = {bag1?.inventoryType}");
        Debug.LogWarning($"bag2 type = {bag2?.inventoryType}");
    }
}
