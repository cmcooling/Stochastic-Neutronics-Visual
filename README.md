# Stochastic-Neutronics-Visual

## Introduction

This project uses Unity to perform a simulation of neutrons within a system with a low neutron population. In this type of system, the behaviour of neutrons is stochastic as each neutron will travel in a random direction and may be absorbed (and, so disappear from the system) or they might cause a fission. If a fission occurs, anywhere between 0 and 7 neutrons may be produced (the average is around 2.4). This can have a substantial impact on the bahviour of scenarios with small numbers of neutrons such as a reactor which is starting up. A fuller discussion of this behaviour may be found in [Cooling, 2016](#1).

This project contains a project made in [Unity](https://unity.com/) which simulates neutrons being created and travelling within a 1m cube. Neutrons may then be absorved or cause a fission, potentially creating new neutrons. This is all displayed visually in 3D space. The project had been designed to be built as a standalone Windows app, a Web GL app which may be hosted online, or an Oculus Quest 2 app which allows the system to be viewed in Virtual Reality.








## Bibliography

<a id="1"></a> C.M. Cooling, M.M.R. Williams, M.D. Eaton (2016)
Coupled Probabilistic and Point Kinetics Modelling of Fast Pulses in Nuclear Systems
Annals of Nuclear Energy, 94, 655-671
https://doi.org/10.1016/j.anucene.2016.04.012
