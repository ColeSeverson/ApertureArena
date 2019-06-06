using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
  Written by Cole Severson
  This is the script for firing the pistol. Everything is original except for the enemyhealth section
*/
namespace CharacterController {
  using EnemyInformation;
  public class Gun : Weapon
  {
    bool cooldown;
    public Camera mainCamera;
    public AudioClip[] zaps;

    private AudioSource source;
    private Ray cameraRay;
    private Ray gunRay;
    private LineRenderer gunLine;






    //Start initializes the LineRenderer
    void Start(){
      gunLine = GetComponent<LineRenderer>();
      gunLine.enabled = false;
      source = GetComponent<AudioSource>();
    }
    void Zap(){
      source.clip = zaps[Random.Range(0, zaps.Length)];
      source.Play();
    }
    //GunLineTimer removes the line
    IEnumerator gunLineTimer(){
      yield return new WaitForSeconds(.2f);
      gunLine.enabled = false;
    }
    IEnumerator fireTime(){
      yield return new WaitForSeconds(.4f);
      cooldown = false;
    }

    //Fire is the main code. It waits to wait for the animation. THen it traces a ray from the camera to calculate the angle that the gun should shoot
    IEnumerator fire(){
      yield return new WaitForSeconds(.3f);
      Zap();
      cameraRay.direction = mainCamera.transform.forward;
      cameraRay.origin = mainCamera.transform.position;
      RaycastHit hit;

      //if the camera ray hits do the gun firing logic and enemy hit logic, otherwise fire a ray into infinity
      if(Physics.Raycast (cameraRay, out hit, Mathf.Infinity)){//the camera hits something
          gunRay.origin = transform.position;
          gunRay.direction = hit.point - transform.position;
          RaycastHit gunHit;
          if(Physics.Raycast (gunRay, out gunHit, hit.distance * 2)) {
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            if(enemyHealth != null) {
              enemyHealth.TakeDamage (1, hit.point);
            }
            gunLine.SetPosition (1, gunHit.point);
          } else {
            gunLine.SetPosition (1, gunRay.origin + gunRay.direction * Mathf.Infinity);
          }
      } else {
        gunLine.SetPosition (1, (cameraRay.direction * 1000f - transform.position));
      }
      gunLine.SetPosition(0, transform.position);
      gunLine.enabled = true;
      //wait like .2f and remove gunLine
      StartCoroutine(gunLineTimer());
      StartCoroutine(fireTime());
    }

    //Execute only if the gun is cooldown'ed
    public override void Execute(){
      Debug.Log("Gun Weapon");
      if(cooldown == false){
        cooldown = true;
        StartCoroutine(fire());
      }
    }
  }
}
