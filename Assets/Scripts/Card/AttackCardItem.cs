using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System;

//��������
public class AttackCardItem : CardItem, IPointerDownHandler
{
    public override void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public override void OnDrag(PointerEventData eventData)
    {

    }

    public override void OnEndDrag(PointerEventData eventData)
    {

    }

    //����
    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.Instance.PlayEffect("Cards/draw");//��������

        //��������
        UIManager.Instance.ShowUI<LineUI>("LineUI");

        UIManager.Instance.GetUI<LineUI>("LineUI").SetStartPos(transform.GetComponent<RectTransform>().anchoredPosition);//�趨������ʼ��

        Cursor.visible = false;//�������

        StopAllCoroutines();
        StartCoroutine(OnMouseDownRight(eventData));

    }

    IEnumerator OnMouseDownRight(PointerEventData pData) 
    {
        while (true) 
        {
            //��ʱ��������Ҽ�������ѭ��
            if (Input.GetMouseButton(1)) 
            {
                break;
            }

            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform.parent.GetComponent<RectTransform>(),
                pData.position, 
                pData.pressEventCamera, 
                out pos
                ))
            {
                UIManager.Instance.GetUI<LineUI>("LineUI").SetEndPos(pos);//�趨������ֹ��
                UIManager.Instance.GetUI<LineUI>("LineUI").DrawBezier();
                //�������߼���ж��Ƿ���������
                CheckRayToEnemy();
            }

            yield return null;
        }
        //����ѭ����������ʾָ��
        Cursor.visible = true;
        UIManager.Instance.CloseUI("LineUI");//�ر����߽���
    }

    //���߼��
    Enemy hitEnemy;
    private void CheckRayToEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("Enemy")))
        {
            hitEnemy = hit.transform.GetComponent<Enemy>();
            hitEnemy.OnSelect();

            //ѡ�ез�ʱ�������ʹ�ù�������
            if (Input.GetMouseButtonDown(0))
            {
                StopAllCoroutines();

                Cursor.visible = true;
                UIManager.Instance.CloseUI("LineUI");//�ر����߽���

                if (TryUse() == true)
                {
                    PlayEffect(hitEnemy.transform.position);//���ſ�����Ч
                    AudioManager.Instance.PlayEffect("Effect/sword");//���Ŵ����Ч

                    //�����ջ�
                    int val = int.Parse(data["Arg0"]);
                    hitEnemy.Hit(val);
                }

                //����
                hitEnemy.OnUnSelect();//����������Ϊδѡ��
                hitEnemy = null;//��յ��˽ű�
            }
        }
        else 
        {
            //����δ��⵽����
            if (hitEnemy != null)
            {
                hitEnemy.OnUnSelect();//����������Ϊδѡ��
                hitEnemy = null;//��յ��˽ű�
            }
        }
    }
}
