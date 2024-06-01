using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Excel;
using System.Data;

//编辑器脚本
public static class MyEditor
{
    [MenuItem("自制工具/Excel转Txt")]
    public static void ExportExcel2Txt() 
    {
        //_Excel文件夹路径
        string assetPath = Application.dataPath + "/_Excel";
        //获得Excel文件
        string[] files = Directory.GetFiles(assetPath, "*.xlsx");
        for (int i = 0; i < files.Length; i++) 
        {
            //替换文件名中的反斜杠
            files[i] = files[i].Replace('\\', '/');
            //文件流读取
            using(FileStream fs = File.Open(files[i], FileMode.Open, FileAccess.Read))
            { 
                //文件流->Excel对象
                var excelDateReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                //获取Excel数据
                DataSet dataSet = excelDateReader.AsDataSet();
                //读取表0
                DataTable table = dataSet.Tables[0];
                //将表中内容写入到Txt
                readTable2Txt(files[i], table);
            }
        }

        //刷新
        AssetDatabase.Refresh();
    }

    private static void readTable2Txt(string filePath, DataTable table)
    { 
        //获取不带有后缀的文件名用于写回Txt
        string fileName = Path.GetFileNameWithoutExtension(filePath);
        string path = Application.dataPath + "/Resources/Data/" + fileName + ".txt";

        //文件流创建Txt
        using(FileStream fs = new FileStream(path, FileMode.Create))
        {
            //文件流->写入流
            using (StreamWriter sw = new StreamWriter(fs))
            {
                //先行后列的遍历表并按行存入文件
                for (int row = 0; row < table.Rows.Count; row++)
                { 
                    DataRow dataRow = table.Rows[row];

                    string str = "";
                    for (int col = 0; col < table.Columns.Count; col++)
                    { 
                        string var = dataRow[col].ToString();
                        str = str + var + "\t";
                    }

                    //写入
                    sw.Write(str);

                    //若非最后一行则换行
                    if (row != table.Rows.Count-1)
                    {
                        sw.WriteLine();
                    }
                }
            }
        }
    }
}
