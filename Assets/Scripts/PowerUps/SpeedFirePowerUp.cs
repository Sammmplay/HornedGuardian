using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedFirePowerUp : PowerUps
{
    public float _duration;
    [SerializeField] float _velodityAd;
    public override void ApplyEffect(PlayerController player) {
        player.ApplySpeedFire(_velodityAd, _duration);
    }
}
