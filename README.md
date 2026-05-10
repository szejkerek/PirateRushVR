# Pirate Rush VR

[![Made With Unity](https://img.shields.io/badge/made%20with-Unity-57b9d3.svg?logo=Unity)](https://unity.com/)
[![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/szejkerek/PirateRushVR/UnityCI.yml)](https://github.com/szejkerek/PirateRushVR/actions)
[![License](https://img.shields.io/github/license/szejkerek/PirateRushVR?logo=github)]()
[![Last Commit](https://img.shields.io/github/last-commit/szejkerek/PirateRushVR?logo=Mapbox&color=orange)](https://github.com/szejkerek/PirateRushVR/commits/main/)
[![Repo Size](https://img.shields.io/github/repo-size/szejkerek/PirateRushVR?logo=VirtualBox)]()
[![GitHub Release](https://img.shields.io/github/v/release/szejkerek/PirateRushVR)](https://github.com/szejkerek/PirateRushVR/releases)
[![GitHub stars](https://img.shields.io/github/stars/szejkerek/PirateRushVR?branch=main&label=Stars&logo=GitHub&logoColor=ffffff&labelColor=282828&color=informational&style=flat)](https://github.com/szejkerek)
[![GitHub user stars](https://img.shields.io/github/stars/szejkerek?affiliations=OWNER&branch=main&label=User%20Stars&logo=GitHub&logoColor=ffffff&labelColor=282828&color=informational&style=flat)](https://github.com/szejkerek)

## Introduction

### Are you ready to become the ultimate VR pirate?

[Final thesis document](RAu-INZ-295650-2024.pdf)

[![YTLINK](https://github.com/szejkerek/PirateRushVR/assets/69083596/2b427374-4505-431a-85f3-ee924fa180d9)](https://youtu.be/OX5DuAdb4XI?si=0gu1AhMoBGAOVXAX)

Welcome to the VR game project, an engineering project designed for Oculus Quest 2. In this immersive experience, you take on the role of a pirate with the primary objective of destroying fruits using your saber and barrels with your trusty flintlock pistol. Beware of bombs and skillfully dodge them to stay in the game!

## Table of Contents  
- [Gameplay](#gameplay)
- [Features](#features)
- [Installation](#installation)
- [License](#license)
- [Contact](#contact)

## Gameplay

Engage in a thrilling adventure where your precision and speed are crucial. Utilize your saber to slice through fruits, earning points for each successful action and shoot down barrels and boxes with your flintlock pistol. Maintain a flawless gameplay to benefit from a multiplier, but be cautious—using the wrong tool against a projectile will result in a deduction of points. 

### Features
- **Immersive VR Experience**: Dive into a captivating world with Oculus Quest 2 compatibility.
- **Dual Weapon System**: Wield both a saber for slicing fruits and a flintlock pistol for shooting barrels and boxes.
- **Scoring Mechanism**: Earn points for each successful action, with multipliers for flawless gameplay.
- **Projectile Management**: Use the correct tool for projectiles to avoid point deductions.
- **Bomb Threat**: Dodge bombs to maintain your health; shooting them results in an explosive setback.
- **Tutorial System**: In-depth tutorials guide players on using controls and understanding game mechanics.

Beware of bombs! If you shoot a bomb, it explodes and reduces your health. The game incorporates informative tutorials, ensuring that players are well-acquainted with the controls and mechanics. Your performance is reflected in the leaderboard, so aim for the top spot by showcasing your skills and strategic prowess.

Upon starting the game, add your chosen nickname to compete for a spot on the leaderboard. Showcase your gaming prowess and climb to the top by achieving the highest score. 

### Customize your experience with various options:

- **Volume Control**: Adjust the game's audio to your preference.
- **Turn Type**: Choose between different turning mechanisms for a personalized VR experience.
- **Comfort Mode**: Enable comfort mode to reduce VR-induced nausea.

![Gameplay1](https://github.com/szejkerek/PirateRushVR/assets/69083596/8c5175ea-ef9d-4b9e-bcde-6a9cd17dd5b3)

![Gameplay2](https://github.com/szejkerek/PirateRushVR/assets/69083596/8e96835d-8381-421b-aec9-d850dcc2a09e)

![Barrel](https://github.com/szejkerek/PirateRushVR/assets/69083596/19993b1b-a4e0-4bb9-aa1f-77b32b41b545)

![Bomb](https://github.com/szejkerek/PirateRushVR/assets/69083596/21184acf-da0e-4785-8d7d-218ff0a0ed46)

![Leaderboard](https://github.com/szejkerek/PirateRushVR/assets/69083596/42ed9683-129b-4d45-944a-d1adb10b42fc)

![Tutorial](https://github.com/szejkerek/PirateRushVR/assets/69083596/cea52970-f84b-4edd-8bbe-89e54a332ccf)

![Options](https://github.com/szejkerek/PirateRushVR/assets/69083596/fb4c4fc4-2322-4910-9b2f-d1f6f1cb807d)

![Keyboard](https://github.com/szejkerek/PirateRushVR/assets/69083596/f2398f81-21c5-46f7-bd52-3fd8d48b569c)

## Installation

- Download ADB Drivers or use ```platform-tools``` folder from repository.
- Check the driver version using the command:
```
adb version
```
- Configure Oculus Quest 2 Headset and perform basic user account setup.
- Download the Oculus app for both desktop and mobile devices.
- Enable developer mode in the mobile app.
- Allow installation from unknown sources on the desktop.
- Physically connect the headset to the computer using a USB-C cable for data transfer.
- Check connected devices using the command:
```
  adb devices -l
```
- [Download](https://github.com/szejkerek/PirateRushVR/releases) and install the application on the device using the command:
```
adb install -r <file_location.apk>
```
- After successful installation, safely disconnect the headset from the computer.

## License

This project is licensed under the [MIT License](LICENSE).

## Contact

If you have any questions or need further information, feel free to contact me at [bartekk.gordon@gmail.com](mailto:bartekk.gordon@gmail.com).

## Code Highlights

### Perfect Slice Detection via Signed Mesh Volume

```csharp
// Assets/__Scripts/Player/Weapons/Sabre.cs

private float CalculateVolume(MeshCollider collider)
{
    Mesh mesh = collider.sharedMesh;
    if (mesh != null)
    {
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;

        float volume = 0f;
        for (int i = 0; i < triangles.Length; i += 3)
        {
            Vector3 v1 = vertices[triangles[i]];
            Vector3 v2 = vertices[triangles[i + 1]];
            Vector3 v3 = vertices[triangles[i + 2]];

            volume += SignedVolumeOfTriangle(v1, v2, v3);
        }
        return Mathf.Abs(volume);
    }
  
    return 0f;
}

private float SignedVolumeOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
{
    return Vector3.Dot(Vector3.Cross(p1, p2), p3) / 6f;
}

private bool IsSlicePerfect(GameObject upperHull, GameObject lowerHull)
{
    float upperVolume = CalculateVolume(upperHull.GetComponent<MeshCollider>());
    float lowerVolume = CalculateVolume(lowerHull.GetComponent<MeshCollider>());
    float totalVolume = upperVolume + lowerVolume;

    float upperRatio = upperVolume / totalVolume;
    float lowerRatio = lowerVolume / totalVolume;

    return IsWithinPerfectSlice(upperRatio) && IsWithinPerfectSlice(lowerRatio);
}

bool IsWithinPerfectSlice(float value)
{
    float lowerBound = 0.5f - perfectSliceTolerance;
    float upperBound = 0.5f + perfectSliceTolerance;
    return (value >= lowerBound && value <= upperBound);
}
```

After EzySlice splits a projectile into two mesh halves, the game must judge whether the player cut it near the center. Rather than using bounding-box approximations, `CalculateVolume` applies the divergence theorem: each triangle of the mesh is paired with the origin to form a signed tetrahedron, whose volume is `(v1 × v2) · v3 / 6`. Summing all tetrahedra and taking the absolute value gives the exact mesh volume. `IsSlicePerfect` then checks that neither hull deviates too far from a 50/50 volume split, with `perfectSliceTolerance` scaling with difficulty.

---

### Ballistic Trajectory Calculation

```csharp
// Assets/__Scripts/Cannons/CannonShooting.cs

public Vector3 CalculateDirection(Vector3 target, float gravity = -9.81f)
{
    float height = Mathf.Max(settings.Height.GetValueBetween(), target.y);

    float displacementY = target.y - shootingPoint.position.y;
    Vector3 displacementXZ = new Vector3(target.x - shootingPoint.position.x, 0, target.z - shootingPoint.position.z);

    Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
    Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity));

    return velocityY + velocityXZ;
}
```

Cannons aim at the player using full projectile-motion kinematics rather than simple raycasting. Given a configurable apex height `h`, the vertical launch speed is derived from `v_y = sqrt(-2*g*h)`. The total flight time is `sqrt(-2*h/g) + sqrt(2*(dy-h)/g)` — time to rise to apex plus time to fall to the target's altitude. Dividing the XZ displacement by that time gives the horizontal velocity component. Each shot also samples a random offset around the player position (`CalculateRandomTargetPosition`) and a random gravity value from an `Interval<float>`, producing varied arc shapes per difficulty level.

---

### Tick-based Command Queue for Projectile Combos

```csharp
// Assets/__Scripts/Projectiles/Combo/ComboController.cs

public void UpdateOnTick()
{
    if (isPaused())
        return;

    if (queuedBehaviors.Count != 0)
    {
        queuedBehaviors.Dequeue().Execute();
    }
    else
    {
        float comboChance = Random.Range(0f, 1f);
        if (comboChance <= currentDifficulty.GlobalComboChance)
        {
            AddGlobalCombo();
        }
        else
        {
            AddLocalCombo();
        }
    }
}

private void AddGlobalCombo()
{
    ComboDatabase comboDatabase = CannonsManager.Instance.ComboDatabases.SelectRandomElement();
    foreach (var combo in comboDatabase.combos)
    {
        queuedBehaviors.Enqueue(comboItemFactory.CreateSpawn(combo.Projectile));
        queuedBehaviors.Enqueue(comboItemFactory.CreateWait(combo.Wait));
    }
}

private void AddLocalCombo()
{
    EnqueueRandomProjectile();
    EnqueueRandomWaits(currentDifficulty.CountOf25msWaits);
}
```

Each cannon runs a `ComboController` that is driven by a `TickEngine` (32 Hz by default) rather than Unity's `Update` loop. On every tick it dequeues one `ICannonBehavior` command — either a `CannonSpawnBehavior` (fires a projectile) or a `CannonWaitBehavior` (blocks the queue for N ticks). When the queue empties, a probability roll decides between a *global* combo (a hand-authored sequence loaded from a `ComboDatabase` ScriptableObject) and a *local* combo (stochastically selected projectile type + random wait). `ComboItemFactory` translates human-readable wait durations (25 ms, 50 ms, ...) into tick counts at the current tick rate, keeping timing correct regardless of frame rate.
