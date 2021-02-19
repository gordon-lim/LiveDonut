<!--
*** Thanks for checking out the Best-README-Template. If you have a suggestion
*** that would make this better, please fork the repo and create a pull request
*** or simply open an issue with the tag "enhancement".
*** Thanks again! Now go create something AMAZING! :D
***
***
***
*** To avoid retyping too much info. Do a search and replace for the following:
*** github_username, repo_name, twitter_handle, email, project_title, project_description
-->

<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->

<!-- PROJECT LOGO -->
<br />
<p align="center">
  <a href="https://github.com/github_username/repo_name">
    <img src="logo.png" alt="Logo" width="80" height="80">
  </a>

  <h3 align="center">Live Donut</h3>

  <p align="center">
    A YouTube live streaming game! Every chat drops a donut to feed my virtual dog!

<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary><h2 style="display: inline-block">Table of Contents</h2></summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li><a href="#overview">Overview</a></li>
    <li><a href="#demo">Demo</a></li>
    <li><a href="#challenges">Challenges</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgements">Acknowledgements</a></li>
    <li><a href="#useful-links">Useful links</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->

## About The Project

I came across a live stream on YouTube. Its title claimed that every like made the Google dinosaur go faster. The effect was not noticeable despite the number of likes the stream was receiving. I decided on making my own rendition of the 'interactive livestream' but with a more noticeable feedback on every viewer input - every message in the chat drops a donut to feed a virtual in real time.

### Built With

- [Unity](https://unity.com/)
- [YouTube Data API v3](https://developers.google.com/youtube/v3)
- [Streamlabs](https://streamlabs.com/)
- [AWS EC2](https://aws.amazon.com/ec2/)

<!-- OVERVIEW -->

## Overview

I used Unity engine to build the game. The game makes API calls to YouTube and checks for new messages and drops a donut for every new message. I then streamed the game to YouTube using Streamlabs from an AWS EC2 instance. The dog and donut animations were commissioned on Fiverr.

<!-- DEMO -->

## Demo

<!-- CHALLENGES -->

## Challenges

### YouTube Query Limits

If I wanted my live stream to work in real time (a donut drops the second a new message is posted), I needed to make calls to YouTube's every second but because YouTube limtis the numbers of calls you can make in a day I wasn't able to do it conventionally. To work around this, I generated the maximum number of API keys I could and looped through them. Google's algorithm, however, picked it up and disabled them after my stream had run for over a full day.

### Getting JSON to work in C#

I had previous experience working with JSON in python and java but it was quite convoluted when it came to C#.

<!-- CONTACT -->

## Contact

Gordon - gordonlim214@gmail.com

<!-- ACKNOWLEDGEMENTS -->

## Acknowledgements

- [Background and platform art](https://opengameart.org/content/platformer-pack-redux-360-assets)

<!-- USEFUL LINKS -->

## Useful Links

- [How to Make a Pet Simulation Game ](https://www.youtube.com/watch?v=JUgy7Lm3hH8&list=PLbCx65TBvT-QgTitVMCWGH1HQW_YfdMDK)
