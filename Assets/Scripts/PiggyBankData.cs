using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PiggyBankData
{
	public int CoinBankValue { get; private set; }
	/// <summary>
	///  Demo Table Data
	/// </summary>
	/// 
	public DemoBankTable.DemoBankData Data { get; private set; }
	public int CoinBankKey => Data?.id ?? -1;
	public DemoBankType ItemType => Data?.itemType ?? DemoBankType.None;
	public int MaxValue => Data?.maxValue ?? 0;
	public bool IsFull => CoinBankValue >= MaxValue;

	/// <summary>
	/// Use DemoTable
	/// </summary>
	/// <param name="data">Coin bank const data class</param>
	/// <param name="piggyBankValue">Initialize value or saved value</param>
	public PiggyBankData(DemoBankTable.DemoBankData data, int piggyBankValue)
	{
		this.Data = data;
		CoinBankValue = piggyBankValue;
	}

	public async Task<bool> SetValue(int newValue)
	{
		CoinBankValue = Mathf.Clamp(newValue, 0, MaxValue);
		return await Save();
	}

	public async Task<bool> ClearValue()
	{
		return await SetValue(0);
	}

	public async Task<bool> Save()
	{
		#region Save func for Demo 
		/// Use PlayerPrefs
		PlayerPrefs.SetInt(CoinBankKey.ToString(), CoinBankValue);
		#endregion
		return true;
	}

	/// <summary>
	/// Data load for Demo
	/// </summary>
	/// <param name="id">Coin bank data id</param>
	/// <returns></returns>
	public static async Task<PiggyBankData> LoadData(int id)
	{
		var data = await DemoBankTable.GetData(id);
		if(data == null)
		{
			Debug.LogError($"No Data!! Find ID : {id}");
			return null;
		}
		// Get saved value
		var value = PlayerPrefs.GetInt(id.ToString(), 0);

		return new PiggyBankData(data, value);
	}
}
