using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform[] targets; // Karakterlerin Transform'ları
    private int currentIndex = 0;
    public float smoothSpeed = 5f;

    private void Update()
    {
        if (targets.Length == 0) return;

        Vector3 desiredPosition = new Vector3(targets[currentIndex].position.x, targets[currentIndex].position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Karakter geçiş tuşları (← → veya scroll)
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            currentIndex = Mathf.Min(currentIndex + 1, targets.Length - 1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            currentIndex = Mathf.Max(currentIndex - 1, 0);
        }
    }

    public int GetActiveCharacterIndex()
    {
        return currentIndex;
    }
}
