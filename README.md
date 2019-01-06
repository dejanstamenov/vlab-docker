# Repository information

This repository contains Ubuntu Docker image which is being used in Docker Swarm, as by the provided `docker-compose.yml` file.

The idea is to use this base image and extend it to support creation of virtual laboratories. Currently, TCP server built with .NET Core is running inside the container accepting client requests and logging the data that is being streamed. As it is running in Docker Swarm mode, the load is balanced between the containers.

# Docker build tags

Following Docker image tags are available:

- `latest` - this is the latest **stable** image from the `master` branch in the repository. May not be complete with all the features.
- `experimental` - use for testing purposes **only**. The image is most likely not stable.
- `v1.2` - **preferred** image version. This is a **stable** image with all the required features.

Additional details regarding the releases are available at the Wiki [page](https://github.com/dejanstamenov/vlab-docker/wiki/Release-details) of the repository.