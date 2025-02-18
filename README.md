# Docking Scene Simulation

Simulation demo of drone docking scene. The project models a docking mechanism where a drone autonomously navigates to a designated target, simulating aerospace docking scenarios.


## Features
- **Missile Guidance-Based Docking**  
  The drone follows a missile-inspired trajectory to approach the docking basket with precision.
- **Smooth Rotation via Coroutines**  
  Quaternion-based rotations are handled with Unity coroutines for seamless movement.
- **Touchdown Event Triggering**  
  Transparent trigger colliders are placed along the trajectory to detect drone passage, triggering sequential events such as detachment and velocity adjustments.
- **Target Point Optimization for Docking**  
  A series of strategically placed target points ensure smooth, controlled docking.
- **Cinematic Camera Control**  
  Implemented with Unity’s Cinemachine and Dolby Track to enhance visual presentation.
- **Stable Physics Updates**  
  All physics-driven mechanics are executed within `FixedUpdate()` to maintain simulation stability.
  
## Demo Video  
[![Docking Scene Demo](https://img.youtube.com/vi/tNUVmWVUYQc/0.jpg)](https://www.youtube.com/watch?v=tNUVmWVUYQc&autoplay=1)


