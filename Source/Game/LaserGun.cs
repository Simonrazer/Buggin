using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    /// <summary>
    /// LaserGun Script.
    /// </summary>
    public class LaserGun : Script
    {
        /// <inheritdoc/>
        public Actor spawnPoint;
        public Actor shaft;
        public Prefab bullet;
        Actor playerRef;

        public float shootingInterval = 5.0f;
        public float warnTime = 2;
        public override void OnStart()
        {
            playerRef = Scene.GetChild("PlayerPhysics");
        }
        
        /// <inheritdoc/>
        public override void OnEnable()
        {
            // Here you can add code that needs to be called when script is enabled (eg. register for events)
        }

        /// <inheritdoc/>
        public override void OnDisable()
        {
            // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
        }

        /// <inheritdoc/>
        float lastShotTime = 0;
        Float3 rot = new Float3();
        Actor spawned;
        public override void OnUpdate()
        {
            // Here you can add code that needs to be called every frame
            if(Time.GameTime - lastShotTime < warnTime){
                shaft.LookAt(playerRef.Position);
                rot = shaft.LocalEulerAngles;
                rot.X = 0; rot.Z = 0;
                var qua = Quaternion.Euler(rot);
                shaft.LocalOrientation = qua;
                spawned.LocalOrientation = qua;
                var spawnPos = spawnPoint.Position;
                spawnPos.Y = 50;
                spawned.Position = spawnPos;
            }
            

            if(Time.GameTime - lastShotTime > shootingInterval){
                lastShotTime = Time.GameTime;
                var spawnPos = spawnPoint.Position;
                spawnPos.Y = 50;
                spawned = PrefabManager.SpawnPrefab(bullet, spawnPos);               
                spawned.LocalOrientation = Quaternion.Euler(rot);
                var llogic = spawned.GetScript<LaserBeam>();
                llogic.setWarntime(warnTime);
                Destroy(spawned, 3);
            }
            
        }
    }
}
