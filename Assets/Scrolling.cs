using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public float scrollSpeed = 0.2F;    // the speed at which the texture scrolls/wraps
    private float offset;               // used to determine how much texture wrap occurs
    public bool isHorizontalScroll;     // determines if the parallax should be Horizontal
    public bool leftToRight = true;     // determines if the scroll direction is left-to-right

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        offset = Mathf.Repeat(Time.time * scrollSpeed, 1);

        //-- Set the offset of the texture to move in the desired direction & axis ------//

        // HORIZONTAL Axis & Left-To-Right Direction
        if (isHorizontalScroll && leftToRight)
        {
            gameObject.transform.position = new Vector2(-offset, 0f);
        }
        // HORIZONTAL Axis & Right-To-Left Direction
        else if (isHorizontalScroll && !leftToRight)
        {
            gameObject.transform.position = new Vector2(offset, 0f);
        }
    }
}
