using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

public class CommandCardDrag : MonoBehaviour,IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Transform  parentCanvas;
    RectTransform rectTransform;// 移動したいオブジェクトのRectTransform
    CanvasGroup canvasGroup;

    private Vector2 prevPos;//保存しておく初期position

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        Instantiate(this, rectTransform.anchoredPosition, Quaternion.identity, parentCanvas);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 位置を更新　　　　　　　　　　　　　　　　　　　　キャンバスのサイズが変わっても正しく動作する
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) 
    {
        //  ドラッグ前の位置に戻す
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

}
