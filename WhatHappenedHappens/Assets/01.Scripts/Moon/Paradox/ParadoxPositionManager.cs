using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParadoxPositionManager
{
    public Transform B1_Pos;
    public Transform B2_Pos;
    public Transform A_Pos;

    private Vector3 B1_Start_Pos;
    private Vector3 B2_Start_Pos;
    private Vector3 A_Start_Pos;
    private Vector3 playerStartPos;

    public void Save()
    {
        if (B1_Pos != null) B1_Start_Pos = B1_Pos.position;
        if (B2_Pos != null) B2_Start_Pos = B2_Pos.position;
        if (A_Pos != null) A_Start_Pos = A_Pos.position;
    }

    public void ResetPlayer(GameObject player)
    {
        playerStartPos = player.transform.position;
        player.transform.position = playerStartPos;
    }

    public void ResetAll()
    {
        if (B1_Pos != null) B1_Pos.position = B1_Start_Pos;
        if (B2_Pos != null) B2_Pos.position = B2_Start_Pos;
        if (A_Pos != null) A_Pos.position = A_Start_Pos;
    }
}
