using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashDestroyOther : MonoBehaviour
{
    Vector3 resetPoint;

    private void OnCollisionEnter(Collision collision)
    {
        resetPoint = new Vector3(0, 0, 0);
        Debug.Log($"게임오브젝트의 레이어는 {collision.gameObject.layer}");

        if (collision.gameObject.layer == 10 && collision.gameObject.layer == 23)
        {
            Destroy(collision.gameObject);
        }
        else
        {
            collision.gameObject.transform.position = resetPoint;
        }
    }
}
