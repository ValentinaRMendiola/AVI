using System.Collections;
using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
    [Header("Growth Stages")]
    public GameObject seedStage;
    public GameObject stage1;
    public GameObject stage2;
    public GameObject finalStage;

    [Header("Growth Time")]
    [Min(1f)]
    public float totalGrowthTime = 30f;

    private bool growing;

    private void Start()
    {
        // Estado inicial seguro
        SetStage(seedStage);
    }

    public void StartGrowing()
    {
        if (growing)
            return;

        StartCoroutine(GrowPlant());
    }

    IEnumerator GrowPlant()
    {
        growing = true;

        float stageTime = totalGrowthTime / 3f;

        // ETAPA 1
        yield return new WaitForSeconds(stageTime);
        SetStage(stage1);

        // ETAPA 2
        yield return new WaitForSeconds(stageTime);
        SetStage(stage2);

        // ETAPA FINAL
        yield return new WaitForSeconds(stageTime);
        SetStage(finalStage);
    }

    void SetStage(GameObject activeStage)
    {
        // Desactivar todos SOLO si existen
        if (seedStage != null)
            seedStage.SetActive(false);

        if (stage1 != null)
            stage1.SetActive(false);

        if (stage2 != null)
            stage2.SetActive(false);

        if (finalStage != null)
            finalStage.SetActive(false);

        // Activar el actual
        if (activeStage != null)
            activeStage.SetActive(true);
    }
}