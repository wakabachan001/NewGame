using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;

public class CommandDropArea : MonoBehaviour, IDropHandler
{
    public static List<GameObject> CommandList = new List<GameObject>();
    private RectTransform dropAreaRectTransform;
    private GameObject SelectedCommand;

    void Start()
    {
        dropAreaRectTransform = GetComponent<RectTransform>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(gameObject.name + "に" + eventData.pointerDrag.name + "がドロップされました。");

        if (SelectedCommand != null) { Destroy(SelectedCommand); }

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
            SelectedCommand = Instantiate(eventData.pointerDrag, dropAreaRectTransform);

            // 新しいオブジェクトのRectTransformをオリジナルのサイズに設定
            RectTransform SelectedCommandRectTransform = SelectedCommand.GetComponent<RectTransform>();
            SelectedCommandRectTransform.position = dropAreaRectTransform.position;
            SelectedCommandRectTransform.sizeDelta = draggedRectTransform.sizeDelta;
            SelectedCommandRectTransform.localRotation = draggedRectTransform.localRotation;

            // スケールを変更、（ドロップエリアの枠内に収まるようにする）
            SelectedCommandRectTransform.localScale = draggedRectTransform.localScale * 0.8f;

            SelectedCommand.GetComponent<CanvasGroup>().alpha=1f;

            CommandList.Add(SelectedCommand);

            for(int i =0; i<CommandList.Count;i++)
            {
                Debug.Log(CommandList[i].name.StartsWith("Sword"));
            }
            
        }
    }
}
