using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class PanelManager : MonoBehaviour
{

    private Transform parent;
    private string path;

    private string[] exclude = new string[]
    {
        "World",
        "Summon",
        "SummonTips"
    };

    public Transform Parent
    {
        get
        {
            if (this.parent == null)
            {
                GameObject gui = io.Gui;
                if (gui)
                {
                    this.parent = gui.transform.Find("Camera");
                }
            }
            return this.parent;
        }
    }

    public void CreatePanel(DialogType type)
    {
#if UNITY_EDITOR
        string typename = Util.ConvertPanelName(type);
        this.CreatePanel(typename);
#else
        base.StartCoroutine(this.OnCreatePanel(type));
#endif

    }

    private IEnumerator OnCreatePanel(DialogType type)
    {
        path = Util.AppContentDataUri + "UI/"+ type.ToString() + "Panel.unity3d";
        GameObject go = null;

        WWW bundle = new WWW(path);
        yield return bundle;

        try
        {
            if (bundle.assetBundle.Contains(type.ToString() + "Panel"))
            {
                go = Instantiate(bundle.assetBundle.Load(type.ToString() + "Panel", typeof(GameObject))) as GameObject;
            }

        }
        catch (System.Exception ex)
        {
            NGUIDebug.Log("catch go....  " + ex.ToString());
        }

        go.name = type.ToString() + "Panel";
        if (type.ToString() == "Main")
        {
            Util.SetBackground("MainUI/MainGround");
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
        }
        else
        {
             if (type.ToString() != "World" && type.ToString() != "Summon" && type.ToString() != "SummonTips")
             {
                 Util.SetBackground(null);
             }
            
            go.transform.parent = UIContainer.instance.transform;
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
        }

        bundle.assetBundle.Unload(false);
    }

    public void CreatePanel(string name)
    {
        if (this.Parent.FindChild(name) != null)
        {
            return;
        }

        GameObject gameObject = Util.LoadPrefab(Const.PanelPrefabDir + name + ".prefab");
        if (gameObject == null)
        {
            return;
        }

        if (name == "MainPanel" )
        {
            Util.SetBackground("MainUI/MainGround");
            GameObject go = GameObject.Instantiate(gameObject) as GameObject;
            go.transform.localPosition = new Vector3(0f, 0f, 0f);
            go.name = name;
            this.OnCreatePanel(name, go);
        }
        else
        {
            if (name != "WorldPanel" && name != "SummonPanel" && name != "SummonTipsPanel")
            {
                 Util.SetBackground(null);
            }

            GameObject gameObject2 = Util.AddChild(gameObject, UIContainer.instance.transform);
            gameObject2.name = name;
            gameObject2.transform.localPosition = new Vector3(0f, 0f, -5f);

            this.OnCreatePanel(name, gameObject2);
        }

    }

    private void OnCreatePanel(string name, GameObject go)
    {
        switch (name)
        {
            case "LoginPanel":
                this.OnLoginPanel(go);
                break;
            case "CharacterPanel":
                this.OnCharacterPanel(go);
                break;
            case "MainPanel":
                this.OnMainPanel(go);
                break;
            case "WorldPanel":
                this.OnWorldPanel(go);
                break;
            case "DuplicatePanel":
                this.OnDuplicatePanel(go);
                break;
            case "FightPanel":
                this.OnFightPanel(go);
                break;
            case "HealthPanel":
                this.OnHealthPanel(go);
                break;
        }
    }

    private void OnLoginPanel(GameObject go)
    {
        go.transform.localPosition = new Vector3(0f, 0f, 0f);
        io.container.loginPanel = go;
    }

    private void OnCharacterPanel(GameObject go)
    {
        go.transform.localPosition = new Vector3(0f, 0f, 0f);
        io.container.characterPanel = go;
    }

    private void OnMainPanel(GameObject go)
    {
        go.transform.localPosition = new Vector3(0f, 0f, 0f);
        io.container.mainPanel = go;
    }

    private void OnWorldPanel(GameObject go)
    {
        go.transform.localPosition = new Vector3(0f, 0f, 0f);
        io.container.worldPanel = go;
    }

    private void OnDuplicatePanel(GameObject go)
    {
        go.transform.localPosition = new Vector3(0f, 0f, 0f);
        io.container.duplicatePanel = go;
    }

    private void OnFightPanel(GameObject go)
    {
        go.transform.localPosition = new Vector3(-492f, -190f, 0f);
        io.container.fightPanel = go;
    }

    private void OnHealthPanel(GameObject go)
    {
        go.transform.localPosition = new Vector3(0f, 0f, 0f);
        io.container.healthPanel = go;
    }
}
