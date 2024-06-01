using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//≥È≈∆ø®≈∆
public class AddCard : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (TryUse() == true)
        {
            int val = int.Parse(data["Arg0"]);
            //≈–∂œ≥È≈∆∂—÷– «∑Òªπ”–ø®≈∆
            if (FightCardManager.Instance.HasCard() == true)
            {
                UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(val);
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();

                Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2.5f));
                PlayEffect(pos);
            }
            else
            {
                base.OnEndDrag(eventData);
            }
        }
        else 
        {
            base.OnEndDrag(eventData);
        }
        
    }
}
