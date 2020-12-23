
| Description      | Status |
| ----------- | ----------- |
| server build on master | ![build-and-test-server](https://github.com/TomaT3/SpiritSpender/workflows/build-and-test-server/badge.svg?branch=master) |
| client build on master | ![build-and-test-client](https://github.com/TomaT3/SpiritSpender/workflows/build-and-test-client/badge.svg?branch=master) |
| latest version  | ![version](https://img.shields.io/github/v/tag/tomat3/SpiritSpender)        |
<br>

# SpiritSpender
## History
At [SchleFaz](https://www.schlefaz.de/) evenings with friends, there has always been this one poor guy who had to refill the shot glasses. The later the evening the more spirit was spilled and more uneven filling of the shot glasses. That's how the idea of an automatic SpiritSpender was born.

## How it works
The SpiritSpender can fill up to 12 shot glasses (single or double shot). Any bottle can be used that fits into a standard "Bar Liquor Dispenser". The SpiritSpender can be controlled from any device that has an Webbrowser.
<p float="left">
  <img src="Doc/pictures/IMG_20200702_135323.jpg" alt="SpiritSpenderFront" width="40%" height="40%">
  <img src="Doc/pictures/IMG_20200702_135011.jpg" alt="SpiritSpenderElectricControlBox" width="40%" height="40%">
</p>
<br><br>

## Hardware
Main components:
- RaspberryPi 4
- 2 stepper controller (TB6600)
- 2 stepper Motors (NEMA 17 2.0A)
- Linear Motor
- EmergencyStop

## Electrical layout
tbd

## Technical Background
3 Docker containers are running on the raspberry pi:
- MongoDb
  - used to store settings
- ASP .NET Core Server (GPIO control/logic)
  - controlles the hardware via GPIOs in-/output
  - offfers a web api for client
- Nginx Webserver (AngularApp for UI)
  - AngularApp to control the spirit spender from e.g. mobile phone with two main tasks:
    - Setup: setup hardware / change settings
    - Automatic: tell how much to fill in each glas and start filling process




