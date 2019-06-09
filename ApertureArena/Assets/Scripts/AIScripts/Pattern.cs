using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Base class written by Cole Severson.
*/

//an abstract class that causes the ranged ai to fire patterns. Implementation of Subclass Sandbox
public abstract class Pattern : MonoBehaviour
{
    public abstract float Execute(BulletHellAI ai, Vector3 forward, float bulletSpeed, float patternSpeed, float repetitions = 1);

    protected float ShotLevel()
    {
        float[] levels = { 0.0f, -1.0f}; //if you want new shooting levels add them here
        int randIndex = Random.Range(0, levels.Length);
        return levels[randIndex];
    }
}
public class Nova : Pattern
{
    //This pattern loops once so pS doesn't effect it
    public override float Execute(BulletHellAI ai, Vector3 forward,  float bS, float pS, float reps = 1) {
      float level = ShotLevel(); // randomly determine the shot level
      for (int i = 0; i < 16; i++) {

        Vector3 direction = Quaternion.AngleAxis(22.5f * i, Vector3.up) * forward;

        ai.fire(direction, bS, level);
      }

      //this returns just the pattern speed because it is instantaneous
      return pS * 1;
    }
}
public class Cone : Pattern
{
    public override float Execute(BulletHellAI ai, Vector3 forward, float bulletSpeed, float patternSpeed, float reps = 1)
    {
        float level = ShotLevel();
        for (int i = 0; i < 10; i++)
        {
            Vector3 direction = Quaternion.AngleAxis(6f * (i-5), Vector3.up) * forward;
            ai.fire(direction, bulletSpeed, level);

        }
        return patternSpeed * 1;
    }
}

public class Wall : Pattern
{
    public override float Execute(BulletHellAI ai, Vector3 forward, float bulletSpeed, float patternSpeed, float repetitions = 1)
    {
        float level = ShotLevel();
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                Vector3 direction = Quaternion.AngleAxis(3.0f * (j - 5), Vector3.up) * forward;
                ai.fire(direction, bulletSpeed, (i*level)/2.5f);
            }
        }

        return patternSpeed * 1;
    }
}

public class Spray : Pattern
{
    public override float Execute(BulletHellAI ai, Vector3 forward, float bulletSpeed, float patternSpeed, float repetitions = 1)
    {
        float alternator;
        for (int i = 0; i < 20; i++)
        {
            alternator = 1f;
            Vector3 direction = Quaternion.AngleAxis(3.0f * (i - 5), Vector3.up) * forward;
            alternator *= Random.Range(-1, 0);
            ai.fire(direction, bulletSpeed,  alternator);
        }

        return patternSpeed * 1;
    }
}

public class Wave : Pattern
{
    public override float Execute(BulletHellAI ai, Vector3 forward, float bulletSpeed, float patternSpeed, float repetitions = 1)
    {
        float level = ShotLevel();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                level *= -1.0f;
                Vector3 direction = Quaternion.AngleAxis(12.0f * (i - 5), Vector3.up) * forward;
                ai.fire(direction, bulletSpeed, (j * level) / 5f);
            }
        }

        return patternSpeed;
    }
}
