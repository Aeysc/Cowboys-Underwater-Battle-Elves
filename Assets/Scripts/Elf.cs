﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elf
    :
    MonoBehaviour
{
    void Start()
    {
        part = Utility.GetPrefabHolder().particle;
    }
    void OnCollisionEnter( Collision coll )
    {
        if( coll.gameObject.tag == "Bullet" )
        {
            // --hp;
            hp -= coll.gameObject.GetComponent<Bullet>().damage;

            Destroy( coll.gameObject );

            if( hp < 1.0f && !deadAlready )
            {
                deadAlready = true;

                CreateParticles( Random.Range( 7,11 ) );

                Utility.FindInScene( "Cowboy" )
                    .GetComponent<CowboyScore>()
                    .AddScore( scoreAdd );

                Destroy( gameObject );
            }
        }
    }
    void CreateParticles( int amount )
    {
        for( int i = 0; i < amount; ++i )
        {
            var temp = Instantiate( part );
            temp.transform.position = transform.position;

            var scr = temp.GetComponent<Particle>();
            scr.SetColor( myPartCol );
            scr.JumpUp();
        }
    }
    // 
    float hp = 7.0f;
    GameObject part;
    Color myPartCol = new Color( 90.0f / 255.0f,
        197.0f / 255.0f,79.0f / 255.0f );
    const int scoreAdd = 1;
    bool deadAlready = false;
}
