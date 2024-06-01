using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System;

//攻击卡牌
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

    //按下
    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.Instance.PlayEffect("Cards/draw");//播放声音

        //绘制曲线
        UIManager.Instance.ShowUI<LineUI>("LineUI");

        UIManager.Instance.GetUI<LineUI>("LineUI").SetStartPos(transform.GetComponent<RectTransform>().anchoredPosition);//设定曲线起始点

        Cursor.visible = false;//隐藏鼠标

        StopAllCoroutines();
        StartCoroutine(OnMouseDownRight(eventData));

    }

    IEnumerator OnMouseDownRight(PointerEventData pData) 
    {
        while (true) 
        {
            //此时按下鼠标右键则跳出循环
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
                UIManager.Instance.GetUI<LineUI>("LineUI").SetEndPos(pos);//设定曲线终止点
                UIManager.Instance.GetUI<LineUI>("LineUI").DrawBezier();
                //进行射线检测判断是否触碰到敌人
                CheckRayToEnemy();
            }

            yield return null;
        }
        //跳出循环后重新显示指针
        Cursor.visible = true;
        UIManager.Instance.CloseUI("LineUI");//关闭曲线界面
    }

    //射线检测
    Enemy hitEnemy;
    private void CheckRayToEnemy()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("Enemy")))
        {
            hitEnemy = hit.transform.GetComponent<Enemy>();
            hitEnemy.OnSelect();

            //选中敌方时按下左键使用攻击卡牌
            if (Input.GetMouseButtonDown(0))
            {
                StopAllCoroutines();

                Cursor.visible = true;
                UIManager.Instance.CloseUI("LineUI");//关闭曲线界面

                if (TryUse() == true)
                {
                    PlayEffect(hitEnemy.transform.position);//播放卡牌特效
                    AudioManager.Instance.PlayEffect("Effect/sword");//播放打击音效

                    //敌人收击
                    int val = int.Parse(data["Arg0"]);
                    hitEnemy.Hit(val);
                }

                //结束
                hitEnemy.OnUnSelect();//将敌人设置为未选中
                hitEnemy = null;//清空敌人脚本
            }
        }
        else 
        {
            //射线未检测到敌人
            if (hitEnemy != null)
            {
                hitEnemy.OnUnSelect();//将敌人设置为未选中
                hitEnemy = null;//清空敌人脚本
            }
        }
    }
}
