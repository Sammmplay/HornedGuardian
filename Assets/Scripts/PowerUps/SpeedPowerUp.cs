using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : PowerUps
{
    public float _duration;
    float _effectEndTime;
    [SerializeField] float _velodityAd;
    float _originalEfect;
    public override void ApplyEffect(PlayerController player) {
        player.ApplyspeedSpeed(_velodityAd, _duration);
    }

}
