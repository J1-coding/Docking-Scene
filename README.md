# ✈Docking Scene Simulation

Simulation demo of drone docking scene. Showcases a simulation of a drone autonomously docking to a target(basket), representing an aerospace docking scenario.

## Features
- **Missile Guidance-Based Docking**  
  The drone follows a missile-inspired trajectory to approach the docking basket with precision.
- **Smooth Rotation via Coroutines**  
  Quaternion-based rotations are handled with Unity coroutines for smooth movement.
- **Touchdown Event Triggering**  
  Transparent trigger colliders are placed along the drone trajectory, triggering sequential events such as detachment and speed adjustments.
- **Target Point Optimization for Docking**  
  A series of strategically placed target points ensure smooth, controlled docking.
- **Cinematic Camera Control**  
  Used Cinemachine and Dolby Track to enhance visual presentation.
- **Stable Physics Updates**  
  Used `FixedUpdate()` to maintain simulation stability.
  
## Demo Video  
[![Docking Scene Demo](https://img.youtube.com/vi/tNUVmWVUYQc/0.jpg)](https://www.youtube.com/watch?v=tNUVmWVUYQc&autoplay=1)


