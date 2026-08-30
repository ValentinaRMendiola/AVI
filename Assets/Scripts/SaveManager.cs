using System.IO;
using UnityEngine;
using Convai.Scripts.Runtime.Addons;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private string savePath;

    [Header("References")]
    private Transform npc;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        savePath = Application.persistentDataPath + "/save.json";
    }

    public bool SaveExists()
    {
        return File.Exists(savePath);
    }

    public void SaveGame(ConvaiPlayerMovement player)
    {
        SaveData data = new SaveData();

        data.sceneName =
            UnityEngine.SceneManagement.SceneManager
            .GetActiveScene().name;

        // PLAYER

        Vector3 pos = player.transform.position;

        data.playerPosX = pos.x;
        data.playerPosY = pos.y+1f;
        data.playerPosZ = pos.z;

        data.playerRotY =
            player.transform.eulerAngles.y;

        // NPC
        npc = GameObject.FindWithTag("Character")?.transform;

        if (npc != null)
        {
            Vector3 npcPos = npc.position;

            data.npcPosX = npcPos.x;
            data.npcPosY = npcPos.y+1f;
            data.npcPosZ = npcPos.z;

            data.npcRotY = npc.eulerAngles.y;
        }

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(savePath, json);
    }

    public SaveData LoadGame()
    {
        if (!SaveExists())
            return null;

        string json = File.ReadAllText(savePath);

        return JsonUtility.FromJson<SaveData>(json);
    }
}