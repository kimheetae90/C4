using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AssetsSelectFilesInfo
{
    public List<string> listFilePaths;
    public string[]     strArrayFilePaths = { "" };
    public int          selectedFileIndex;
    public bool         bNeedUpdate;

    public AssetsSelectFilesInfo()
    {
        listFilePaths = new List<string>();
        selectedFileIndex = -1;
        bNeedUpdate = true;
    }

    public bool isValidIndex()
    {
        return selectedFileIndex >= 0 && listFilePaths.Count > 0;
    }

}