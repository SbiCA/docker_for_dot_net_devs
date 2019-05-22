# windows container hello world

run a nano server container
```
docker run -it --rm mcr.microsoft.com/windows/nanoserver:1809 cmd.exe
```

open a new cmd/powershell

```
docker ps
````

inside the container run the command

``` 
ping -t 8.8.8.8
``` 

open task manager on your host add columns `Image Path Name`, `Command line`

monitor your task manager while you 

=> stop ping (CTRL+C)
=> stop container (CTRL+C)