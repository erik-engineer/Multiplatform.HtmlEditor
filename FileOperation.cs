using System.IO;

namespace MultiPlatform.HtmlEditor
{
	public static class FileOperation
	{

		public static string openRead(string filename)
		{
			string t = "";
			if (File.Exists(filename))
			{
				if (filename != null)
					t = File.ReadAllText(filename);
			}
		    return t;
		}

		public static void saveFile(string fileContent,string filename)
		{
			createFile(filename);
			if (File.Exists(filename))
			{
				if (filename != null) {
					using (StreamWriter outputFile = new StreamWriter(filename))
					{
						outputFile.Write(fileContent);
					}
				}
			}

		}

		public static void createFile(string filename)
		{
			if (filename != null)
				{
					using (StreamWriter outputFile = new StreamWriter(filename))
					{
						outputFile.Write("");
					}
				}		
		}

	}
}
