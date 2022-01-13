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

        string bagJson = "{" +
            "\"uid\": 1," +
            "\"name\": \"some named bag\", " +
            "\"nameId\": 1, " +
            "\"itemType\": \"Bag\", " +
            "\"capacity\": 10," +
            $"\"contents\": [{armorJson}, {potionJson1}, {potionJson2}]" +
            "}";

        //Assert.That(() => {
        //    var item = new Stackable(json);
        //});



        ItemManager itemManager = new ItemManager();
        Item bag = itemManager.CreateBag(bagJson);


        //Item armor = itemManager.CreateItem(armorJson);
        //Debug.Log(armor.uid);
        //Debug.Log(armor.name);
        //Debug.Log(armor.itemType);
        //Debug.Log((int)armor.itemType);
        //armor.Perform();

        //Item bag = itemManager.CreateItem(bagJson);
        //Debug.Log(bag.uid);
        //Debug.Log(bag.name);
        //Debug.Log(bag.itemType);
        //Debug.Log((int)bag.itemType);
        //bag.Perform();

        //Item potion1 = itemManager.CreateItem(potionJson1);
        //Debug.Log(potion1.uid);
        //Debug.Log(potion1.name);
        //Debug.Log(potion1.itemType);
        //Debug.Log((int)potion1.itemType);
        //potion1.Perform();

        //Item potion2 = itemManager.CreateItem(potionJson2);
        //Debug.Log(potion2.uid);
        //Debug.Log(potion2.name);
        //Debug.Log(potion2.itemType);
        //Debug.Log((int)potion2.itemType);
        //potion2.Perform();

    }
}
