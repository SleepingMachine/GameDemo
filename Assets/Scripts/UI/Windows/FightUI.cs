using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using System;

//战斗UI
public class FightUI : UIBase
{
    private Text  cardCountText;
    private Text  usedCardCountText;
    private Text  powerCountText;
    private Text  defenseCountText;
    private Text  hpCountText;
    private Image hpCountImg;

    private List<CardItem> cardItemList;//卡牌集合

    private void Awake()
    {
        cardItemList = new List<CardItem>();

        cardCountText = transform.Find("hasCard/icon/Text").GetComponent<Text>();
        usedCardCountText = transform.Find("noCard/icon/Text").GetComponent<Text>();
        powerCountText = transform.Find("mana/Text").GetComponent<Text>();
        hpCountText = transform.Find("hp/Text").GetComponent<Text>();
        hpCountImg = transform.Find("hp/fill").GetComponent<Image>();
        defenseCountText = transform.Find("hp/fangyu/Text").GetComponent<Text>();
        transform.Find("turnBtn").GetComponent<Button>().onClick.AddListener(onChangeTurnBtn);
    }

    //切换到玩家回合
    private void onChangeTurnBtn()
    {
        if (FightManager.Instance.fightUnit is Fight_PlayerTurn)
        {
            FightManager.Instance.ChangeType(FightType.EnemyTurn);
        }
    }

    private void Start()
    {
        UpdateHp();
        UpdatePower();
        UpdateDefense();
        UpdateDefense();
        UpdateCardCount();
        UpdateUsedCardCount();
    }

    //更新Hp信息
    public void UpdateHp()
    {
        hpCountText.text = FightManager.Instance.CurHpCount + "/" + FightManager.Instance.MaxHpCount;
        hpCountImg.fillAmount = (float)FightManager.Instance.CurHpCount / (float)FightManager.Instance.MaxHpCount;
    }

    //更新能量信息
    public void UpdatePower()
    {
        powerCountText.text = FightManager.Instance.CurPowCount + "/" + FightManager.Instance.MaxPowCount;
    }

    //更新防御信息
    public void UpdateDefense()
    {
        defenseCountText.text = FightManager.Instance.DefenseCount.ToString();
    }

    //更新卡牌堆信息
    public void UpdateCardCount()
    {
        cardCountText.text = FightCardManager.Instance.cardList.Count.ToString();
    }

    //更新弃牌堆信息
    public void UpdateUsedCardCount()
    {
        usedCardCountText.text = FightCardManager.Instance.usedCardList.Count.ToString();
    }

    //创建卡牌
    public void CreateCardItem(int count) 
    {
        //防止抽卡数溢出
        if (count >  FightCardManager.Instance.cardList.Count) 
        {
            count = FightCardManager.Instance.cardList.Count;
        }

        for (int i = 0; i < count; i++) 
        {
            GameObject obj = Instantiate(Resources.Load("UI/CardItem"), transform) as GameObject;
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-400, -350);
            obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            //var item = obj.AddComponent<CardItem>();

            string cardId = FightCardManager.Instance.DrawCard();
            Dictionary<string, string> data = GameConfigManager.Instance.GetCardById(cardId);
            CardItem item = obj.AddComponent(System.Type.GetType(data["Script"])) as CardItem;
            item.Init(data);
            cardItemList.Add(item);
        }

        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
    }

    //更新卡牌位置
    public void UpdateCardItemPos()
    {
        float offset = 400f / cardItemList.Count;
        Vector2 startPos = new Vector2(-cardItemList.Count/ 2f*offset + offset*0.5f, -350);
        for (int i = 0;i < cardItemList.Count;i++) 
        {
            cardItemList[i].GetComponent<RectTransform>().DOAnchorPos(startPos, 0.5f);
            startPos.x += offset;
        }
    }

    //删除卡牌
    public void RemoveCard(CardItem item) 
    {
        AudioManager.Instance.PlayEffect("Cards/cardShove");//播放移除卡牌音效

        item.enabled = false;//禁用卡牌
        FightCardManager.Instance.usedCardList.Add(item.data["Id"]);//添加到弃牌堆
        usedCardCountText.text = FightCardManager.Instance.usedCardList.Count.ToString();//更新弃牌堆数量
        cardItemList.Remove(item);//从集合中删除
        UpdateCardItemPos();//刷新卡牌位置

        //移动到弃牌堆并消失效果
        item.GetComponent<RectTransform>().DOAnchorPos(new Vector2(500, -350), 0.25f);
        item.transform.DOScale(0, 0.25f);

        Destroy(item.gameObject, 1);
    }

    //删除所有卡牌
    public void RemoveAllCards()
    {
        for (int i = cardItemList.Count-1; i >= 0; i--) 
        {
            RemoveCard(cardItemList[i]);
        }
    }
}
