# api-hackathon-2021-rmm-level-2
A repo to host the hackathon code for an API with a Richardson Maturity Model level of 2.

Background info: https://gist.github.com/SageCaleb/c70f8447e3c4255a6bd349037d79653a

This repository is intended to be paired with [these docs on creating a level 2 REST API][level-2-guide]

The code was initially generated based on the following command and subsequently modified:

```
dotnet new webapi -o MeteoriteLandingService
```

## Prerequisites

* [Docker Compose][docker-compose-install]

## Database

The database is hosted locally by running the following command:

```
docker-compose up -detach
```

Once this has been done, you can access the database at `localhost:8080` from code.\
For a visual view of the database, you can leverage the locally-hosted PgAdmin 4 instance running at [`localhost:5431`](localhost:5431)

## Repository Layout

### datasets

These are the raw `.csv` files for the datasets available to this project.

### sql

This contains a number of unique files

* `init.sql` - the `.sql` file mapped to the Postgres docker container to use in creating the initial database for this project, `datasets`
* `pgadmin_servers.json` - a file for PgAdmin 4 to have pre-existing knowledge of the local Postgres DB

The files starting with `V` are Flyway migration files and are used to create database tables and load data from the `.csv` files. This is done automatically with the docker-compose so that the database is setup and ready to be queried from the start.

[level-2-guide]: https://github.com/Sage-CRE/docs/APIs/REST/how-to-level-2.md
[docker-compose-install]: https://docs.docker.com/compose/install/
