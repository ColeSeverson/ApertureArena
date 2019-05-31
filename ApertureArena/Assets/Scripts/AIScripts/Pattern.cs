using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pattern : MonoBehaviour
{
    public abstract float Execute(BulletHellAI ai, Vector3 forward, float bulletSpeed, float patternSpeed, float repetitions = 1);
}
public class Nova : Pattern
{
    //This pattern loops once so pS doesn't effect it
    public override float Execute(BulletHellAI ai, Vector3 forward,  float bS, float pS, float reps = 1) {
      Quaternion rotation;
      for (int i = 0; i < 16; i++) {

        Vector3 direction = Quaternion.AngleAxis(22.5f * i, Vector3.up) * forward;

        ai.fire(direction, bS);
      }

      //this returns just the pattern speed because it is instantaneous
      return pS * 1;
    }
}
public class Spray : Pattern
{
    public override float Execute(BulletHellAI ai, Vector3 forward, float bulletSpeed, float patternSpeed, float reps = 1)
    {
        Quaternion rotation;
        for (int i= 0; i<10; i++)
        {
            Vector3 direction = Quaternion.AngleAxis(6f * (i-5), Vector3.up) * forward;
            ai.fire(direction, bulletSpeed);
        }
        return patternSpeed * 1;
    }
}
