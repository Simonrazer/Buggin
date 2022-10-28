using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    /// <summary>
    /// ProjectileShooter Script.
    /// </summary>
    public class ProjectileShooter : Script
    {
        /// <inheritdoc/>
        public Actor spawnPoint;
        public Actor shaft;
        public Prefab bullet;
        Actor playerRef;

        public float shootingInterval = 2.0f;
        public float bulletSpeed = 1000;
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
        public override void OnUpdate()
        {
            // Here you can add code that needs to be called every frame
            shaft.LookAt(playerRef.Position);
            var rot = shaft.LocalEulerAngles;
            rot.X = 0; rot.Z = 0;
            shaft.LocalOrientation = Quaternion.Euler(rot);

            if(Time.GameTime - lastShotTime> shootingInterval){
                lastShotTime = Time.GameTime;
                var spawnPos = spawnPoint.Position;
                spawnPos.Y = 50;
                Actor spawned = PrefabManager.SpawnPrefab(bullet, spawnPos);
                var a = spawned.GetChild("RigidBody").As<RigidBody>();
                a.LinearVelocity = (Vector3.Normalize(shaft.Position - playerRef.Position) * -bulletSpeed);
                Destroy(spawned, 1);
            }
            
        }
    }
}
