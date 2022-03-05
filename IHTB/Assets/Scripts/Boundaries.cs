using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, this.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 checkPosition = this.transform.position;
        checkPosition.x = Mathf.Clamp(checkPosition.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        checkPosition.y = Mathf.Clamp(checkPosition.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        this.transform.position = checkPosition;
    }
}
