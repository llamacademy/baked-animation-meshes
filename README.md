# Baked Animation Meshes

In this tutorial repository and [accompanying video](https://youtu.be/Hh5zcT2IkaQ) you will learn how to bake animations as a series of "snapshots" of meshes. These snapshots can be taken at a configurable framerate that suits your game.
The concept here is attacking a similar problem to [Animation Instancing](https://blog.unity.com/technology/animation-instancing-instancing-for-skinnedmeshrenderer), but is still handled by the CPU and does not require any "shader magic" to manipulate the vertices. It also does not reduce the Draw Calls, as Animation Instancing will do. Instead, this attacks the "CPU Skinning" taking up a lot of CPU time on the main Unity thread to improve FPS.

This can be taken one step further to batch all the meshes into a single draw call per material via [Graphics.DrawMeshInstanced()](https://docs.unity3d.com/ScriptReference/Graphics.DrawMeshInstanced.html). Strangely, with my initial attempt, the performance was actually worse than what is implemented here. I didn't have time to dive into that and the performance here was "good enough" for my use case.

[![Youtube Tutorial](./Video%20Screenshot.jpg)](https://youtu.be/Hh5zcT2IkaQ)

This tutorial was inspired by the [codingwoodsman](https://github.com/codingwoodsman/ProximityManagerPart2)'s implementation. Special thanks to him for getting my gears turning on this topic and helping my own game's performace with this technique.

## Patreon Supporters
Have you been getting value out of these tutorials? Do you believe in LlamAcademy's mission of helping everyone make their game dev dream become a reality? Consider becoming a Patreon supporter and get your name added to this list, as well as other cool perks.
Head over to https://patreon.com/llamacademy to show your support.

### Phenomenal Supporter Tier
* Andrew Bowen
* Andrew Allbright
* YOUR NAME HERE!

### Tremendous Supporter Tier
* YOUR NAME HERE!

### Awesome Supporter Tier
* Gerald Anderson
* AudemKay
* Matt Parkin
* Ivan
* Reulan
* YOUR NAME HERE!

### Supporters
* Bastian
* Trey Briggs
* Matt Sponholz
* Dr Bash
* Tarik
* EJ
* Chris B.
* Sean
* YOUR NAME HERE!

## Other Projects
Interested in other AI Topics in Unity, or other tutorials on Unity in general? 

* [Check out the LlamAcademy YouTube Channel](https://youtube.com/c/LlamAcademy)!
* [Check out the LlamAcademy GitHub for more projects](https://github.com/llamacademy)

## Socials
* [YouTube](https://youtube.com/c/LlamAcademy)
* [Facebook](https://facebook.com/LlamAcademyOfficial)
* [TikTok](https://www.tiktok.com/@llamacademy)
* [Twitter](https://twitter.com/TheLlamAcademy)
* [Instagram](https://www.instagram.com/llamacademy/)
* [Reddit](https://www.reddit.com/user/LlamAcademyOfficial)

## Requirements
* Requires Unity 2021.3 LTS or higher.