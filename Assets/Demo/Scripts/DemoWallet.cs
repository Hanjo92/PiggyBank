using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoWallet
{
	private static DemoWallet mInstance = null;
	public static DemoWallet Instance => mInstance ??= new DemoWallet();

	public Action<DemoBankType, int> onChangeMoney;

	private int coinCount = 0;
	private int diaCount = 0;
	private int ticketCount = 0;

	public void AddMoney(DemoBankType bankType, int count)
	{
		var newCount = bankType switch
		{
			DemoBankType.Coin => coinCount += count,
			DemoBankType.Diamond => diaCount += count,
			DemoBankType.Ticket => ticketCount += count,
			_ => 0,
		};
		onChangeMoney?.Invoke(bankType, newCount);
	}

	public int GetCoinCount() => Instance.coinCount;
	public int GetDiamondCount() => Instance.diaCount;
	public int GetTicketCount() => Instance.ticketCount;
}