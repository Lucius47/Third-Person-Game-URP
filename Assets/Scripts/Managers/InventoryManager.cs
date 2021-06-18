using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
	public ManagerStatus status { get; private set; }
	public string equippedItem { get; private set; }
	private Dictionary<string, int> items;
	public void StartUp()
	{
		Debug.Log("InventoryManager initializing...");
		items = new Dictionary<string, int>();
		status = ManagerStatus.Started;
	}
	public void DisplayItems()
	{
		string itemsDisplay = "Items: ";
		foreach (KeyValuePair<string, int> item in items)
		{
			itemsDisplay += item.Key + "(" + item.Value + ") ";
		}
		Debug.Log(itemsDisplay);
	}
	public void AddItem(string name)
	{
		if (items.ContainsKey(name))
		{
			items[name] += 1;
		}
		else
		{
			items[name] = 1;
		}
		DisplayItems();
	}
	public List<string> GetItemsList()
	{
		List<string> list = new List<string>(items.Keys);
		return list;
	}
	public int GetItemsCount (string name)
	{
		if (items.ContainsKey(name))
		{
			return items[name];
		}
		return 0;
	}
	public bool EquipItem(string name)
	{
		if(items.ContainsKey(name) && equippedItem != name)
		{
			equippedItem = name;
			Debug.Log("Equipped " + name);
			return true;
		}
		equippedItem = null;
		Debug.Log("Unequipped");
		return false;
	}
}