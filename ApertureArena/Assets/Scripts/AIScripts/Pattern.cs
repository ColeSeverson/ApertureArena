using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pattern : MonoBehaviour
{
    public abstract void Execute(BulletHellAI ai, Vector3 forward, float bulletSpeed, float patternSpeed);
}
public class Nova : Pattern
{
    //This pattern loops once so pS doesn't effect it
    public override void Execute(BulletHellAI ai, Vector3 forward,  float bS, float pS) {
      Quaternion rotation;
      for (int i = 0; i < 16; i++) {
      
        Vector3 direction = Quaternion.AngleAxis(22.5f * i, Vector3.up) * forward;

        ai.fire(direction, bS);
      }
    }
}
