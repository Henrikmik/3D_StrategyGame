using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shake : MonoBehaviour
{

    public IEnumerator ShakeObject(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        //Debug.Log(originalPos);

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(- 40f, 40f) * magnitude;
            float y = Random.Range(- 40f, 40f) * magnitude;
            //Debug.Log(x + " " + y);

            transform.GetComponent<TMP_Text>().color = Color.red;
            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.GetComponent<TMP_Text>().color = Color.white;
        transform.localPosition = originalPos;
    }

}
