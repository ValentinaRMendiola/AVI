using UnityEngine;

public class NPCSaveLoader : MonoBehaviour
{
    private void Start()
    {
        SaveData data =
            SaveManager.Instance.LoadGame();

        if (data == null)
            return;

        transform.position = new Vector3(
            data.npcPosX,
            data.npcPosY,
            data.npcPosZ
        );

        transform.rotation =
            Quaternion.Euler(
                0,
                data.npcRotY,
                0
            );
    }
}