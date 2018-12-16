# Repository information

This repository contains Ubuntu Docker image with Apache2 server which is being used in Docker Swarm, as by the provided `docker-compose.yml` file.

The idea is to use this base image and extend it to support creation of virtual laboratories. Currently, on the Apache2 server a static web site is being hosted. Essentially, the static web site should be replaced with web application that will be handling the tasks of the virtual laboratory.