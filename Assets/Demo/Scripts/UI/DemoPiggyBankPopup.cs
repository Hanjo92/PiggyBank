using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Threading.Tasks;

public class DemoPiggyBankPopup : MonoBehaviour
{
	[SerializeField] private RectTransform root;
	[SerializeField] private Text title;
	[SerializeField] private Text description;

	[SerializeField] private Image typeImage;

	[SerializeField] private SizeDeltaGauge gauge;
	[SerializeField] private RectTransform buyButtonObject;
	private Button buyButton;

	private void Awake()
	{
		PiggyBankManager.Instance.OnChangeData += (PiggyBankData d) =>
		{
			if(d == null)
			{
				return;
			}
			title.text = string.Format(DemoDefines.DemoBankTitleFormat, d.ItemType.ToString());
			description.text = string.Format(DemoDefines.DemoBankDescriptionFormat, d.ItemType.ToString());
			typeImage.color = DemoDefines.GetBankColor(d.ItemType);
			gauge.Initialize(d.CoinBankValue, d.MaxValue);
			BuyButtonToggle(d.IsFull);
		};
		PiggyBankManager.Instance.OnChangeDataValue += (v, a, f) =>
		{
			gauge.SetGauge(v, a);
			BuyButtonToggle(f);
		};

		if(buyButtonObject != null)
		{
			buyButton = buyButtonObject.GetComponent<Button>();
			if(buyButton == null )
			{
				buyButton = buyButtonObject.gameObject.AddComponent<Button>();
			}

			buyButton.onClick.AddListener(async () => { await OnBuyButtonClickAsync(); });
		}

		void BuyButtonToggle(bool value)
		{
			buyButtonObject.gameObject.SetActive(value);
			LayoutRebuilder.ForceRebuildLayoutImmediate(root);
		}

		async Task OnBuyButtonClickAsync()
		{
			// Click effect
			await OnBuy();
		}
	}
	private async Task OnBuy()
	{
		// Screen lock
		// ScreenLock();
		await PiggyBankManager.Instance.Buy();
		// Give Effect
		// Screen Unlock
	}
}