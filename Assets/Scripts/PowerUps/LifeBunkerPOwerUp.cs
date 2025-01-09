using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBunkerPOwerUp : PowerUps
{
    public override void ApplyEffect(PlayerController player) {
        for (int i = 0; i < GameManager.Instance._bunkers.Count; i++) {
            GameManager.Instance._bunkers[i].RestoreToLife();
        }
    }
}
