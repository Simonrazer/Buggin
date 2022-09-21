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
        public float SlowBreak { get; set; } = 15.0f;
        
        [Tooltip("Dampening when no input")]
        public float SpeedBreak { get; set; } = 10.0f;
        
        [Tooltip("Dash cooldown Time")]
        public float DashTime { get; set; } = 2.0f;

        [Tooltip("Slash duration")]
        public float SlashDuration { get; set; } = 2.0f;

        [Tooltip("Slash cooldown Time")]
        public float SlashTime { get; set; } = 1.0f;
        [Tooltip("Dash Multiplier")]
        public float DashMultiplier { get; set; } = 40.0f;

        /// <inheritdoc/>
        RigidBody rb;
        Actor slashActor;
        public override void OnStart()
        {
            // Here you can add code that needs to be called when script is created, just before the first game update
            rb = Actor.As<RigidBody>();
            slashActor = Actor.GetChild("SlashVisuals");
        }

        /// <inheritdoc/>
        public override void OnDisable()
        {
            // Here you can add code that needs to be called when script is disabled (eg. unregister from events)
        }

        /// <inheritdoc/>
        float lastDashTime = 0;
        float lastSlashTime = 0;
        bool isSlashing = false;
         Vector3 slashDir = Vector3.Zero;
        public override void OnUpdate()
        {
            float multp = 1.0f;
            if (Input.GetAction("Dash") && Time.GameTime - lastDashTime > DashTime){
                lastDashTime = Time.GameTime;
                multp = DashMultiplier;
            }
            Vector3 wantedDir = new Vector3(Input.GetAxis("Horizontal")*5000*multp, 0 , Input.GetAxis("Vertical")*5000*multp);
            if(!wantedDir.IsZero){
                rb.LinearDamping = SpeedBreak;
                rb.AddForce(wantedDir);
            }
            else{
                rb.LinearDamping = SlowBreak;
            }

            if (Input.GetAction("Slash") && Time.GameTime - lastSlashTime > SlashTime){
                lastSlashTime = Time.GameTime;
                isSlashing = true;
                slashDir = wantedDir.Normalized * 200;
            }
            if(isSlashing){
                slashActor.LocalPosition = Vector3.Lerp(Vector3.Zero,slashDir, (Time.GameTime - lastSlashTime)/SlashDuration);
                if(Time.GameTime - lastSlashTime >= SlashDuration){
                    isSlashing = false;
                    slashActor.LocalPosition = Vector3.Down * 82;
                }
            }
        }
    }
}
