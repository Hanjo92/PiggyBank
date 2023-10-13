using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUIGroup : MonoBehaviour
{
    [SerializeField] private Button add10;
    [SerializeField] private Button full;
    [SerializeField] private Button clear;

	[SerializeField] private Text coinText;
	[SerializeField] private Text diaText;
	[SerializeField] private Text ticketText;

	private void Awake()
	{
		add10.onClick.AddListener(async () =>
		{
			await PiggyBankManager.Instance.AddCurrentDataValue(10);
		});

		full.onClick.AddListener(async () =>
		{
			await PiggyBankManager.Instance.SetFullCurrentDataValue();
		});

		clear.onClick.AddListener(async () =>
		{
			await PiggyBankManager.Instance.SetCurrentDataValue(0);
		});

		DemoWallet.Instance.onChangeMoney += OnChangeMoney;
		coinText.text = DemoWallet.Instance.GetCoinCount().ToString();
		diaText.text = DemoWallet.Instance.GetDiamondCount().ToString();
		ticketText.text = DemoWallet.Instance.GetTicketCount().ToString();
	}

	private void OnDestroy()
	{
		DemoWallet.Instance.onChangeMoney -= OnChangeMoney;
	}

	private void OnChangeMoney(DemoBankType bankType, int count)
	{
		switch(bankType)
		{
			case DemoBankType.Coin:
			coinText.text = bankType.ToString();
			break;
			case DemoBankType.Diamond:
			diaText.text = bankType.ToString();
			break;
			case DemoBankType.Ticket:
			ticketText.text = bankType.ToString();
			break;
		}
	}
}
