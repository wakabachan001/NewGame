using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;

public class CommandDropArea : MonoBehaviour, IDropHandler
{
    private GameManager gameManager;
    private RectTransform dropAreaRectTransform;
    private GameObject SelectedCommand;

    void Start()
    {
        dropAreaRectTransform = GetComponent<RectTransform>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(gameObject.name + "��" + eventData.pointerDrag.name + "���h���b�v����܂����B");

        if (SelectedCommand != null) { Destroy(SelectedCommand); GameManager.CommandList.Remove(SelectedCommand); }

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
            SelectedCommand = Instantiate(eventData.pointerDrag, dropAreaRectTransform);

            // �V�����I�u�W�F�N�g��RectTransform���I���W�i���̃T�C�Y�ɐݒ�
            RectTransform SelectedCommandRectTransform = SelectedCommand.GetComponent<RectTransform>();
            SelectedCommandRectTransform.position = dropAreaRectTransform.position;
            SelectedCommandRectTransform.sizeDelta = draggedRectTransform.sizeDelta;
            SelectedCommandRectTransform.localRotation = draggedRectTransform.localRotation;

            // �X�P�[����ύX�A�i�h���b�v�G���A�̘g���Ɏ��܂�悤�ɂ���j
            SelectedCommandRectTransform.localScale = draggedRectTransform.localScale * 0.8f;

            SelectedCommand.GetComponent<CanvasGroup>().alpha=1f;

            GameManager.CommandList.Add(SelectedCommand);

            for(int i =0; i< GameManager.CommandList.Count;i++)
            {
                Debug.Log(GameManager.CommandList[i].name.StartsWith("Sword"));
            }
            
        }
    }
}
