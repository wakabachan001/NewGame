using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

public class CommandCardDrag : MonoBehaviour,IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Transform  parentCanvas;
    RectTransform rectTransform;// �ړ��������I�u�W�F�N�g��RectTransform
    CanvasGroup canvasGroup;

    private Vector2 prevPos;//�ۑ����Ă�������position

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
        // �ʒu���X�V�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�@�L�����o�X�̃T�C�Y���ς���Ă����������삷��
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) 
    {
        //  �h���b�O�O�̈ʒu�ɖ߂�
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

}
