using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    /// <summary>
    /// ControllerBoing Script.
    /// </summary>
    
    public class ControllerBoing : Script
    {
        [Tooltip("Dampening when no input")]
        public float SlowBreak { get; set; } = 20.0f;

        [Tooltip("Dampening when no input")]
        public float SpeedBreak { get; set; } = 20.0f;
        /// <inheritdoc/>
        RigidBody rb;
        public override void OnStart()
        {
            // Here you can add code that needs to be called when script is created, just before the first game update
            rb = Actor.As<RigidBody>();
        }

        /// <inheritdoc/>
        public override void OnDisable()
        {
            // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
        }

        /// <inheritdoc/>
        public override void OnUpdate()
        {
            Vector3 wantedDir = new Vector3(Input.GetAxis("Horizontal")*5000,0, Input.GetAxis("Vertical")*5000);
            if(!wantedDir.IsZero){
                rb.LinearDamping = SpeedBreak;
                rb.AddForce(wantedDir);
            }
            else{
                rb.LinearDamping = SlowBreak;
            }
        }
    }
}
