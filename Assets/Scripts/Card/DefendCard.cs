using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//��������
public class DefendCard : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (TryUse() == true)
        {
            //����Ч��
            int val = int.Parse(data["Arg0"]);
            FightManager.Instance.DefenseCount += val;
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense();

            //������Ч
            AudioManager.Instance.PlayEffect("Effect/healspell");

            Vector3 pos = Camera.main.transform.position;
            pos.y -= 0.5f;
            PlayEffect(pos);
        }
        else 
        {
            base.OnEndDrag(eventData);
        }
        
    }
}
