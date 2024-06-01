using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Excel;
using System.Data;

//�༭���ű�
public static class MyEditor
{
    [MenuItem("���ƹ���/ExcelתTxt")]
    public static void ExportExcel2Txt() 
    {
        //_Excel�ļ���·��
        string assetPath = Application.dataPath + "/_Excel";
        //���Excel�ļ�
        string[] files = Directory.GetFiles(assetPath, "*.xlsx");
        for (int i = 0; i < files.Length; i++) 
        {
            //�滻�ļ����еķ�б��
            files[i] = files[i].Replace('\\', '/');
            //�ļ�����ȡ
            using(FileStream fs = File.Open(files[i], FileMode.Open, FileAccess.Read))
            { 
                //�ļ���->Excel����
                var excelDateReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                //��ȡExcel����
                DataSet dataSet = excelDateReader.AsDataSet();
                //��ȡ��0
                DataTable table = dataSet.Tables[0];
                //����������д�뵽Txt
                readTable2Txt(files[i], table);
            }
        }

        //ˢ��
        AssetDatabase.Refresh();
    }

    private static void readTable2Txt(string filePath, DataTable table)
    { 
        //��ȡ�����к�׺���ļ�������д��Txt
        string fileName = Path.GetFileNameWithoutExtension(filePath);
        string path = Application.dataPath + "/Resources/Data/" + fileName + ".txt";

        //�ļ�������Txt
        using(FileStream fs = new FileStream(path, FileMode.Create))
        {
            //�ļ���->д����
            using (StreamWriter sw = new StreamWriter(fs))
            {
                //���к��еı��������д����ļ�
                for (int row = 0; row < table.Rows.Count; row++)
                { 
                    DataRow dataRow = table.Rows[row];

                    string str = "";
                    for (int col = 0; col < table.Columns.Count; col++)
                    { 
                        string var = dataRow[col].ToString();
                        str = str + var + "\t";
                    }

                    //д��
                    sw.Write(str);

                    //�������һ������
                    if (row != table.Rows.Count-1)
                    {
                        sw.WriteLine();
                    }
                }
            }
        }
    }
}
