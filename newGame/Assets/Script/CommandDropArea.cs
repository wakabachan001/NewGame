using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CommandDropArea : MonoBehaviour, IDropHandler
{
     private Transform parent;
    private RectTransform dropAreaRectTransform;


    void Start()
    {
        parent = this.transform;
        dropAreaRectTransform = GetComponent<RectTransform>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(gameObject.name + "に" + eventData.pointerDrag.name + "がドロップされました。");
        //if (eventData.pointerDrag != null)
        //{
        //    Instantiate(eventData.pointerDrag, GetComponent<RectTransform>().anchoredPosition, Quaternion.identity, parent);
        //}

        if (eventData.pointerDrag != null)
        {
            // ドラッグしているオブジェクトのRectTransformを取得
            RectTransform draggedRectTransform = eventData.pointerDrag.GetComponent<RectTransform>();

            // ドロップ位置の変換
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                dropAreaRectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out localPoint);

            // 新しいオブジェクトの生成
            GameObject newObject = Instantiate(eventData.pointerDrag, dropAreaRectTransform);

            // 新しいオブジェクトのRectTransformをオリジナルのサイズに設定
            RectTransform newRectTransform = newObject.GetComponent<RectTransform>();
            newRectTransform.position = dropAreaRectTransform.position;
            newRectTransform.sizeDelta = draggedRectTransform.sizeDelta;
            newRectTransform.localRotation = draggedRectTransform.localRotation;

            // アンカーとピボットをコピー
            newRectTransform.anchorMin = draggedRectTransform.anchorMin;
            newRectTransform.anchorMax = draggedRectTransform.anchorMax;
            newRectTransform.pivot = draggedRectTransform.pivot;

            // スケールをコピー (親のスケールが影響しないように)
            newRectTransform.localScale = draggedRectTransform.localScale * 0.8f;

            newObject.GetComponent<CanvasGroup>().alpha=1f;
        }
    }
}
