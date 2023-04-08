using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemi4_movement : MonoBehaviour
{
    public class movement4 : MonoBehaviour
    {
        private bool isMovingRing = true;
        public float basespeed = 0.4f;


        // Start is called before the first frame update
        void Start()
        {
        }
        private void Move()
        {
            float movement = basespeed * Time.fixedDeltaTime * Time.timeScale;
            transform.Translate(isMovingRing ? movement : -movement, 0, 0);
        }
    }
    public  void Direction()
    {
     
        transform.Translate(0, 0.4f, 0);
    }
        // Update is called once per frame
    }

