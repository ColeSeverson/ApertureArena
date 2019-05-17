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
    public Camera mainCamera;
    private Ray cameraRay;
    private Ray gunRay;
    private LineRenderer gunLine;
    void Start(){
      gunLine = GetComponent<LineRenderer>();
      gunLine.enabled = false;
    }
    IEnumerator gunLineTimer(){
      yield return new WaitForSeconds(.2f);
      gunLine.enabled = false;
    }
    public override void Execute(){
      Debug.Log("Gun Weapon");

      cameraRay.origin = mainCamera.transform.position;
      cameraRay.direction = mainCamera.transform.forward;
      RaycastHit hit;

      //Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * 100f, Color.green, 1000f, false);
      if(Physics.Raycast (cameraRay, out hit, Mathf.Infinity)){//the camera hits something
          //Debug.Log("Hit");
          //Debug.DrawRay(transform.position, (hit.point - transform.position) * hit.distance, Color.yellow, 1000f, false);
          gunRay.origin = transform.position;
          gunRay.direction = hit.point - transform.position;
          RaycastHit gunHit;
          if(Physics.Raycast (gunRay, out gunHit, hit.distance * 2)) {
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            if(enemyHealth != null) {
              enemyHealth.TakeDamage (100, hit.point);
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


    }
  }
}
