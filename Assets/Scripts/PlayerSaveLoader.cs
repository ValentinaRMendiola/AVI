using UnityEngine;
using System.Collections;

public class PlayerSaveLoader : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return null;

        if (SaveLoader.LoadFromSave)
        {
            LoadPlayerData();

            SaveLoader.LoadFromSave = false;
        }
    }

    private void LoadPlayerData()
    {
        SaveData data =
            SaveManager.Instance.LoadGame();

        if (data == null)
            return;

        CharacterController cc =
            GetComponent<CharacterController>();

        if (cc != null)
            cc.enabled = false;

        transform.position = new Vector3(
            data.playerPosX,
            data.playerPosY,
            data.playerPosZ
        );

        transform.rotation =
            Quaternion.Euler(
                0,
                data.playerRotY,
                0
            );

        if (cc != null)
            cc.enabled = true;
    }
}