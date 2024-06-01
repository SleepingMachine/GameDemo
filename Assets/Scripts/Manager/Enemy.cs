using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//���˽ű�

//�����ж�����
public enum ActionType 
{
    None,
    Defend,
    Attact,
    End
}

public class Enemy : MonoBehaviour
{
    protected Dictionary<string, string> data;//������Ϣ

    public ActionType type;

    public GameObject hpItemObj;
    public GameObject actionIconObj;

    //UI��ʾ���
    public Transform attackIconTF;
    public Transform defendIconTF;
    public Text hpText;
    public Text defendText;
    public Image hpImg;

    //UI��ֵ���
    public int Defend;
    public int Attack;
    public int MaxHp;
    public int CurHp;

    //������
    SkinnedMeshRenderer _meshRenderer;
    public Animator ani;


    public void Init(Dictionary<string, string> data) 
    {
        this.data = data;
    }

    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = transform.GetComponentInChildren<SkinnedMeshRenderer>();
        ani = transform.GetComponent<Animator>();

        type = ActionType.None;

        //��UI���
        hpItemObj = UIManager.Instance.CreateHpItem();
        actionIconObj = UIManager.Instance.CreateActionIcon();

        attackIconTF = actionIconObj.transform.Find("attack");
        defendIconTF = actionIconObj.transform.Find("defend");

        defendText = hpItemObj.transform.Find("fangyu/Text").GetComponent<Text>();
        hpText = hpItemObj.transform.Find("hpText").GetComponent<Text>();
        hpImg = hpItemObj.transform.Find("fill").GetComponent<Image>();

        //����Ѫ�����ж���ʾλ�úʹ�С
        hpItemObj.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.down*0.2f);
        hpItemObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        actionIconObj.transform.position = Camera.main.WorldToScreenPoint(transform.Find("head").position + Vector3.up*0.2f);
        actionIconObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        //��ʼ����ֵ
        Attack = int.Parse(data["Attack"]);
        Defend = int.Parse(data["Defend"]);
        CurHp = int.Parse(data["Hp"]);
        MaxHp = CurHp;

        SetRandomAction();
        UpdataDefend();
        UpdataHp();
    }

    //��������ж���ͼ
    public void SetRandomAction() 
    {
        int random = Random.Range(1, (int)ActionType.End);
        type = (ActionType)random;

        switch (type) 
        {
            case ActionType.None:
                break;
            case ActionType.Defend:
                attackIconTF.gameObject.SetActive(false);
                defendIconTF.gameObject.SetActive(true);
                break;
            case ActionType.Attact:
                attackIconTF.gameObject.SetActive(true);
                defendIconTF.gameObject.SetActive(false);
                break;
        }
    }

    //����Hp��Ϣ
    public void UpdataHp() 
    {
        hpText.text = CurHp + "/" + MaxHp;
        hpImg.fillAmount = (float)CurHp / (float)MaxHp;
    }

    //���·�����Ϣ
    public void UpdataDefend()
    {
        defendText.text = Defend.ToString();
    }

    //������ѡ����ʾ
    public void OnSelect() 
    {
        _meshRenderer.material.SetColor("_OtlColor", Color.yellow);
    }

    //δ������ѡ��
    public void OnUnSelect()
    {
        _meshRenderer.material.SetColor("_OtlColor", Color.black);
    }

    //�ܻ�
    public void Hit(int val)
    {
        //�ȿۻ���
        if (Defend >= val)
        {
            Defend -= val;
            ani.Play("hit", 0, 0);//�������˶���
        }
        else 
        {
            val -= Defend;
            Defend = 0;
            CurHp -= val;

            //����
            if (CurHp <= 0)
            {
                CurHp = 0;

                //������������
                ani.Play("die");
                EnemyManager.Instance.DeleteEnemy(this);

                Destroy(gameObject, 1);
                Destroy(actionIconObj);
                Destroy(hpItemObj);
            }
            else 
            {
                ani.Play("hit", 0, 0);
            }
        }

        //ˢ��UI��Ϣ
        UpdataDefend();
        UpdataHp();
    }

    //���ع�����ͼ
    public void HideAction() 
    {
        attackIconTF.gameObject.SetActive(false);
        defendIconTF.gameObject.SetActive(false);
    }

    //ִ�е����ж�
    public IEnumerator DoAction() 
    {
        HideAction();

        //TODO���������Ͷ�����ʱ��ӵ�Excel���Դ��������Ĭ�Ϲ���������Ĭ����ʱ
        ani.Play("attack");
        yield return new WaitForSeconds(0.5f);

        //TODO�������Ч
        switch (type) 
        {
            case ActionType.None:
                break;
            case ActionType.Defend:
                Defend += 1;
                UpdataDefend();
                break;
            case ActionType.Attact:
                FightManager.Instance.GetPlayerHit(Attack);
                Camera.main.DOShakePosition(0.1f, 0.2f, 5, 45);
                break;
        }
    }
}
