using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.EventSystems;

public class UITabsSwitch : MonoBehaviour, IPointerClickHandler
{
    public UITabGroup tabGroup;

    public Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        tabGroup.addSwitch(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }
}
