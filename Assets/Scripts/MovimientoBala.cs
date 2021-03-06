﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoBala : MonoBehaviour {
  public int velocidad = 80;
  public int danio = 100;
  public float ttl = 3;
  public int fragments = 0;
  float fragmentArc = 20f;
  void Update() {
    gameObject.transform.Translate(0f, 0f, velocidad * Time.deltaTime);
    Invoke("destroyGameObject", ttl);
  }
  void OnTriggerEnter(Collider other) {
    if (other.gameObject.tag != "Player"
    && other.gameObject.tag != "Bala") {
      destroyGameObject();
    }
  }
  void destroyGameObject() {
    Destroy(gameObject);
    if (fragments > 0) {
      newBullet(-fragmentArc);
      newBullet(fragmentArc);
    }
  }
  void newBullet(float arc = 0) {
        transform.position = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z);
    var bullet = Instantiate(gameObject, transform.position, transform.rotation);
    bullet.GetComponent<MovimientoBala>().fragments = fragments - 1;
    bullet.transform.Rotate(0, arc, 0);
  }
}
