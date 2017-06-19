# Hands on with docker for .net devs

## Expectations and schedule

- Docker basics e.g. Container, Images etc. 30'
    - **5' Break**
- Getting familiar with docker cli 45'
    - **15' Coffee break**
- How to build own images 60'
    - **60' Lunch break**
- Running advanced scenario with multiple containers (compose) 45'
    - bring/build your own service
    - **10' Coffee break**
- Be creative 30'
    - Run your app in the cloud (azure, aws, digital ocean)
    - Build your swarm (locally)
    - Build your CI-System
    - Integrationtesting ???
    - [try out somethings from play with docker (official)](http://training.play-with-docker.com/)
- Retro 10'
------------------------------

## Start with installing (for those who haven't)

- Docker for windows(preferable) or docker toolbox
- VS Code with docker extension (optional)

[Troubleshooting](https://docs.docker.com/docker-for-windows/troubleshoot)

**Tip: use ethernet cabel instead of wifi since you'll have to use a nat-network adapter.**
------------------------------

## Tooling

Under windows we need to use either:
- Docker for Windows preferably
    - Linux and Windows
    - Docker Community vs Enterprise

- Docker toolbox (using virtualbox)
    - Linux containers only

Talk to the docker daemon:

- docker cli 
- kitematic (UI)
- portainer (UI)
- ...

-------------------------------------

# Cloud providers

- Azure
- Google cloud
- AWS
- [Docker cloud](https://cloud.docker.com/)
- [and some others](http://t3n.de/news/docker-hosting-613802/)


---------------

# Advantage docker
- lightweight
- small footprint
- isolation

# When to use?
- workflow (build, push, pull, run)
- workflow enhanced (build, sign, push, pull, test, sign, push, pull, run)


<!--https://github.com/mikegcoleman/docker101/blob/master/Docker_101_Workshop_DockerCon.pdf-->

<!--https://www.heise.de/developer/artikel/Windows-und-Linux-basierte-Docker-Container-auf-Windows-nutzen-Teil-1-von-2-3735148.html?artikelseite=3-->


Further info:
- [Containerize an app in 5 Steps](https://blog.sixeyed.com/how-to-dockerize-windows-applications/)
- [Swarm and orchestration workshop](http://jpetazzo.github.io/orchestration-workshop/#1)
- [.Net officially embraced Docker<3](https://blogs.msdn.microsoft.com/dotnet/2017/05/25/using-net-and-docker-together/)
