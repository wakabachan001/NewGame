using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CommandDropArea : MonoBehaviour, IDropHandler
{
     private Transform parent;

    void Start()
    {
        parent = this.transform;
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(gameObject.name + "に" + eventData.pointerDrag.name + "がドロップされました。");
        if (eventData.pointerDrag != null)
        {
            Instantiate(eventData.pointerDrag, GetComponent<RectTransform>().anchoredPosition, Quaternion.identity, parent);
        }
    }
}
