using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CardItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Dictionary<string, string> data;//��������

    public void Init(Dictionary<string, string> data) 
    {
        this.data = data;
    }

    private int index;//�ݴ�����

    //�������
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(0.6f, 0.25f);
        index = transform.GetSiblingIndex();
        transform.SetAsLastSibling();

        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.white);
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 10);
    }

    //����Ƴ�
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(0.5f, 0.25f);
        transform.SetSiblingIndex(index);

        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.black);
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 1);
    }

    private void Start()
    {
        //Id Name    Script Type    Des BgIcon  Icon Expend  Arg0 Effects
        //Ψһ�ı�ʶ�������ظ���	���� ������ӵĽű� �������͵�Id ����  ���Ƶı���ͼ��Դ·�� ͼ����Դ��·�� ���ĵķ���

        transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["BgIcon"]);
        transform.Find("bg/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Icon"]);
        
        transform.Find("bg/msgTxt").GetComponent<Text>().text  = string.Format(data["Des"], data["Arg0"]);
        transform.Find("bg/nameTxt").GetComponent<Text>().text = data["Name"];
        transform.Find("bg/useTxt").GetComponent<Text>().text  = data["Expend"];
        transform.Find("bg/Text").GetComponent<Text>().text = GameConfigManager.Instance.GetCardTypeById(data["Type"])["Name"];

        //���ÿ��Ƶ���߿����
        transform.Find("bg").GetComponent<Image>().material = Instantiate(Resources.Load<Material>("Mats/outline"));
    }

    Vector2 initPos;//��¼��ʼ��קʱ��λ��
    //��ק��ʼ
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        initPos = transform.GetComponent<RectTransform>().anchoredPosition;

        //��������
        AudioManager.Instance.PlayEffect("Cards/draw");
    }

    //��ק��
    public virtual void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out pos)) 
        {
            transform.GetComponent<RectTransform>().anchoredPosition = pos;
        }
    }

    //��ק����
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        transform.GetComponent<RectTransform>().anchoredPosition = initPos;
        transform.SetSiblingIndex(index);
    }

    //����ʹ�ÿ���
    public virtual bool TryUse()
    {
        int cost = int.Parse(data["Expend"]);//��ȡ���Ƶĺķ�

        if (cost > FightManager.Instance.CurPowCount)
        {
            //���ò���
            AudioManager.Instance.PlayEffect("Effect/lose");//����ʹ��ʧ����Ч
            UIManager.Instance.ShowTip("���ò���", Color.red);//��ʾʹ��ʧ����ʾ

            return false;
        }
        else 
        {
            FightManager.Instance.CurPowCount -= cost;//�۳��������
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();//���·���
            //ɾ����ʹ�ÿ���
            UIManager.Instance.GetUI<FightUI>("FightUI").RemoveCard(this);
            return true;
        }
    }

    //ʹ�ÿ�����Ч
    public void PlayEffect(Vector3 pos) 
    {
        GameObject effectObj = Instantiate(Resources.Load(data["Effects"])) as GameObject;
        effectObj.transform.position = pos;
        pos = new Vector3(0, 0, 0);
        Destroy(effectObj, 2);
    }
}
