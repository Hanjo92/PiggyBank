using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class DemoDefines
{
	public static Color CoinColor = Color.yellow;
	public static Color DiaColor = Color.cyan;
	public static Color TicketColor = Color.gray;
	public static Color GetBankColor(DemoBankType bankType) => bankType switch
	{
		DemoBankType.Coin => CoinColor,
		DemoBankType.Diamond => DiaColor,
		DemoBankType.Ticket => TicketColor,
		_ => Color.white,
	};

	public const string DemoBankTitleFormat = "{0} Bank";
	public const string DemoBankDescriptionFormat = "Piggy Bank Demo\nCurrent Type\n{0}";
}
