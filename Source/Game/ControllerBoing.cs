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
            Vector3 wantedDir = new Vector3(Input.GetAxis("Horizontal")*500, Input.GetAxis("Vertical")*500, 0);
            rb.LinearVelocity = wantedDir;
        }
    }
}
