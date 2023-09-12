using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PiggyBankManager
{
	private static PiggyBankManager mInstance = null;
	public static PiggyBankManager Instance => mInstance ??= new PiggyBankManager();

	private PiggyBankData mData = null;
	private PiggyBankData CurrentPiggyBankData
	{
		get => mData;
		set
		{
			mData = value;
			OnChangeData?.Invoke(mData);
		}
	}

	public Action<PiggyBankData> OnChangeData;
	/// <summary>
	/// First : New value, Second : is Add, Third : is Full
	/// </summary>
	public Action<int, bool, bool> OnChangeDataValue;

	public async Task<bool> LoadData(int coinBankKey)
	{
		var loadData = await PiggyBankData.LoadData(coinBankKey);
        if (loadData == null)
        {
			Debug.LogError("Load fail no Data");
			return false;
        }
		CurrentPiggyBankData = loadData;
		OnChangeDataValue?.Invoke(CurrentPiggyBankData.CoinBankValue, false, CurrentPiggyBankData.IsFull);

		return true;
    }
	public async Task<bool> SetFullCurrentDataValue()
	{
		if(CurrentPiggyBankData == null)
		{
			Debug.LogWarning("CurrentPiggyBankData is null");
			return false;
		}

		return await SetCurrentDataValue(CurrentPiggyBankData.MaxValue, true);
	}
	public async Task<bool> AddCurrentDataValue(int value)
	{
		if(CurrentPiggyBankData == null)
		{
			Debug.LogWarning("CurrentPiggyBankData is null");
			return false;
		}
		if(CurrentPiggyBankData.IsFull)
		{
			Debug.LogWarning("CurrentPiggyBankData already full");
			return false;
		}

		var newValue = CurrentPiggyBankData.CoinBankValue + value;

		return await SetCurrentDataValue(newValue, true);
	}
	public async Task<bool> SetCurrentDataValue(int newValue, bool isAdd = false)
	{
		if( CurrentPiggyBankData == null )
		{
			Debug.LogWarning("CurrentPiggyBankData is null");
			return false;
		}

		var isComplete = await CurrentPiggyBankData.SetValue(newValue);
		if( isComplete == false )
		{
			Debug.LogError("Data save fail");
			return false;
		}
		OnChangeDataValue?.Invoke(CurrentPiggyBankData.CoinBankValue, isAdd, CurrentPiggyBankData.IsFull);
		return true;
	}

	public async Task<bool> Buy()
	{
		// Check
		if(CurrentPiggyBankData == null)
		{
			Debug.LogWarning("CurrentPiggyBankData is null");
			return false;
		}
		if(CurrentPiggyBankData.IsFull == false)
		{
			Debug.LogWarning("CurrentPiggyBankData is not full");
			return false;
		}
		// IAP check

		DemoWallet.Instance.AddMoney(CurrentPiggyBankData.ItemType, CurrentPiggyBankData.MaxValue);
		Debug.Log("Complete");
		await SetCurrentDataValue(0);
		return true;
	}
}
