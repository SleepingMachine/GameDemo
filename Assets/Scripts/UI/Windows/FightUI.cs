using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;
using System;

//ս��UI
public class FightUI : UIBase
{
    private Text  cardCountText;
    private Text  usedCardCountText;
    private Text  powerCountText;
    private Text  defenseCountText;
    private Text  hpCountText;
    private Image hpCountImg;

    private List<CardItem> cardItemList;//���Ƽ���

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

    //�л�����һغ�
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

    //����Hp��Ϣ
    public void UpdateHp()
    {
        hpCountText.text = FightManager.Instance.CurHpCount + "/" + FightManager.Instance.MaxHpCount;
        hpCountImg.fillAmount = (float)FightManager.Instance.CurHpCount / (float)FightManager.Instance.MaxHpCount;
    }

    //����������Ϣ
    public void UpdatePower()
    {
        powerCountText.text = FightManager.Instance.CurPowCount + "/" + FightManager.Instance.MaxPowCount;
    }

    //���·�����Ϣ
    public void UpdateDefense()
    {
        defenseCountText.text = FightManager.Instance.DefenseCount.ToString();
    }

    //���¿��ƶ���Ϣ
    public void UpdateCardCount()
    {
        cardCountText.text = FightCardManager.Instance.cardList.Count.ToString();
    }

    //�������ƶ���Ϣ
    public void UpdateUsedCardCount()
    {
        usedCardCountText.text = FightCardManager.Instance.usedCardList.Count.ToString();
    }

    //��������
    public void CreateCardItem(int count) 
    {
        //��ֹ�鿨�����
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

    //���¿���λ��
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

    //ɾ������
    public void RemoveCard(CardItem item) 
    {
        AudioManager.Instance.PlayEffect("Cards/cardShove");//�����Ƴ�������Ч

        item.enabled = false;//���ÿ���
        FightCardManager.Instance.usedCardList.Add(item.data["Id"]);//��ӵ����ƶ�
        usedCardCountText.text = FightCardManager.Instance.usedCardList.Count.ToString();//�������ƶ�����
        cardItemList.Remove(item);//�Ӽ�����ɾ��
        UpdateCardItemPos();//ˢ�¿���λ��

        //�ƶ������ƶѲ���ʧЧ��
        item.GetComponent<RectTransform>().DOAnchorPos(new Vector2(500, -350), 0.25f);
        item.transform.DOScale(0, 0.25f);

        Destroy(item.gameObject, 1);
    }

    //ɾ�����п���
    public void RemoveAllCards()
    {
        for (int i = cardItemList.Count-1; i >= 0; i--) 
        {
            RemoveCard(cardItemList[i]);
        }
    }
}
