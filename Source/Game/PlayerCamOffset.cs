using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    /// <summary>
    /// PlayerCamOffset Script.
    /// </summary>
    public class PlayerCamOffset : Script
    {
        /// <inheritdoc/>
        [Limit(0, 1),Tooltip("Lerp Speed")]
        public float LerpSpeed { get; set; } = 20.0f;
        Actor Player;
        public override void OnStart()
        {
            // Here you can add code that needs to be called when script is created, just before the first game update
            Player = Actor.Scene.FindActor("PlayerPhysics");
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
        public override void OnUpdate()
        {
            Actor.Position = Vector3.Lerp(Actor.Position, Player.Position, LerpSpeed);
        }
    }
}
