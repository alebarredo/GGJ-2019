﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  public GameObject Bala;
  AudioSource sourceAudio;
  SoundManager soundScrpt;
  float speed = 25f;
  float cooldown = 1.5f;
  float cooldownTimer = 0f;
  Vector3 cameraOffset = 30 * new Vector3(0f, 3f, -1.5f);
  float cameraSpeed = 20f;

  Animator anim;
  void Start() {
    anim = GetComponent<Animator>();
    sourceAudio = FindObjectOfType<AudioSource>().GetComponent<AudioSource>();
    soundScrpt = FindObjectOfType<SoundManager>().GetComponent<SoundManager>();
    }
  void Update() {
    var translation = new Vector3();
    var displacement = speed * Time.deltaTime;
    if (Input.GetKey(KeyCode.W)) {
      translation += displacement * Vector3.forward;
    }
    if (Input.GetKey(KeyCode.A)) {
      translation += displacement * Vector3.left;
    }
    if (Input.GetKey(KeyCode.S)) {
      translation += displacement * Vector3.back;
    }
    if (Input.GetKey(KeyCode.D)) {
      translation += displacement * Vector3.right;
    }
    transform.position += translation;
    if (cooldownTimer > 0) {
      cooldownTimer -= Time.deltaTime;
    } else {
      if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)) {
        cooldownTimer = 0.5f;
        anim.SetBool("Attack", true);
        anim.SetBool("Walk", false);
        anim.SetBool("Idle", false);
        Instantiate(Bala, transform.position, transform.rotation);
        sourceAudio.PlayOneShot(soundScrpt.clipMisil, 0.1f);
      } else {
        anim.SetBool("Attack", false);
        anim.SetBool("Walk", translation != Vector3.zero);
        anim.SetBool("Idle", translation == Vector3.zero);
      }
    }
    float distance;
    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    var plane = new Plane(Vector3.up, transform.position);
    if (plane.Raycast(ray, out distance)) {
      transform.LookAt(ray.GetPoint(distance));
    }
    Camera.main.transform.position = Vector3.MoveTowards(
      Camera.main.transform.position,
      transform.position + cameraOffset,
      cameraSpeed * Time.deltaTime
    );
    Camera.main.transform.LookAt(transform.position);
  }
}
