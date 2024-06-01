using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//敌人脚本

//敌人行动类型
public enum ActionType 
{
    None,
    Defend,
    Attact,
    End
}

public class Enemy : MonoBehaviour
{
    protected Dictionary<string, string> data;//敌人信息

    public ActionType type;

    public GameObject hpItemObj;
    public GameObject actionIconObj;

    //UI显示相关
    public Transform attackIconTF;
    public Transform defendIconTF;
    public Text hpText;
    public Text defendText;
    public Image hpImg;

    //UI数值相关
    public int Defend;
    public int Attack;
    public int MaxHp;
    public int CurHp;

    //组件相关
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

        //绑定UI组件
        hpItemObj = UIManager.Instance.CreateHpItem();
        actionIconObj = UIManager.Instance.CreateActionIcon();

        attackIconTF = actionIconObj.transform.Find("attack");
        defendIconTF = actionIconObj.transform.Find("defend");

        defendText = hpItemObj.transform.Find("fangyu/Text").GetComponent<Text>();
        hpText = hpItemObj.transform.Find("hpText").GetComponent<Text>();
        hpImg = hpItemObj.transform.Find("fill").GetComponent<Image>();

        //设置血条和行动提示位置和大小
        hpItemObj.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.down*0.2f);
        hpItemObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        actionIconObj.transform.position = Camera.main.WorldToScreenPoint(transform.Find("head").position + Vector3.up*0.2f);
        actionIconObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        //初始化数值
        Attack = int.Parse(data["Attack"]);
        Defend = int.Parse(data["Defend"]);
        CurHp = int.Parse(data["Hp"]);
        MaxHp = CurHp;

        SetRandomAction();
        UpdataDefend();
        UpdataHp();
    }

    //设置随机行动意图
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

    //更新Hp信息
    public void UpdataHp() 
    {
        hpText.text = CurHp + "/" + MaxHp;
        hpImg.fillAmount = (float)CurHp / (float)MaxHp;
    }

    //更新防御信息
    public void UpdataDefend()
    {
        defendText.text = Defend.ToString();
    }

    //被卡牌选中提示
    public void OnSelect() 
    {
        _meshRenderer.material.SetColor("_OtlColor", Color.yellow);
    }

    //未被卡牌选中
    public void OnUnSelect()
    {
        _meshRenderer.material.SetColor("_OtlColor", Color.black);
    }

    //受击
    public void Hit(int val)
    {
        //先扣护盾
        if (Defend >= val)
        {
            Defend -= val;
            ani.Play("hit", 0, 0);//播放受伤动画
        }
        else 
        {
            val -= Defend;
            Defend = 0;
            CurHp -= val;

            //噶了
            if (CurHp <= 0)
            {
                CurHp = 0;

                //播放死亡动画
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

        //刷新UI信息
        UpdataDefend();
        UpdataHp();
    }

    //隐藏怪物意图
    public void HideAction() 
    {
        attackIconTF.gameObject.SetActive(false);
        defendIconTF.gameObject.SetActive(false);
    }

    //执行敌人行动
    public IEnumerator DoAction() 
    {
        HideAction();

        //TODO：将动画和动画延时添加到Excel中以代替这里的默认攻击动画和默认延时
        ani.Play("attack");
        yield return new WaitForSeconds(0.5f);

        //TODO：添加特效
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
