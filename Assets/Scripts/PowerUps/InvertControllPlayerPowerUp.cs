using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertControllPlayerPowerUp : PowerUps
{
    [SerializeField] float _duracion =10.0f;
    public override void ApplyEffect(PlayerController player) {
        player.ApplyInverControls(_duracion);
    }

}
