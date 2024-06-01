using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

//����ѡȡ���߻���
public class LineUI : UIBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //������������λ��
    public void SetStartPos(Vector2 pos) 
    {
        transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = pos;
    }

    //�������߽���λ��
    public void SetEndPos(Vector2 pos)
    {
        transform.GetChild(transform.childCount-1).GetComponent<RectTransform>().anchoredPosition = pos;
    }

    public void DrawBezier() 
    {
        Vector3 startPos = transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition;
        Vector3 endPos = transform.GetChild(transform.childCount - 1).GetComponent<RectTransform>().anchoredPosition;
        Vector3 midPos = Vector3.zero;
        midPos.x = startPos.x;
        midPos.y = (startPos.y + endPos.y) * 0.5f;

        //���㷽��Ƕ�
        Vector3 dir = (endPos - startPos).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        //�����յ�Ƕ�
        transform.GetChild(transform.childCount - 1).eulerAngles = new Vector3(0, 0, angle-90);

        //���������ڵ�ĽǶȡ�λ�úʹ�С
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = GetBezier(startPos, midPos, endPos, i / (float)transform.childCount);
            
            if (i != transform.childCount - 1)
            {
                dir = (transform.GetChild(i + 1).GetComponent<RectTransform>().anchoredPosition - transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition).normalized;
                angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.GetChild(i).eulerAngles = new Vector3(0, 0, angle-90);

                float scale = (float)(0.7 * i / (transform.childCount - 1));
                transform.GetChild(i).localScale = new Vector3(scale, scale, 1f);
            }
        }  
    }

    //����������
    public Vector3 GetBezier(Vector3 start, Vector3 mid, Vector3 end, float t) 
    {
        return (1.0f - t) * (1.0f - t) * start + 2.0f * t * (1.0f - t) * mid + t * t * end;
    }
}
