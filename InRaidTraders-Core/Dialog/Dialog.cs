using System.Collections.Generic;
using System.Linq;
using Comfort.Common;
using EFT;
using EFT.Counters;
using EFT.InputSystem;
using EFT.InventoryLogic;
using EFT.Trading;
using EFT.UI;
using EFT.UI.Screens;
using InRaidTraders.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace InRaidTraders.Dialog;

public class Dialog : GClass2379
{
	public override ETraderDialogType Type => ETraderDialogType.Main;
	public override string BaseDescriptionKey { get; }
	private readonly Profile.TraderInfo _traderInfo;
	private readonly GInterface237 _stateStorage;

	public Dialog(Profile.TraderInfo traderData, AbstractQuestControllerClass quests, InventoryController inventoryController, GInterface237 stateStorage, DialogOptionDataStruct source) : base(traderData, quests, inventoryController, source)
	{
		_traderInfo = traderData;
		_stateStorage = stateStorage;
		if (!source.DescriptionKey.IsNullOrEmpty())
		{
			BaseDescriptionKey = source.DescriptionKey;
		}
		else if (source.DialogAction.Equals(GClass3353.EDialogState.CommonGreetigs))
		{
			BaseDescriptionKey = Utils.Utils.TraderIdToName(_traderInfo.Id).ToLower() + "_generic_greetings_" +
			                     Random.Range(1, 4).ToString().PadLeft(2, '0');
		}
		DialogOptions();
		OnRedraw += DialogOptions;
		compositeDisposableClass.AddDisposable(RedrawDialogOptions);
		_traderInfo.OnTraderServicesUpdated += method_5;
		compositeDisposableClass.AddDisposable(UpdateServices);
	}

	private void DialogOptions()
	{
		// Main dialog list
		List<GClass2341> dialogOptionList = new List<GClass2341>();
		// News Dialog
		GStruct271 newsReadOnlyList = new(ETraderDialogType.Main, null, [
		new GStruct268("Trading/Dialog/"+ Utils.Utils.TraderIdToName(_traderInfo.Id) +"/News", new DialogOptionDataStruct(ETraderDialogType.Main, ETraderNewsDialogState.News1, "Trading/Dialog/"+ Utils.Utils.TraderIdToName(_traderInfo.Id) +"/News1"), new GStruct268.GStruct269(CounterTag.BtrNews, 1, GStruct268.ESaveStateType.Global), new GStruct268.GStruct269(CounterTag.BtrNews, 0), GStruct268.EDialogLiteIconType.QuestionMark),
		new GStruct268("Trading/Dialog/"+ Utils.Utils.TraderIdToName(_traderInfo.Id) +"/News/Next", new DialogOptionDataStruct(ETraderDialogType.Main, ETraderNewsDialogState.News2, "Trading/Dialog/"+ Utils.Utils.TraderIdToName(_traderInfo.Id) +"/News2"), new GStruct268.GStruct269(CounterTag.BtrNews, 2, GStruct268.ESaveStateType.Global), new GStruct268.GStruct269(CounterTag.BtrNews, 1), GStruct268.EDialogLiteIconType.QuestionMark),
		new GStruct268("Trading/Dialog/"+ Utils.Utils.TraderIdToName(_traderInfo.Id) +"/News/Next", new DialogOptionDataStruct(ETraderDialogType.Main, ETraderNewsDialogState.News3, "Trading/Dialog/"+ Utils.Utils.TraderIdToName(_traderInfo.Id) +"/News3"), new GStruct268.GStruct269(CounterTag.BtrNews, 3, GStruct268.ESaveStateType.Global), new GStruct268.GStruct269(CounterTag.BtrNews, 2), GStruct268.EDialogLiteIconType.QuestionMark),
		new GStruct268("Trading/Dialog/"+ Utils.Utils.TraderIdToName(_traderInfo.Id) +"/News/Next", new DialogOptionDataStruct(ETraderDialogType.Main, ETraderNewsDialogState.News4, "Trading/Dialog/"+ Utils.Utils.TraderIdToName(_traderInfo.Id) +"/News4"), new GStruct268.GStruct269(CounterTag.BtrNews, 4, GStruct268.ESaveStateType.Global), new GStruct268.GStruct269(CounterTag.BtrNews, 3), GStruct268.EDialogLiteIconType.QuestionMark),
		new GStruct268("Trading/Dialog/"+ Utils.Utils.TraderIdToName(_traderInfo.Id) +"/News/Next", new DialogOptionDataStruct(ETraderDialogType.Main, ETraderNewsDialogState.News5, "Trading/Dialog/"+ Utils.Utils.TraderIdToName(_traderInfo.Id) +"/News5"), new GStruct268.GStruct269(CounterTag.BtrNews, 5, GStruct268.ESaveStateType.Global), new GStruct268.GStruct269(CounterTag.BtrNews, 4), GStruct268.EDialogLiteIconType.QuestionMark),
		new GStruct268("Trading/Dialog/"+ Utils.Utils.TraderIdToName(_traderInfo.Id) +"/News/Next", new DialogOptionDataStruct(ETraderDialogType.Main, ETraderNewsDialogState.News5, "Trading/Dialog/"+ Utils.Utils.TraderIdToName(_traderInfo.Id) +"/NoNews"), new GStruct268.GStruct269(CounterTag.BtrNews, 5, GStruct268.ESaveStateType.Global), new GStruct268.GStruct269(CounterTag.BtrNews, 5), GStruct268.EDialogLiteIconType.QuestionMark)
		]);
		foreach (GClass2341 newsDialog in new GClass2383(newsReadOnlyList, _stateStorage, Source).Lines)
		{
			dialogOptionList.Add(newsDialog);
		}
		// Quest Dialog
		if (inventoryController_0.Profile.Side != EPlayerSide.Savage)
		{
			GClass2382 questOptions = new GClass2382(traderInfo_0, abstractQuestControllerClass, inventoryController_0,
				_stateStorage, Source);
			compositeDisposableClass.AddDisposable(questOptions);
			foreach (GClass2341 questDialog in questOptions.Lines)
			{
				if (!(questDialog is GClass2358))
				{
					dialogOptionList.Add(questDialog);
				}
			}

			// Trading Dialog Option
			GClass2357 tradingDialogOption = new GClass2357(
				new DialogOptionDataStruct(ETraderDialogType.None,
					GClass3353.EDialogState.None,
					"Trading/Dialog/" + Utils.Utils.TraderIdToName(_traderInfo.Id) +
					"/Trading/Description".Localized()),
				"Trading/Dialog/" + Utils.Utils.TraderIdToName(_traderInfo.Id) + "/Trading",
				GStruct268.EDialogLiteIconType.ShoppingCart);
			tradingDialogOption.OnChangeDialog += OpenTradingUI;
			dialogOptionList.Add(tradingDialogOption);

			// Services Dialog Option
			foreach (Config configOption in Globals.ConfigList)
			{
				if (_traderInfo.Id == Globals.RAGMAN_ID || (configOption.traderID == _traderInfo.Id && configOption.hasServices)) 
				{
					GClass2357 servicesDialogOption = new GClass2357(
						new DialogOptionDataStruct(ETraderDialogType.Services,
							GClass3353.EDialogState.AvailableServices,
							"Trading/Dialog/" + Utils.Utils.TraderIdToName(_traderInfo.Id) +
							"/AvailableServices/Description".Localized()),
							"Trading/Dialog/" + Utils.Utils.TraderIdToName(_traderInfo.Id) + "/AvailableServices",
							GStruct268.EDialogLiteIconType.Suitcase);
					servicesDialogOption.OnChangeDialog += OpenServicesUI;
					dialogOptionList.Add(servicesDialogOption);
				}
			}
		}

		// Quit Dialog Option
		GClass2357 quitDialogOption = new GClass2357(
			new DialogOptionDataStruct(ETraderDialogType.Quit, 
				GClass3353.EDialogState.CommonFarewell, 
				(Utils.Utils.TraderIdToName(_traderInfo.Id).ToLower() + "_generic_farewell_01")), 
			"Trading/Dialog/"+ Utils.Utils.TraderIdToName(_traderInfo.Id) +"/Quit", 
			GStruct268.EDialogLiteIconType.QuitIcon, null, null, ECommand.Escape);
		dialogOptionList.Add(quitDialogOption);
		
		method_0(dialogOptionList);
	}

	private void RedrawDialogOptions()
	{
		OnRedraw -= DialogOptions;
	}

	private void OpenServicesUI(DialogOptionDataStruct test, GStruct267 test2)
	{		
		Plugin.LogSource.LogWarning("Called OpenServicesUI");
	}

	private void OpenTradingUI(DialogOptionDataStruct dialogOptionStruct, GStruct267 test2)
	{
		if (!Input.GetKeyDown(KeyCode.Escape))
		{
			TraderClass[] tradeableArray = Singleton<ClientApplication<ISession>>.Instance.Session.Traders.Where(MainMenuControllerClass.Class1394.class1394_0.method_4).ToArray();
			TraderClass[] currentTrader = [tradeableArray.First()];
			foreach (var trader in tradeableArray)
			{
				if (trader.Id == _traderInfo.Id)
				{
					currentTrader = [trader];
				}
			}
			var gClass3599 = new TraderScreensGroup.GClass3599(currentTrader.First(),
				currentTrader,
				Singleton<GameWorld>.Instance.MainPlayer.Profile,
				Globals.InventoryController,
				Singleton<GameWorld>.Instance.MainPlayer.HealthController,
				Singleton<GameWorld>.Instance.MainPlayer.AbstractQuestControllerClass,
				Singleton<GameWorld>.Instance.MainPlayer.AbstractAchievementControllerClass,
				Singleton<ClientApplication<ISession>>.Instance.Session);
			gClass3599.ShowScreen(EScreenState.Queued);
		}
	}

	private void UpdateServices()
	{
		_traderInfo.OnTraderServicesUpdated -= method_5;
	}
}