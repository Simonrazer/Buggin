using System;
using System.Collections.Generic;
using FlaxEngine;

namespace Game
{
    /// <summary>
    /// LaserBeam Script.
    /// </summary>
    public class LaserBeam : Script
    {
        /// <inheritdoc/>

        public Actor noHurt;
        public Actor bigHurt;
        float warnTime;
        float existingSince = 0;
        public void setWarntime(float w){
            warnTime = w;
        }
        public override void OnStart()
        {
            // Here you can add code that needs to be called when script is created, just before the first game update
            existingSince = Time.GameTime;
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

            // Here you can add code that needs to be called every frame
            if(Time.GameTime - existingSince > warnTime){
                bigHurt.IsActive = true;
                noHurt.IsActive = false;
                Destroy(this);
            }
        }
    }
}
