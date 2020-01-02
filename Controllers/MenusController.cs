using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


using ElectronNET.API.Entities;
using ElectronNET.API;


namespace MultiPlatform.HtmlEditor.Controllers
{
	public class MenusController : Controller
	{

		public IActionResult Index()
		{
			if (HybridSupport.IsElectronActive)
			{
				var menu = new MenuItem[] {
				new MenuItem { Label = "File", Submenu = new MenuItem[] {
					new MenuItem { Label = "Open HTML",
								   Accelerator = "CmdOrCtrl+O",
								   Click = async ()  =>
									{
									// Open file HTML
									var mainWindow = Electron.WindowManager.BrowserWindows.First();
									var options = new OpenDialogOptions
											{
												Title = "Open HTML file",
												Filters = new FileFilter[]
												{
												new FileFilter { Name = "HTML", Extensions = new string[] {"html","htm" } }
												}
											};

									var result = await Electron.Dialog.ShowOpenDialogAsync(mainWindow, options);

									if (result.Count() !=0)
										{
										string OpenfilePath = result.First();
										string strContent = FileOperation.openRead(OpenfilePath);
										
										//Call Render JS
										var mainWindow1 = Electron.WindowManager.BrowserWindows.First();
										Electron.IpcMain.Send(mainWindow1,"setContent",strContent);

										mainWindow.SetTitle(OpenfilePath);
										}


									}
								 },
					new MenuItem { Label = "Save HTML",
									Accelerator = "CmdOrCtrl+S",
									Click = async () =>
									{
									var mainWindow = Electron.WindowManager.BrowserWindows.First();

									Electron.IpcMain.Send(mainWindow,"saveContent");
									}
								 },
					new MenuItem { Type = MenuType.separator },
					new MenuItem {  Label = "Exit",
									Accelerator = "CmdOrCtrl+X",
									Click = () =>
									{
									// Exit app
									Electron.App.Exit();
									}
								},
					}
				},
				new MenuItem { Label = "Help", Submenu = new MenuItem[] {
					new MenuItem
					{
						Label = "About",
						Accelerator = "CmdOrCtrl+R",
						Click = async () =>
						{
                            // open native  message  windows
							var options = new MessageBoxOptions("This is a demo application for Electron.NEt and .NET CORE 3.");
							options.Type = MessageBoxType.info;
							options.Title = "About HTMLEditor";

							await Electron.Dialog.ShowMessageBoxAsync(options);
						}
					}
				}
				}
			};

				Electron.Menu.SetApplicationMenu(menu);
			}

			return View();
		}
	}
}
