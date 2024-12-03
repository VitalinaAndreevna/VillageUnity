using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSwitch : MonoBehaviour
{
    public Transform Switch1;
    public Transform Switch21;
    public Transform Switch22;
    public Transform teleport1;
    public Transform teleport21;
    public Transform teleport22;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Switch1.position.z < -6)
        {
            Switch1.position = teleport1.position;
        }

        if ((Switch21.position.z > -5) || (Switch21.position.x > -6))
        {
            Switch21.position = teleport21.position;
        }

        if ((Switch22.position.z > -5) || (Switch22.position.x > -6))
        {
            Switch22.position = teleport22.position;
        }
    }
}
