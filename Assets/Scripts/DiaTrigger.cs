using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaTrigger : MonoBehaviour
{
    public Dialouge dia;

    public void TriggerDia()
    {
        DiaManager.Instance.StartDia(dia);
    }
}
