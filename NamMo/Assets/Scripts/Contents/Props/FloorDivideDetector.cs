using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDivideDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<FloorDivideTrigger>() == null) return;
        collision.gameObject.GetComponent<FloorDivideTrigger>().SetCameraOffset();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<FloorDivideTrigger>() == null) return;
        if (transform.position.y > collision.transform.position.y) return;
        collision.gameObject.GetComponent<FloorDivideTrigger>().SetCameraOffsetDownFloor();
    }
}
