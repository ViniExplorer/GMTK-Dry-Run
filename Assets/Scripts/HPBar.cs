using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    HP healthData;
    bool alreadyEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        healthData = GetComponentInParent<HP>();
    }

    // Update is called once per frame
    void Update()
    {
        if(healthData.hp < healthData.maxHP)
        {
            if (alreadyEnabled == false)
            {
                alreadyEnabled = true;
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(true);
            }
            transform.GetChild(0).localScale = new Vector3(healthData.hp / healthData.maxHP, 1f, 1f);
        }
    }
}
