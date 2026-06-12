using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionWaypoint : MonoBehaviour
{
    public Image indicator;
    public Transform target;
    public TextMeshProUGUI distance;

    // Update is called once per frame
    void Update()
    {
        float minX = indicator.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;
        float minY = indicator.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector2 pos = Camera.main.WorldToScreenPoint(target.position);

        if (Vector3.Dot((target.position - transform.position), transform.forward) < 0)
        {
            if (pos.x < Screen.width / 2)
            {
                pos.x = maxX;
            }
            else
            {
                pos.x = minX;
            }
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        indicator.transform.position = pos;
        distance.text = ((int)Vector3.Distance(target.position, transform.position)).ToString() + "m"; 
    }
}
