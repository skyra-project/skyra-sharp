# Skyra Dockerfiles

This folder contains all the files required to control Docker development environments for Skyra. Most of the meat of
the content is in the `docker-compose.yml` file which has the info on which images can be build and as which containers
they would be ran. In order to easily control the docker-compose file there are powershell and bash scripts:
`ps-skyra.ps1` and `bash-skyra.sh`.

Alternatively if you have access to the .NET SDK you can also run `SkyraDocker.cs` will will in turn run the `ps-skyra.ps1` script.

Skyra currently has the following microservices that can be dockerized:

- Lavalink
  - Service name in docker-compose: `lavalink`
  - Image used: `skyrabot/lavalink:latest`
- PostgreSQL Database
  - Service name in docker-compose: `postgres`
  - Image used: `skyrabot/postgres:latest`
- GraphQL-Pokémon
  - Service name in docker-compose: `pokedex`
  - Image used: `favware/graphql-pokemon:latest`
- RabbitMQ
  - Service name in docker-compose: `rabbitmq`
  - Image used: `rabbitmq:management-alpine`
- Redis
  - Service name in docker-compose: `redis`
  - Image used: `redis:alpine`
    <!-- - InfluxDB
  - Service name in docker-compose: `influxdb`
  - Image used: `skyrabot/influxdb` -->

# Image Configuration

The following steps are required for each image for it to build on your machine. These images can run just fine without locally building, however for customization such as modifying the default password you need to build the image locally.

## Lavalink

1. Download the latest .jar file from https://ci.fredboat.com/viewLog.html?buildId=lastSuccessful&buildTypeId=Lavalink_Build&tab=artifacts&guest=1
2. Drop this .jar file in the 'lavalink' folder
3. Duplicate the 'application.example.yml' file and rename it to 'application.yml'
4. Set any password in the yaml file and also set the same password in config.ts in the root folder of this project

## Postgres

1. Duplicate the `Dockerfile.example` file in the `postgres` folder and name it `Dockerfile`
2. Fill in your desired `POSTGRES_USER`, `POSTGRES_PASSWORD` and `POSTGRES_DB`

<!--## InfluxDB-->

<!--1. In the influxdb folder, duplicate the 'config.sample.toml' file and rename it to 'config.toml'-->
