using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class DemoBankTypeToggle : MonoBehaviour
{
	private Toggle toggle;
	[SerializeField] private int coinBankKey = 000;

	private void Awake()
	{
		toggle = GetComponent<Toggle>();
		toggle.onValueChanged.AddListener(async isOn =>
		{
			if(isOn) await ChangeData();
		} );

		async Task ChangeData()
		{
			// screen lock
			await PiggyBankManager.Instance.LoadData(coinBankKey);
			// screen unlock
		}
	}
}
