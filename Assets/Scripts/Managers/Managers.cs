using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InventoryManager))]
public class Managers : MonoBehaviour
{
    public static PlayerManager Player { get; private set; }
    public static InventoryManager Inventory { get; private set; }

    private List<IGameManager> startupSequence;
	private void Awake()
	{
		Player = GetComponent<PlayerManager>();
		Inventory = GetComponent<InventoryManager>();

		startupSequence = new List<IGameManager>();
		startupSequence.Add(Player);
		startupSequence.Add(Inventory);

		StartCoroutine(StartupManagers());
	}

	private IEnumerator StartupManagers()
	{
		foreach (IGameManager manager in startupSequence)
		{
			manager.StartUp();
		}
		yield return null;

		int numberOfModules = startupSequence.Count;
		int numberOfReadyModules = 0;

		while (numberOfReadyModules < numberOfModules)
		{
			int lastReady = numberOfReadyModules;
			numberOfReadyModules = 0;

			foreach (IGameManager manager in startupSequence)
			{
				if (manager.status == ManagerStatus.Started)
				{
					numberOfReadyModules++;
				}
			}

			if (numberOfReadyModules > lastReady)
			{
				Debug.Log("Progress: " + numberOfReadyModules + "/" + numberOfModules);
			}
			yield return null;
		}
		Debug.Log("All managers started up.");
	}
}
