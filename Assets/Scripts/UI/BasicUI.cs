using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUI : MonoBehaviour
{
    void OnGUI()
	{
		int posX = 10;
		int posY = 10;
		int width = 100;
		int height = 30;
		int buffer = 10;

		List<string> itemsList = Managers.Inventory.GetItemsList();
		if (itemsList.Count == 0)
		{
			GUI.Box(new Rect(posX, posY, width, height), "No Items");
		}
		foreach(string item in itemsList)
		{
			int count = Managers.Inventory.GetItemsCount(item);
			Texture2D image = Resources.Load<Texture2D>("icons/" + item);
			GUI.Box(new Rect(posX, posY, width, height), new GUIContent("(" + count + ")", image));
			posX += width + buffer;
		}
		string equipped = Managers.Inventory.equippedItem;
		if (equipped != null)
		{
			posX = Screen.width - (width + buffer);
			Texture2D image = Resources.Load("icons/" + equipped) as Texture2D;
			GUI.Box(new Rect(posX, posY, width, height), new GUIContent("Equipped", image));
		}
		posX = 10;
		posY += height + buffer;

		foreach (string item in itemsList)
		{
			if (GUI.Button(new Rect(posX, posY, width, height), "Equip " + item))
			{
				Managers.Inventory.EquipItem(item);
			}
			posX += width + buffer;
		}
	}
}
