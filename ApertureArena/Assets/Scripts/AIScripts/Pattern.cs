using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pattern : MonoBehaviour
{
    public abstract float Execute(BulletHellAI ai, Vector3 forward, float bulletSpeed, float patternSpeed, float repetitions = 1);

    protected int ShotLevel()
    {
        int[] levels = { 0, -1}; //if you want new shooting levels add them here
        int randIndex = Random.Range(0, levels.Length);
        return levels[randIndex];
    }
}
public class Nova : Pattern
{
    //This pattern loops once so pS doesn't effect it
    public override float Execute(BulletHellAI ai, Vector3 forward,  float bS, float pS, float reps = 1) {
      int level = ShotLevel(); // randomly determine the shot level 
      for (int i = 0; i < 16; i++) {

        Vector3 direction = Quaternion.AngleAxis(22.5f * i, Vector3.up) * forward;

        ai.fire(direction, bS, level);
      }

      //this returns just the pattern speed because it is instantaneous
      return pS * 1;
    }
}
public class Spray : Pattern
{
    public override float Execute(BulletHellAI ai, Vector3 forward, float bulletSpeed, float patternSpeed, float reps = 1)
    {
        int level = ShotLevel();
        for (int i = 0; i < 10; i++)
        {
            Vector3 direction = Quaternion.AngleAxis(6f * (i-5), Vector3.up) * forward;
            ai.fire(direction, bulletSpeed, level);

        }
        return patternSpeed * 1;
    }
}
