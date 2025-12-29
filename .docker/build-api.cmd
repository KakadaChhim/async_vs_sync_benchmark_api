
call C:\Docker\svr\svr-cmd.cmd

docker build .. --file ../Dockerfile -t %REGISTRY%/tenant-mgt-api:dev

docker push %REGISTRY%/tenant-mgt-api:dev