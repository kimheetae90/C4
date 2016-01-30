using UnityEngine;
using System.Collections;
using System;
using System.Xml;

public class CXMLTool : MonoBehaviour
{
    EditorMode editorMode;

    XmlNode toolNode;
    XmlDocument toolDoc;
    XmlNodeList toolNodeList;

    XmlNode monsterNode;
    XmlDocument monsterDoc;
    XmlNodeList monsterNodeList;

    XmlDocument prevToolDoc;
    XmlDocument prevMonDoc;

    XmlNode curNode;

    Vector2 mainScroll;
    Vector2 dataScroll;

    enum EditorMode
    {
        None,
        EditTool,
        EditMonster
    }

    void Start()
    {
        Debug.Log("Start");

        editorMode = EditorMode.None;
        LoadData();
    }

    public void LoadData()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Data/Tool");
        toolDoc = new XmlDocument();
        toolDoc.LoadXml(textAsset.text);
        toolNode = toolDoc.SelectSingleNode("ToolData");
        toolNodeList = toolNode.SelectNodes("Tool");

        textAsset = (TextAsset)Resources.Load("Data/Monster");
        monsterDoc = new XmlDocument();
        monsterDoc.LoadXml(textAsset.text);
        monsterNode = monsterDoc.SelectSingleNode("MonsterData");
        monsterNodeList = monsterNode.SelectNodes("Monster");
    }

    void OnGUI()
    {
        OnMainMenu();
        OnChangeDataMenu();
    }

    void OnMainMenu()
    {
        GUILayout.BeginArea(new Rect(5, 5, 254, Screen.height - 10));
        {
            GUILayout.BeginVertical("box");
            {
                GUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("도구 수정", GUILayout.Width(120)))
                    {
                        editorMode = EditorMode.EditTool;
                        curNode = null;
                    }
                    if (GUILayout.Button("몬스터 수정", GUILayout.Width(120)))
                    {
                        editorMode = EditorMode.EditMonster;
                        curNode = null;
                    }
                }
                GUILayout.EndHorizontal();

                mainScroll = GUILayout.BeginScrollView(mainScroll);
                {
                    OnSelect();
                }
                GUILayout.EndScrollView();
            }
            GUILayout.EndVertical();
        }
        GUILayout.EndArea();
    }

    private void OnSelect()
    {
        if (editorMode == EditorMode.EditTool)
        {
            GUILayout.BeginVertical();
            {
                foreach (XmlNode tNode in toolNodeList)
                {
                    string name = tNode["name"].InnerText;

                    if (GUILayout.Button(tNode["id"].InnerText + " (" + name + ")"))
                    {
                        curNode = tNode;
                    }
                }
            }
            GUILayout.EndVertical();
        }
        else if (editorMode == EditorMode.EditMonster)
        {
            GUILayout.BeginVertical();
            {
                foreach (XmlNode tNode in monsterNodeList)
                {
                    string name = tNode["name"].InnerText;

                    if (GUILayout.Button(tNode["id"].InnerText + " (" + name + ")"))
                    {
                        curNode = tNode;
                    }
                }
            }
            GUILayout.EndVertical();
        }
    }

    private void OnChangeDataMenu()
    {
        GUILayout.BeginArea(new Rect(Screen.width - 500, 5, 500 - 5, Screen.height - 10));
        {
            GUILayout.BeginVertical("box");
            {
                OnCurrInfo();

                dataScroll = GUILayout.BeginScrollView(dataScroll);
                {
                    OnEditData();
                }
                GUILayout.EndScrollView();

                GUILayout.FlexibleSpace();

                OnSaveData();
            }
            GUILayout.EndVertical();
        }
        GUILayout.EndArea();
    }

    void OnCurrInfo()
    {
        if (curNode == null)
        {
            GUILayout.Label(" 선택한 툴 혹은 몬스터가 존재하지 않습니다.");
        }
        else
        {
            GUILayout.BeginHorizontal();
            {
                if (editorMode == EditorMode.EditTool)
                {
                    GUILayout.Label(" 현재 선택한 툴 :", GUILayout.Width(98));
                }
                else if (editorMode == EditorMode.EditMonster)
                {
                    GUILayout.Label(" 현재 선택한 몬스터 :", GUILayout.Width(124));
                }

                GUILayout.Label(curNode["id"].InnerText + "(" + curNode["name"].InnerText + ")");
            }
            GUILayout.EndHorizontal();
        }

    }

    string[] toolDataList = { "id", "name" , "power" , "range" , "attackSpeed" , "hp" , "piercingForce" , "moveSpeed", "upgradePower", "upgradeRange", "upgradeAttackSpeed",
        "upgradeHp" , "upgradePiercingForce", "upgradeMoveSpeed" ,"price" , "upgradePrice" };
    string[] monDataList = { "id", "name", "hp", "power", "cooldownTime", "attackSpeed", "range", "moveSpeed", "skillID" };

    private void OnEditData()
    {
        if (curNode == null) return;

        if (editorMode == EditorMode.EditTool)
        {
            foreach(var td in toolDataList)
            {
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label(td, GUILayout.Width(200));
                    curNode[td].InnerText = GUILayout.TextArea(curNode[td].InnerText, GUILayout.Width(80));
                }
                GUILayout.EndHorizontal();
            }
        }
        else if (editorMode == EditorMode.EditMonster)
        {
            foreach (var md in monDataList)
            {
                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label(md, GUILayout.Width(200));
                    curNode[md].InnerText = GUILayout.TextArea(curNode[md].InnerText, GUILayout.Width(80));
                }
                GUILayout.EndHorizontal();
            }
        }
    }

    private void OnSaveData()
    {
        if (curNode == null) return;

        if (GUILayout.Button("저장", GUILayout.Height(60), GUILayout.MaxWidth(200), GUILayout.MinWidth(200)))
        {
            toolDoc.Save("Assets/Resources/Data/Tool.xml");
            monsterDoc.Save("Assets/Resources/Data/Monster.xml");

            Debug.Log("save");
        }
    }

}
