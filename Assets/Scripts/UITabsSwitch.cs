using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.EventSystems;

public class UITabsSwitch : MonoBehaviour
{
    public UITabGroup tabGroup;

    public Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        tabGroup.addSwitch(this);
    }

    public void OnClick()
    {
        tabGroup.OnTabSelected(this);
    }
}
