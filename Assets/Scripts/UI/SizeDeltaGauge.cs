using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class SizeDeltaGauge : MonoBehaviour
{
	[SerializeField] private Image bg;
	[SerializeField] private Image gauge;
	[SerializeField] private Text gaugeText;

	private Vector2 originDelta;
	private int maxCount;

	private void Awake()
	{
		if(gauge != null)
		{
			originDelta = gauge.rectTransform.sizeDelta;
		}
	}

	public void Initialize(int startValue, int bankFullCount)
	{
		maxCount = bankFullCount;
		SetGauge(startValue);
	}

	CancellationTokenSource cancellation = new CancellationTokenSource();
	public async void SetGauge(int value, bool animation = false)
	{
		var amount = (float)value / maxCount;
		if (gauge != null)
		{
			var target = originDelta;
			target.x *= amount;
			if(animation)
			{
				if(cancellation != null)cancellation.Cancel();
				cancellation = new CancellationTokenSource();
				await DemoFillAnim(gauge.rectTransform, target);
			}
			else
			{
				gauge.rectTransform.sizeDelta = target;
			}
		}
		if(gaugeText != null)
		{
			gaugeText.text = $"{(Mathf.Min(value, maxCount)).ToString()}/{maxCount}";
		}
	}

	private async Task DemoFillAnim(RectTransform target, Vector2 targetDelta, float time = 0.5f)
	{
		float t = 0;
		Vector2 startDelta = target.sizeDelta;
		while(t < time)
		{
			if(cancellation.Token.IsCancellationRequested) break;

			target.sizeDelta = Vector2.Lerp(startDelta, targetDelta, t / time);
			t += Time.deltaTime;
			await Task.Yield();
		}
		target.sizeDelta = targetDelta;
	}
}
