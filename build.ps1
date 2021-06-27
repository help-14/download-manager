#Requires -Version 7.0
cd ./src
docker build -t help14/download-manager:latest .
docker image push