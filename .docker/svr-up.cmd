
call "%~dp0build-api.cmd"
docker pull %REGISTRY%/tenant-mgt-api:dev
docker-compose -f api.yml -p tenant-mgt up -d 
TIMEOUT 15
