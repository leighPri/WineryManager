using UnityEngine;
using System.Collections;
//these let the class write to a file to persist data outside the instance of the program
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad {

    //allows other classes to reference this and lets it make references to itself withing creating instances
    public static SaveLoad sys;

    //saves data out to a file, can be called by other objects using GameControl.Save() because class is static and self-referential
    public void Save()
    {
        //does the actual reading/writing to file
        BinaryFormatter bf = new BinaryFormatter();
        //Creates actual file that can be written to
        //this DOES WORK on iOS and Android
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        //creates holder for data to be saved
        PlayerData data = new PlayerData();
        //copies data from GameControl using this syntax:
        //data.health = health;

        //takes the Serializable class, data, and writes it out to file
        bf.Serialize(file, data);
        Debug.Log(Application.persistentDataPath + "/playerInfo.dat");
        //stops editing playerInfo.dat
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            //does the actual reading/writing to file
            BinaryFormatter bf = new BinaryFormatter();
            //opens previously created file
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

            //reads from file and puts it in a PlayerData instance
            //the parenthesis casts it from a generic object to specifically a PlayerData object
            PlayerData data = (PlayerData)bf.Deserialize(file);
            //have the data in the data class, no longer need file
            file.Close();

            //takes information from PlayerData instance and loads them into GameControl using this syntax:
            //health = data.health;
        }
    }
}

//holder that organizes data passed to it from GameControl and Serializes it in order to save it to a file
[Serializable]
class PlayerData
{
    //add same parameters as GameControl here
}
