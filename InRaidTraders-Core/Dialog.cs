using System.Collections.Generic;
using System.Linq;
using Comfort.Common;
using EFT;
using EFT.Counters;
using EFT.InputSystem;
using EFT.InventoryLogic;
using EFT.Trading;

namespace InRaidTraders;

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
			IReadOnlyList<string> greetingsList = new List<string>
			{
				Utils.TraderIdToName(_traderInfo.Id).ToLower() + "_generic_greetings_01",
				Utils.TraderIdToName(_traderInfo.Id).ToLower() + "_generic_greetings_02",
				Utils.TraderIdToName(_traderInfo.Id).ToLower() + "_generic_greetings_03",
				Utils.TraderIdToName(_traderInfo.Id).ToLower() + "_generic_greetings_04"
			};
			
			BaseDescriptionKey = greetingsList.PickRandom();
		}
		DialogOptions();
		OnRedraw += DialogOptions;
		compositeDisposableClass.AddDisposable(RedrawDialogOptions);
		_traderInfo.OnTraderServicesUpdated += method_5;
		compositeDisposableClass.AddDisposable(updateServices);
	}

	public void DialogOptions()
	{
		// Main dialog list
		List<GClass2341> dialogOptionList = new List<GClass2341>();
		// News Dialog
		GStruct271 newsReadOnlyList = new(ETraderDialogType.Main, null, [
		new GStruct268("Trading/Dialog/"+ Utils.TraderIdToName(_traderInfo.Id) +"/News", new DialogOptionDataStruct(ETraderDialogType.Main, ETraderNewsDialogState.News1, "Trading/Dialog/"+ Utils.TraderIdToName(_traderInfo.Id) +"/News1"), new GStruct268.GStruct269(CounterTag.BtrNews, 1, GStruct268.ESaveStateType.Global), new GStruct268.GStruct269(CounterTag.BtrNews, 0), GStruct268.EDialogLiteIconType.QuestionMark),
		new GStruct268("Trading/Dialog/"+ Utils.TraderIdToName(_traderInfo.Id) +"/News/Next", new DialogOptionDataStruct(ETraderDialogType.Main, ETraderNewsDialogState.News2, "Trading/Dialog/"+ Utils.TraderIdToName(_traderInfo.Id) +"/News2"), new GStruct268.GStruct269(CounterTag.BtrNews, 2, GStruct268.ESaveStateType.Global), new GStruct268.GStruct269(CounterTag.BtrNews, 1), GStruct268.EDialogLiteIconType.QuestionMark),
		new GStruct268("Trading/Dialog/"+ Utils.TraderIdToName(_traderInfo.Id) +"/News/Next", new DialogOptionDataStruct(ETraderDialogType.Main, ETraderNewsDialogState.News3, "Trading/Dialog/"+ Utils.TraderIdToName(_traderInfo.Id) +"/News3"), new GStruct268.GStruct269(CounterTag.BtrNews, 3, GStruct268.ESaveStateType.Global), new GStruct268.GStruct269(CounterTag.BtrNews, 2), GStruct268.EDialogLiteIconType.QuestionMark),
		new GStruct268("Trading/Dialog/"+ Utils.TraderIdToName(_traderInfo.Id) +"/News/Next", new DialogOptionDataStruct(ETraderDialogType.Main, ETraderNewsDialogState.News4, "Trading/Dialog/"+ Utils.TraderIdToName(_traderInfo.Id) +"/News4"), new GStruct268.GStruct269(CounterTag.BtrNews, 4, GStruct268.ESaveStateType.Global), new GStruct268.GStruct269(CounterTag.BtrNews, 3), GStruct268.EDialogLiteIconType.QuestionMark),
		new GStruct268("Trading/Dialog/"+ Utils.TraderIdToName(_traderInfo.Id) +"/News/Next", new DialogOptionDataStruct(ETraderDialogType.Main, ETraderNewsDialogState.News5, "Trading/Dialog/"+ Utils.TraderIdToName(_traderInfo.Id) +"/News5"), new GStruct268.GStruct269(CounterTag.BtrNews, 5, GStruct268.ESaveStateType.Global), new GStruct268.GStruct269(CounterTag.BtrNews, 4), GStruct268.EDialogLiteIconType.QuestionMark),
		new GStruct268("Trading/Dialog/"+ Utils.TraderIdToName(_traderInfo.Id) +"/News/Next", new DialogOptionDataStruct(ETraderDialogType.Main, ETraderNewsDialogState.News5, "Trading/Dialog/"+ Utils.TraderIdToName(_traderInfo.Id) +"/NoNews"), new GStruct268.GStruct269(CounterTag.BtrNews, 5, GStruct268.ESaveStateType.Global), new GStruct268.GStruct269(CounterTag.BtrNews, 5), GStruct268.EDialogLiteIconType.QuestionMark)
		]);
		foreach (GClass2341 newsDialog in new GClass2383(newsReadOnlyList, _stateStorage, Source).Lines)
		{
			dialogOptionList.Add(newsDialog);
		}
		// Quest Dialog
		if (inventoryController_0.Profile.Side != EPlayerSide.Savage)
		{
			GClass2382 questOptions = new GClass2382(traderInfo_0, abstractQuestControllerClass, inventoryController_0, _stateStorage, Source);
			compositeDisposableClass.AddDisposable(questOptions);
			foreach (GClass2341 questDialog in questOptions.Lines)
			{
				if (!(questDialog is GClass2358))
				{
					dialogOptionList.Add(questDialog);
				}
			}
		}
		// Services Dialog Option
		if (_traderInfo.Id == "5ac3b934156ae10c4430e83c")
		{
			GClass2361 servicesDialogOption = new GClass2361(
				new DialogOptionDataStruct(ETraderDialogType.Services, GClass3353.EDialogState.AvailableServices,
					"Trading/" + Utils.TraderIdToName(_traderInfo.Id) +
					"/Dialog/AvailableServices/Description".Localized()),
				"Trading/Dialog/" + Utils.TraderIdToName(_traderInfo.Id) + "/AvaliableServices",
				GStruct268.EDialogLiteIconType.Suitcase);
			Dictionary<ETraderServiceType, BackendConfigSettingsClass.ServiceData> servicesData =
				Singleton<BackendConfigSettingsClass>.Instance.ServicesData;
			servicesDialogOption.IsActiveAndInteractive = (servicesData != null && servicesData.Any(GetTraderID));
			dialogOptionList.Add(servicesDialogOption);
		}

		// Quit Dialog Option
		GClass2357 quitDialogOption = new GClass2357(new DialogOptionDataStruct(ETraderDialogType.Quit, GClass3353.EDialogState.CommonFarewell, (Utils.TraderIdToName(_traderInfo.Id).ToLower() + "_generic_farewell_01")), "Trading/Dialog/"+ Utils.TraderIdToName(_traderInfo.Id) +"/Quit", GStruct268.EDialogLiteIconType.QuitIcon, null, null, ECommand.Escape);
		dialogOptionList.Add(quitDialogOption);
		
		method_0(dialogOptionList);
	}
	public void RedrawDialogOptions()
	{
		OnRedraw -= DialogOptions;
	}
	public void updateServices()
	{
		_traderInfo.OnTraderServicesUpdated -= method_5;
	}
	public bool GetTraderID(KeyValuePair<ETraderServiceType, BackendConfigSettingsClass.ServiceData> data)
	{
		return data.Value.TraderId == _traderInfo.Id;
	}
}