/*
 * Author : Hosik Choo
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTree : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.CompareTag("Weapon"))
      {
         Destroy(gameObject);
      }
   }
}
