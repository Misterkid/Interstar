using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
public class DataLoader  
{
    //private StreamReader waveReader;


    public DataLoader()
    {


    }
    public static void LoadConfig()
    {
        StreamReader configReader;
        configReader = new StreamReader(Application.dataPath + "/../Config/Config.txt");
        string line = "";

        while ((line = configReader.ReadLine()) != null)
        {
            if (!line.Contains("//"))
            {
                line = line.Replace(" ",string.Empty);
                if(line.Contains("timeBetweenSpawns="))
                {
                    line = line.Replace("timeBetweenSpawns=", string.Empty);
                    Debug.Log(line);
                    ConfigData.gameConfig.timeBetweenSpawns = float.Parse(line);
                }
                else if (line.Contains("timeBetweenWaves="))
                {
                    line = line.Replace("timeBetweenWaves=", string.Empty);
                    ConfigData.gameConfig.timeBetweenWaves = float.Parse(line);
                }
            }
        }
    }
    public static void LoadMonsters()
    {
        ConfigData.monsterDatas = new List<ConfigData.ConfMonster>();
        StreamReader monsterReader;
        monsterReader = new StreamReader(Application.dataPath + "/../Config/Monster.txt");

        string line = "";

        while ((line = monsterReader.ReadLine()) != null)
        {
            ConfigData.ConfMonster monster = new ConfigData.ConfMonster();
            if (!line.Contains("//"))
            {
                string[] split = line.Split(':');
                monster.monsterId = int.Parse(split[0]);
                string[] monsterData = split[1].Split(',');
                monster.speed = float.Parse(monsterData[0]);
                monster.damage = int.Parse(monsterData[1]);
                monster.minSueezePower = int.Parse(monsterData[2]);
                monster.maxSqueezePower = int.Parse(monsterData[3]);

                ConfigData.monsterDatas.Add(monster);
            }

        }
    }
    public static void LoadWaves()
    {
        ConfigData.waveDatas = new List<Wave>();

        StreamReader waveReader = new StreamReader(Application.dataPath + "/../Config/Waves.txt");
        string line = "";
        while ((line = waveReader.ReadLine()) != null)
        {
            Wave wave = new Wave();
            if (!line.Contains("//"))
            {
                string[] split = line.Split(':');
                string[] waveDatas = split[1].Split(',');
                int[] convert = new int[waveDatas.Length];
                for (int i = 0; i < convert.Length; i++)
                {
                    convert[i] = int.Parse(waveDatas[i]);
                }
                wave.monsters = convert;
                ConfigData.waveDatas.Add(wave);
            }
        }
    }
}
