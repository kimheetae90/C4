using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class PathUtility 
{
	public static bool DirSearch(string sDir,string extention,ref List<string> searchedfiles) 
	{
		if (searchedfiles == null)
			return false;

		try	
		{
			var info = new DirectoryInfo(sDir);
			var fileInfo = info.GetFiles();
			var dirsInfo = info.GetDirectories();

			foreach(var file in fileInfo)
			{
				if(file.Extension == extention)
				{
					searchedfiles.Add(rebuildToAssetsPath(file.FullName));
				}
			}

			foreach(var dir in dirsInfo)
			{
				DirSearch(dir.FullName,extention,ref searchedfiles);
			}
		}
		catch (System.Exception excpt) 
		{
			throw new ToolException(excpt.Message);
		}

		return true;
	}

	private static string rebuildToAssetsPath(string path)
	{
		string name = "";
		string[] array = path.Split (Path.DirectorySeparatorChar);

		bool findAsset = false;
		for(int i = 0; i < array.Length ; ++i)
		{
			if(findAsset)
			{
				name += "/"+array[i];
			}

			if(array[i] == "Assets")
			{
				findAsset = true;
				name += array[i];
			}
		}
	
		return name;
	}
}
