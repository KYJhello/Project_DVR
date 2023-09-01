using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : MonoBehaviour
{
    private Timer timer;

    private void Awake()
    {
        timer = GameObject.Find("Clock").GetComponent<Timer>();
    }

    public void TouchBell()
    {
        StartCoroutine(ObjectChengePos());
        // 사운드 내주기
    }

    IEnumerator ObjectChengePos()
    {
        yield return null;

        transform.position = new Vector3(0, 0.5f, 0);
        yield return null;

        timer.StertSell();
        yield return new WaitForSeconds(1f);
        transform.position = new Vector3(0, 0.6f, 0);
    }
}
