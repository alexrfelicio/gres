using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerB : MonoBehaviour
{
    UIManagerB uiMan;
    [SerializeField]
    private int mBattery = 20;
    private bool mDead = false;

    private void Awake()
    {
        uiMan = GameObject.Find("UIManager").GetComponent<UIManagerB>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            uiMan.Perdeu();
            Debug.Log("LOSE!");
            mDead = true;
        }
    }

    public bool AllowedToMove()
    {
        Debug.Log("Battery remaining: " + mBattery);
        bool allow = (mBattery > 0);
        mBattery--;
        return allow && !mDead;
    }
}
