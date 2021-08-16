# Stochastic-Neutronics-Visual

## Introduction

This project uses Unity to perform a simulation of neutrons within a system with a low neutron population. In this type of system, the behaviour of neutrons is stochastic as each neutron will travel in a random direction and may be absorbed (and, so disappear from the system) or they might cause a fission. If a fission occurs, anywhere between 0 and 7 neutrons may be produced (the average is around 2.4). This can have a substantial impact on the bahviour of scenarios with small numbers of neutrons such as a reactor which is starting up. A fuller discussion of this behaviour may be found in [Cooling, 2016](#1).

This project contains a project made in [Unity](https://unity.com/) which simulates neutrons being created and travelling within a 1m cube. Neutrons may then be absorved or cause a fission, potentially creating new neutrons. This is all displayed visually in 3D space. The project had been designed to be built as a standalone Windows app, a Web GL app which may be hosted online, or an Oculus Quest 2 app which allows the system to be viewed in Virtual Reality.

## Using the Project

To open the project, you will need to download the [Unity Hub and the Unity Editor](https://unity3d.com/get-unity/download). The project was built in Unity 2020.3.14f1 so you could download this version of the engine or download a newer version then [upgrade the project to your newer version](https://docs.unity3d.com/Manual/GettingStartedOpeningProjects.html). If you want to create a build of the project, you will need to install one or more of the following modules:

* Universal Windows Platform Build Support (for Windows)
* Mac Build Support (for Mac)
* Linux Build Support (for Linux)
* WebGL Build Support (for web deployment)
* Android Build Support (for Oculus Quest 2)

If you don't install any of these modules, you will still be able to run the project in the editor by clicking the "play" button at the top of the editor. It is also recommended you install the Microsoft Visual Studio module, regardless of how you intend to run the app.

If you want to build any of the standalone apps, or run it from the editor, make sure the "Desktop Components" GameObject in the hierarchy is active and the "Oculus Components" GameObject is inactive. If you want to run the device on an Oculus Quest 2, make sure the "Desktop Components" GameObject in the hierarchy is inactive and the "Oculus Components" GameObject is active. You can choose which build of the app you are making in the File/Build Settings menu and can also build the app from here.

To use the project on an Oculus Quest 2, you will also need:

* An Oculus Quest 2 headset and controllers,
* An Oculus Link cable (or equivalent) or to use [Oculus Air Link](https://support.oculus.com/link/),
* To have set your device to [developer mode](https://developer.oculus.com/documentation/native/android/mobile-device-setup/),
* To launch Oculus Rift from the Quest menu. 






## Bibliography

<a id="1"></a> C.M. Cooling, M.M.R. Williams, M.D. Eaton (2016)
Coupled Probabilistic and Point Kinetics Modelling of Fast Pulses in Nuclear Systems
Annals of Nuclear Energy, 94, 655-671
https://doi.org/10.1016/j.anucene.2016.04.012
