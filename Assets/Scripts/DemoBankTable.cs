using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum DemoBankType
{
	None,
	Coin,
	Diamond,
	Ticket,
}

public static class DemoBankTable
{
	public class DemoBankData
	{
		public readonly int id;
		public readonly DemoBankType itemType;
		public readonly int maxValue;
		public DemoBankData(int _id, DemoBankType _itemType, int _maxValue)
		{
			id = _id;
			itemType = _itemType;
			maxValue = _maxValue;
		}
	}

	private static DemoBankData[] DemoDatas =
	{
		new DemoBankData(111, DemoBankType.Coin, 200),
		new DemoBankData(222, DemoBankType.Diamond, 100),
		new DemoBankData(333, DemoBankType.Ticket, 50)
	};  


	public static async Task<DemoBankData> GetData(int id)
	{
		var data = DemoDatas.FirstOrDefault(data => data.id == id);
		if(data == null)
		{
			Debug.LogError($"No Data!! Find ID : {id}");
		}

		return data;
	}
}