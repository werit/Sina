rem Run recipes microservice container
cd ..

docker build -t recipies:0.0.1 .
docker stop recipiesmicroservicecontainer
docker rm recipiesmicroservicecontainer
docker run -it --rm -d -p 3000:80 --name recipiesmicroservicecontainer recipies:0.0.1