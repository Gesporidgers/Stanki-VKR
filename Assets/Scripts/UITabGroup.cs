using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class UITabGroup : MonoBehaviour
{
    public List<UITabsSwitch> Switchs;
    public List<GameObject> Tabs;

    private void Start()
    {
        if (Tabs != null)
        {
            Tabs[0].SetActive(true);
            for (int i = 1; i < Tabs.Count; i++)
            {
                Tabs[i].SetActive(false);
            }
        }
    }

    public void addSwitch(UITabsSwitch _switch)
    {
        if (Switchs == null)
        {
            Switchs = new List<UITabsSwitch>();
        }
        Switchs.Add(_switch);
    }

    public void OnTabSelected(UITabsSwitch _switch)
    {
        int id = _switch.transform.GetSiblingIndex();
        for (int i = 0; i < Tabs.Count; i++)
        {
            if( i == id )
                Tabs[i].SetActive(true);
            else
                Tabs[i].SetActive(false);
        }

    }
}
