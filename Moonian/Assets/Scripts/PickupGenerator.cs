using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGenerator : MonoBehaviour
{
    public Transform parentTF;
    public List<GenObj> GenerateObjects;
    public List<BoxItems> ContainerItems;
    public int MaxContainerItemNum = 6;
    private List<BoxItems> SortedItems;

    public SaveLoadSystem sls;
    void Awake()
    {
        SortContainerItems();
    }

    public void SpawnPickups(bool newGameIndicator)
    {
        foreach (GenObj g in GenerateObjects)
        {
            int num = (int)Random.Range(g.minNum, g.maxNum);

            // adjust the num of generation
            if (!newGameIndicator)
            {
                int[] pked = InventoryManager.Instance.picked;
                if (g.Prefab.GetComponent<ItemController>().item.id != 256) // box/chest not affected
                {
                    int j;
                    for (j = 0; j < sls.iList.Count; j++)
                    {
                        if (sls.iList[j].id == g.Prefab.GetComponent<ItemController>().item.id)
                        {
                            break;
                        }
                    }
                    num -= pked[j];
                }
            }
            Debug.Log("Generating" + g.Prefab.GetComponent<ItemController>().item.name + ":" + num);
            if (num <= 0) continue; // no more generation of this item
            for (int i = 0; i < num; i++)
            {
                // generate random items with random positions and rotations
                float xPos = Random.Range(g.xMin, g.xMax) * Mathf.Sign(Random.value - 0.5f);
                float zPos = Random.Range(g.zMin, g.zMax) * Mathf.Sign(Random.value - 0.5f);
                float xRot = Random.Range(-10f, 10f);
                float yRot = Random.Range(-180f, 180f);
                float zRot = Random.Range(-10f, 10f);
                Vector3 pos = new Vector3(xPos, 0, zPos);
                Quaternion rot = Quaternion.Euler(xRot, yRot, zRot);
                pos.y = Terrain.activeTerrain.SampleHeight(pos);
                GameObject obj = Instantiate(g.Prefab);
                obj.transform.position = pos;
                obj.transform.rotation = rot;
                obj.transform.parent = parentTF;

                if (obj.name.Contains("Cardbox") || obj.name.Contains("Chest1"))
                {
                    List<Item> CItems = new List<Item>();
                    List<int> CAmounts = new List<int>();
                    foreach (BoxItems b in SortedItems)
                    {
                        if (ProbToBool(b.prob) && CItems.Count <= MaxContainerItemNum)
                        {
                            int itemAmt = (int)Random.Range(b.minNum, b.maxNum);
                            CItems.Add(Instantiate(b.item));
                            CAmounts.Add(itemAmt);
                        }
                    }
                    obj.GetComponent<BoxOfItems>().Contents = CItems;
                    obj.GetComponent<BoxOfItems>().Amounts = CAmounts;
                }
            }
        }
    }

    private bool ProbToBool(float pr)
    {
        float rand = Random.Range(0f, 1f);
        if (rand <= pr) return true;
        else return false;
    }

    private void SortContainerItems()
    {
        SortedItems = new List<BoxItems>(ContainerItems);
        SortedItems.Sort(SortFunc);
    }
    private int SortFunc(BoxItems a, BoxItems b)
    {
        if (a.prob < b.prob)
            return -1;
        else if (a.prob > b.prob)
            return 1;
        return 0;
    }

}

[System.Serializable]
public class GenObj
{
    public GameObject Prefab;

    [Range(100, 4900)]
    public int xMin, xMax;
    [Range(100, 4900)]
    public int zMin, zMax;  // range that objects can be generated (Absolute values!)
    [Range(0, 999)]
    public uint minNum, maxNum;
}

[System.Serializable]
public class BoxItems
{
    public Item item;
    [Range(1, 10)]
    public int minNum, maxNum;
    [Range(0f, .5f)]
    public float prob; // probability that an item would occur
}