﻿using System.Collections.Generic;
using UnityEngine;

namespace WarChess
{
    /// <summary>
    /// Represents a data table file in csv format
    /// </summary>
    public class ConfigData
    {
        /// <summary>
        /// Dictionary that contains every data table, key is the id of the inner dictionary, inner value is the data of the row
        /// </summary>
        protected Dictionary<int, Dictionary<string, string>> datas;

        /// <summary>
        /// name of the config file
        /// </summary>
        public string fileName;
        
        public ConfigData(string fileName)
        {
            this.fileName = fileName;
            datas = new Dictionary<int, Dictionary<string, string>>();
        }

        /// <summary>
        /// Load the text file
        /// </summary>
        /// <returns></returns>
        public TextAsset LoadFile()
        {
            return Resources.Load<TextAsset>($"Data/{fileName}");
        }

        /// <summary>
        /// Read and save data from the text
        /// </summary>
        /// <param name="txt"></param>
        public void Load(string txt)
        {
            string[] dataArr = txt.Split('\n');
            string[] titleArr = dataArr[0].Trim().Split(','); // retrieve the first row being the inner keys
            
            // start reading the content from the third row
            for (int i = 2; i < dataArr.Length; i++)
            {
                string[] contentArr = dataArr[i].Trim().Split(',');
                Dictionary<string, string> data = new Dictionary<string, string>();
                for (int j = 0; j < titleArr.Length; j++)
                {
                    data.Add(titleArr[j], contentArr[j]);
                }
                datas.Add(int.Parse(data["Id"]), data);
            }
        }
        
        public Dictionary<string, string> GetDataById(int id)
        {
            if (datas.ContainsKey(id))
            {
                return datas[id];
            }
            else
            {
                Debug.LogError($"Data {fileName} does not contain id {id}");
                return null;
            }
        }

        public Dictionary<int, Dictionary<string, string>> GetLines()
        {
            return datas;
        }
    }
}