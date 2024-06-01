using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class SelectCardUI : UIBase
{
    private void Awake()
    {
        //·µ»Ø°´Å¥
        Register("bg/content/returnBtn").onClick = onReturnBtn;

        AudioManager.Instance.PlayBGM("BGM1");

        transform.Find("bg/content").GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 1);
    }

    private void onReturnBtn(GameObject obj, PointerEventData pData)
    {
        Close();
        FightManager.Instance.ChangeType(FightType.Init);
    }
}
