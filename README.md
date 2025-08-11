# COMP-6001-VuProGen-Buildings-AR

## Description of the Project
This is a project by Wong Chung Man for COMP-6001 Capstone Project and Dissertation at Open Institute of Technology (OPIT) for BSc in Modern Computer Science, under the supervision of Assistant Professor Tomasz Zawadzki.

This project aims to explore the practical aspects of procedural building generation and Augmented Reality (AR). An AR mobile application is developed using Unity and Vuforia, with an UI to allow the users to procedurally generate and edit a building on an image target in real time.

## Issue with shadows not showing properly during initial project set-up
When the project is initally set up, the shadows do not cast properly in game, this is because the build profiles is set to Windows but not Android. To resolve this issue, go to File -> Build Profiles. In Build Profiles, select Android and click "Switch Platform". The shadows should then show properly.