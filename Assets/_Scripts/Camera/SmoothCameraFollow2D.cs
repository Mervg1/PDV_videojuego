﻿using UnityEngine;
using System.Collections;

//Written by Scott Kovacs via UnityAnswers.com; Oct 5th 2010
//2DCameraFollow - Platformer Script

public class SmoothCameraFollow2D : MonoBehaviour {

	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;
	
	// Update is called once per frame
	void Update ()
	{
		if (target) {
            // Obtiene la posición que tiene el objeto que estamos siguiendo
            // según la vista de la cámara
			Vector3 point = GetComponent<Camera>().WorldToViewportPoint(new Vector3(target.position.x, (target.position.y + 2.2f), target.position.z));
			// la diferencia que existe entre la posición del objeto y la posición que tiene la cámara
			// respecto al objeto
			Vector3 delta = new Vector3(target.position.x, (target.position.y + 2.2f), target.position.z) - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
			// establecemos el destino es decir, la posición que tengo como cámara más la posición
			// delta obtenida anteriormente
			Vector3 destination = transform.position + delta;
			// Movemos la cámara utilizando el método de Smooth Damp
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
	}
}
