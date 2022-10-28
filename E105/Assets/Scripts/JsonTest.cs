// using System.Collections; 
// using System.Collections.Generic;
// using System.IO;
// using UnityEngine; 

// [System.Serializable]
// public class ItemObject
// {
//     public int ItemCode;
//     public string Name;
//     public string Category;
//     public int BuyCost;
//     public int SellCost;
// }

// [System.Serializable]
// public class ItemData
// {
//     public List<ItemObject> Item;
// }

// public static class JsonHelper
// {
//     public static T[] FromJson<T>(string json)
//     {
//         Wrapper<T> wrapper = UnityEngine.JsonUtility.FromJson<Wrapper<T>>(json);
//         return wrapper.Items;
//     }

//     public static string ToJson<T>(T[] array)
//     {
//         Wrapper<T> wrapper = new Wrapper<T>();
//         wrapper.Items = array;
//         return  UnityEngine.JsonUtility.ToJson(wrapper);
//     }

//     [System.Serializable]
//     private class Wrapper<T>
//     {
//         public T[] Items;
//     }
// }

// public class JsonTest : MonoBehaviour 
// { 
//     // Start is called before the first frame update 
//     void Start() 
//     {

//     } 
 
//     // Update is called once per frame 
//     void Update() 
//     { 

//     }

//     public ItemObject getItemInfo(int ItemCode)
//     {
//         string jsonString = File.ReadAllText(Application.dataPath + "/Scripts/ItemTable.json");
//         var itemData = JsonHelper.FromJson<ItemObject>(jsonString);
//         foreach(var item in itemData) {
//             if (item.ItemCode == ItemCode) {
//                 Debug.Log(item.Name);
//                 Debug.Log(item.Category);
//                 return item;
//             } 
//         }
//         return null;
//     }
// }
