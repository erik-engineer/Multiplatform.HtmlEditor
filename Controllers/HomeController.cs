using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MultiPlatform.HtmlEditor.Controllers
{
	public class HomeController : Controller
	{

		public IActionResult Index()
		{
			if (HybridSupport.IsElectronActive)
			{
				//save file
				Electron.IpcMain.On("saveFile", async (args) =>
				{
					string content = args.ToString();

					var mainWindow = Electron.WindowManager.BrowserWindows.First();
					var options = new SaveDialogOptions
					{
						Title = "Save HTML file",
						Filters = new FileFilter[]
								{
								new FileFilter { Name = "HTML", Extensions = new string[] {"html","htm" } }
								}
					};

					string result = await Electron.Dialog.ShowSaveDialogAsync(mainWindow, options);
					if (result.Count() != 0)
					{
						FileOperation.saveFile(content, result);
					}
				});


			}

			return View();
		}

	}
}
