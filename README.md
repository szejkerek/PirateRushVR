[![Made With Unity](https://img.shields.io/badge/made%20with-Unity-57b9d3.svg?logo=Unity)](https://unity.com/)
[![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/szejkerek/PirateRushVR/UnityCI.yml)](https://github.com/szejkerek/PirateRushVR/actions)


<p align="center"><h1>Pirate Rush VR</h1></p>
<p align="center">

  <a>
    <a href="https://unity.com/">
    <img alt="Made With Unity" src="https://img.shields.io/badge/made%20with-Unity-57b9d3.svg?logo=Unity">
  </a>
  <a>
    <a href="https://github.com/szejkerek/PirateRushVR/actions">
    <img alt="GitHub Actions Workflow Status" src="https://img.shields.io/github/actions/workflow/status/szejkerek/PirateRushVR/UnityCI.yml">
  </a> 
  <a>
  <img alt="License" src="https://img.shields.io/github/license/szejkerek/PirateRushVR?logo=github">
  </a>
  <a>
    <a href="https://github.com/szejkerek/PirateRushVR/commits/main/">
    <img alt="Last Commit" src="https://img.shields.io/github/last-commit/szejkerek/PirateRushVR?logo=Mapbox&color=orange">
  </a>
  <a>
    <img alt="Repo Size" src="https://img.shields.io/github/repo-size/szejkerek/PirateRushVR?logo=VirtualBox">
  </a>
  <a href="https://github.com/szejkerek/PirateRushVR/releases">
    <img alt="GitHub Release" src="https://img.shields.io/github/v/release/szejkerek/PirateRushVR">
  </a>
  <a>
    <img alt="GitHub stars" src="https://img.shields.io/github/stars/szejkerek/PirateRushVR?branch=main&label=Stars&logo=GitHub&logoColor=ffffff&labelColor=282828&color=informational&style=flat">
  </a>
  <a>
    <img alt="GitHub user stars" src="https://img.shields.io/github/stars/szejkerek?affiliations=OWNER&branch=main&label=User%20Stars&logo=GitHub&logoColor=ffffff&labelColor=282828&color=informational&style=flat">
  </a>
</p>

## Introduction

### Are you ready to become the ultimate VR pirate?

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
