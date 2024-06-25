using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;

public class CommandDropArea : MonoBehaviour, IDropHandler
{
     private Transform parent;
    private RectTransform dropAreaRectTransform;
    private GameObject newObject;

    void Start()
    {
        parent = this.transform;
        dropAreaRectTransform = GetComponent<RectTransform>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(gameObject.name + "��" + eventData.pointerDrag.name + "���h���b�v����܂����B");

        if (newObject != null) { Destroy(newObject); }

        if (eventData.pointerDrag != null)
        {
            // �h���b�O���Ă���I�u�W�F�N�g��RectTransform���擾
            RectTransform draggedRectTransform = eventData.pointerDrag.GetComponent<RectTransform>();

            // �h���b�v�ʒu�̕ϊ�
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                dropAreaRectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out localPoint);

            // �V�����I�u�W�F�N�g�̐���
            newObject = Instantiate(eventData.pointerDrag, dropAreaRectTransform);

            // �V�����I�u�W�F�N�g��RectTransform���I���W�i���̃T�C�Y�ɐݒ�
            RectTransform newRectTransform = newObject.GetComponent<RectTransform>();
            newRectTransform.position = dropAreaRectTransform.position;
            newRectTransform.sizeDelta = draggedRectTransform.sizeDelta;
            newRectTransform.localRotation = draggedRectTransform.localRotation;

            // �A���J�[�ƃs�{�b�g���R�s�[
            newRectTransform.anchorMin = draggedRectTransform.anchorMin;
            newRectTransform.anchorMax = draggedRectTransform.anchorMax;
            newRectTransform.pivot = draggedRectTransform.pivot;

            // �X�P�[�����R�s�[ (�e�̃X�P�[�����e�����Ȃ��悤��)
            newRectTransform.localScale = draggedRectTransform.localScale * 0.8f;

            newObject.GetComponent<CanvasGroup>().alpha=1f;
        }
    }
}
