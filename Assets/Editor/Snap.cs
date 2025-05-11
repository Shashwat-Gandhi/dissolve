using UnityEngine;
using UnityEditor;
public class Snap : MonoBehaviour
{
    const float offset_dist = 0.01f;

    [MenuItem("Custom/Snap_BC %g")]
    public static void SnapBottomCenter()
    {
        foreach(var transform in Selection.transforms)
        {
           
            var hits = Physics2D.RaycastAll(transform.position + Vector3.up*0.2f, Vector3.down, 2);
            foreach(var hit in hits)
            {
                if(hit.collider.gameObject == transform.gameObject)
                    continue;

                Vector2 offset = new Vector3(0, offset_dist);
                transform.position = hit.point + offset;
                break;
            }
        }
    }

    [MenuItem("Custom/Snap_BR %h")]
    public static void SnapBottomRight()
    {
        foreach (var transform in Selection.transforms)
        {
           
            Vector3 bottom_right = transform.position + transform.right.normalized * transform.localScale.x / 2 ;
            
            var hits = Physics2D.RaycastAll(bottom_right, Vector3.down, 2);
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject == transform.gameObject)
                    continue;
                
                Vector2 offset = new Vector3(0, offset_dist);
                bottom_right -= transform.position;
                Vector2 br = new Vector2(bottom_right.x, bottom_right.y);
                transform.position = hit.point  - br + offset;
                break;
            }
        }
    }

    [MenuItem("Custom/Snap_BL %j")]
    public static void SnapBottomLeft()
    {
        foreach (var transform in Selection.transforms)
        {
         
            Vector3 bottom_left = transform.position - transform.right.normalized * transform.localScale.x / 2;

            var hits = Physics2D.RaycastAll(bottom_left, Vector3.down, 2);
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject == transform.gameObject)
                    continue;

                Vector2 offset = new Vector3(0, offset_dist);
                bottom_left -= transform.position;
                Vector2 br = new Vector2(bottom_left.x, bottom_left.y);
                transform.position = hit.point - br + offset;
                break;
            }
        }
    }
}
